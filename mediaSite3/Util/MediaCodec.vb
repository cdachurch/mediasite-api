

Public Class MediaCodec

    Public Enum ENCODETYPE As Integer
        Legacy = 0
        XML = 1
        PlainText = 2
        HTML = 3
        PlainTextLyric = 4
        Json = 5
        NONE = 99
    End Enum

    Private Enum LINETYPE As Integer
        Lyric = 0
        Notes = 1
    End Enum

    Private Enum OBJECTTYPE As Integer
        song = 0
        songPart = 1
        songNoteLine = 2
        songNote = 4
        songLyricLine = 3
    End Enum

    'var XMLTAGS = {
    '	song : 'SONG',
    '	part : 'PART',
    '	lyricLine : 'LYRICLINE',
    '	noteLine: 'NOTELINE',
    '	note: 'NOTE',
    '	xpos: 'XPOS'
    '};

    'Public Enum HTML_CLASS_NAMES As String
    '    song = "songContainer"
    '    part = "songPart"
    '    partName = "songPartName"
    '    lyricLine = "lyricLine"
    '    noteLine = "noteLine"
    'End Enum


    'var vbCrLf = '\r\n';

    Private config As Object
    Private id As Integer
    Private title As String
    Private key As String

    Public Sub New(pConfig, pId, pTitle, pSongKey)
        config = pConfig
        id = pId
        title = pTitle
        key = pSongKey

    End Sub

    Public Function Encode(pJsonData As SongJson, pEncodeTo As ENCODETYPE, pTransposeKey As String) As Object
        Dim transposeObj = New transposer(key, pTransposeKey)

        Select Case pEncodeTo
            'Case ENCODETYPE.HTML
            '    Return encodeHTML(pJsonData, OBJECTTYPE.song, transposeObj)

            'Case ENCODETYPE.PlainText
            '    Return encodePlainText(pJsonData, OBJECTTYPE.song, True)

            'Case ENCODETYPE.PlainTextLyric
            '    Return encodePlainText(pJsonData, OBJECTTYPE.song, False)

            'Case ENCODETYPE.Legacy
            '    Return encodeLegacy(pJsonData, OBJECTTYPE.song)

            Case ENCODETYPE.Json
                Return pJsonData

                'Case ENCODETYPE.XML
                '    Return encodeXML(pJsonData, OBJECTTYPE.song)

        End Select
    End Function

    Public Function Decode(pDataStream As String, pDecodeFrom As ENCODETYPE) As Object
        Select Case pDecodeFrom
            Case ENCODETYPE.Legacy
                Return decodeLegacy(pDataStream, OBJECTTYPE.song)

                'Case ENCODETYPE.XML
                '    Return decodeXML(pDataStream, OBJECTTYPE.song)

                'Case ENCODETYPE.PlainText
                '    Return decodePlainText(pDataStream, OBJECTTYPE.song)

        End Select
    End Function

    Private Function decodeLegacy(pData As Object, pObjectType As OBJECTTYPE) As Object
        Dim retData As Object
        Dim temp As String

        Select Case pObjectType
            Case OBJECTTYPE.song
                'Extract Header
                Dim header As String = pData.Substring(1, (pData.IndexOf(">") - 1))
                temp = header.Substring(0, (header.IndexOf(",")))

                'Get # of Parts
                Dim intPartCount As Integer = CInt(temp)

                'Get Part Name List
                temp = header.Substring(header.IndexOf(",") + 1)
                Dim partNamesList = temp.Split(",")

                retData = New SongJson With {.id = id, .key = key, .partCount = intPartCount, .title = title}

                For I = 0 To (partNamesList.Length - 1)
                    temp = partNamesList(I)
                    Dim startTagPos = InStr(pData, "<" & temp & ">")
                    Dim endTagPos = InStr(pData, "</" & temp & ">")
                    Dim start = startTagPos + (temp.Length + 2)
                    Dim strEnd = endTagPos - start
                    retData.parts.add(decodeLegacy({temp, Mid(pData, start, strEnd)}, OBJECTTYPE.songPart))
                Next

            Case OBJECTTYPE.songPart
                retData = New SongPartJson With {.partName = pData(0)}

                Dim lineArr = pData(1).ToString.Split(New String() {"<NL>"}, StringSplitOptions.RemoveEmptyEntries)

                For I = 0 To (lineArr.Length - 1)
                    If (lineArr(I).IndexOf("<") > -1 AndAlso lineArr(I).IndexOf(">") > -1) Then
                        retData.partData.Add(decodeLegacy(lineArr(I), OBJECTTYPE.songNoteLine))
                    Else
                        retData.partData.Add(decodeLegacy(lineArr(I), OBJECTTYPE.songLyricLine))
                    End If
                Next

            Case OBJECTTYPE.songLyricLine
                retData = New SongPartLine With {.lyric = pData}

            Case OBJECTTYPE.songNoteLine
                retData = New SongPartLine
                Dim noteArr = pData.ToString.Split(New String() {">"}, StringSplitOptions.RemoveEmptyEntries)
                For I = 0 To (noteArr.Length - 1)
                    temp = noteArr(I).Trim()
                    temp = temp.Replace("<", "")
                    Dim noteItemArr = temp.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                    If (temp <> "") Then
                        retData.note.Add(decodeLegacy(noteItemArr, OBJECTTYPE.songNote))
                    End If
                Next

            Case OBJECTTYPE.songNote
                retData = New SongPartNote With {.note = pData(0), .position = pData(1)}
        End Select

        Return retData

    End Function




    Private Class transposer
        Private key As String
        Private transposeKey As String
        Private noteArray1 As String()
        Private noteArray2 As String()
        Private numTransposeVal As Integer

        Public Sub New(pKey As String, pTransposeKey As String)

            key = pKey
            transposeKey = pTransposeKey

            ReDim noteArray1(11)
            ReDim noteArray2(11)

            noteArray1(0) = "C"
            noteArray2(0) = "C"
            noteArray1(1) = "C#"
            noteArray2(1) = "Db"
            noteArray1(2) = "D"
            noteArray2(2) = "D"
            noteArray1(3) = "D#"
            noteArray2(3) = "Eb"
            noteArray1(4) = "E"
            noteArray2(4) = "E"
            noteArray1(5) = "F"
            noteArray2(5) = "F"
            noteArray1(6) = "F#"
            noteArray2(6) = "Gb"
            noteArray1(7) = "G"
            noteArray2(7) = "G"
            noteArray1(8) = "G#"
            noteArray2(8) = "Ab"
            noteArray1(9) = "A"
            noteArray2(9) = "A"
            noteArray1(10) = "A#"
            noteArray2(10) = "Bb"
            noteArray1(11) = "B"
            noteArray2(11) = "B"

            numTransposeVal = getNoteShiftVal(pKey, pTransposeKey)

        End Sub

        Function getNoteShiftVal(pSongKey, pTransposeKey)
            Dim i As Integer = 0
            Dim num1 As Integer = 0
            Dim num2 As Integer = 0

            If (pSongKey = pTransposeKey) Then
                Return 0
            End If

            For i = 0 To 11
                If (noteArray1(i) = pSongKey.toUpper) Then num1 = i
                If (noteArray2(i) = pSongKey.toUpper) Then num1 = i
                If (noteArray1(i) = pTransposeKey.toUpper) Then num2 = i
                If (noteArray2(i) = pTransposeKey.toUpper) Then num2 = i

            Next

            Return (num2 - num1)

        End Function

        Public Function getTransposedNote(pNote)
            Dim track As Integer
            Dim bNoteFound As Boolean
            Dim noteNumVal As Integer

            If (pNote = "") Then Return ""

            For I = 0 To 11
                If (noteArray1(I) = pNote.toUpper) Then
                    track = 0
                    bNoteFound = True
                    noteNumVal = I
                    Exit For
                End If
                If (noteArray2(I) = pNote.toUpper) Then
                    track = 1
                    bNoteFound = True
                    noteNumVal = I
                    Exit For
                End If
            Next

            'Simple Note Case --- Transpose & we're done
            If (bNoteFound = True) Then
                If (track = 0) Then
                    Return noteArray1(wrapNoteVal(noteNumVal))
                Else
                    Return noteArray2(wrapNoteVal(noteNumVal))
                End If
            End If



            'Complex Note (i.e. CSus4/F)
            'Parse the Note, separate out the elements, transpose elements, recombine
            If (pNote.indexof("/") > -1) Then

                Dim noteArr As String() = pNote.split("/")
                Dim retAry = New List(Of String)

                For I = 0 To (noteArr.Count - 1)
                    retAry.Add(getTransposedNote(noteArr(I)))
                Next

                Return String.Join("/", retAry)
            End If

            Dim notes As String = "ABCDEFG"
            Dim flatSharp As String = "#b"

            'Note with minor, aug, sus, 7
            If (notes.IndexOf(pNote.substring(0, 1)) > -1) Then
                'The first character is a note.

                'Check for "#" or "b"

                If (flatSharp.IndexOf(pNote.substring(1, 2)) > -1) Then
                    Return getTransposedNote(pNote.substring(0, 2)) + pNote.substring(2)
                Else
                    Return getTransposedNote(pNote.substring(0, 1)) + pNote.substring(1)
                End If
            End If
            'Flag as Error
            Return "{!}"

        End Function

        Private Function wrapNoteVal(pNoteVal As Integer) As Integer
            If ((pNoteVal + numTransposeVal) < 12) Then
                Return (pNoteVal + numTransposeVal)
            Else
                Return (pNoteVal + numTransposeVal) - 12
            End If
        End Function

    End Class

End Class


Public Class SongJson
    Public Property id As Integer
    Public Property title As String
    Public Property key As String
    Public Property partCount As Integer
    Public Property parts As New List(Of SongPartJson)
End Class

Public Class SongPartJson
    Public Property partName As String
    Public Property partData As New List(Of SongPartLine)
End Class

Public Class SongPartLine
    Public Property lyric As String
    Public Property note As New List(Of SongPartNote)
End Class

Public Class SongPartNote
    Public Property position As Integer
    Public Property note As String
End Class
