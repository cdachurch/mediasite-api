Imports mediaSite3.ViewModels
Imports mediaSite3.Models
Imports mediaSite3.Repositories
Imports mediaSite3.Params

Namespace Services
    Public Class SongsService
        Implements IDisposable

        Private _songRepo As New SongRepo
        Private _AWSGateWay As New Gateways.AWS

        Public Function GetSongCount()
            Return _songRepo.SongCount()
        End Function

        Public Function BrowseSongs(Params As BrowseSongsParams) As List(Of Song)
            Return _songRepo.BrowseSongs(Params)
        End Function

        Public Function GetSong(songId As Integer) As Song
            Dim objSong As Song
            objSong = _songRepo.SongById(songId)

            Dim objMediaCodec As New MediaCodec(Nothing, songId, objSong.Title, objSong.SongKey)
            objSong.SongData = objMediaCodec.Decode(objSong.SongData, objSong.SongEncoding)
            objMediaCodec = Nothing

            Return objSong
        End Function

        Public Function SearchSong(Params As SearchSongsParams, ByRef pCount As Long) As List(Of Song)
            Return _songRepo.SearchSong(Params, pCount)
        End Function

        Public Function SaveSong(SongData As Song) As String
            Return ""
        End Function

        Public Function DeleteSong(songId As Integer) As String
            Return ""
        End Function

        Public Sub SetFileName(songId As Integer, fileType As Integer, fileName As String)
            _songRepo.SetFileName(songId, fileType, fileName)
        End Sub

        Public Function GetFileName(songId As Integer, fileType As Integer) As String
            Return _songRepo.GetFileName(songId, fileType)
        End Function

        Public Function DownloadFile(songId As Integer, fileTypeId As Integer) As System.IO.MemoryStream

            'Get File 
            Dim fileKey = GetFileNameString(songId, fileTypeId)
            Return _AWSGateWay.DownloadFile(fileKey)

        End Function

        Public Function UploadFile(file As HttpPostedFileBase, songId As String, fileTypeId As Integer) As HttpStatusCodeResult

            'Get File 
            Dim fileKey = GetFileNameString(songId, fileTypeId)
            Return New HttpStatusCodeResult(_AWSGateWay.UploadFile(fileKey, file.InputStream))

        End Function

        Public Function DeleteFile(songId As String, fileTypeId As Integer) As HttpStatusCodeResult

            'Get File 
            Dim fileKey = GetFileNameString(songId, fileTypeId)
            Return New HttpStatusCodeResult(_AWSGateWay.DeleteFile(fileKey))

        End Function

        Private Function GetFileNameString(ByVal p_intID As Integer, ByVal p_intfileType As Integer) As String

            Dim strRet As String = "00000000"

            If p_intfileType = 0 Then
                strRet = "00000000" & p_intID
                strRet = Right(strRet, 8)
            Else
                strRet = "00000000" & p_intID
                strRet = "1" & Right(strRet, 7)
            End If

            GetFileNameString = strRet & ".dat"

        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    _songRepo.Dispose()
                    _AWSGateWay.Dispose()
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class


End Namespace