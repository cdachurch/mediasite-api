Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Globalization
Imports System.Data.Entity
Imports mediaSite3.Models
Imports mediaSite3.Params

Namespace Repositories
    Public Class SongRepo
        Inherits dbUtil

        Public Sub New()
            MyBase.New("DevelopmentConnection")
        End Sub

        'Public Property Songs As List(Of Song)

        Public Sub SetFileName(songId As Integer, fileType As Integer, fileName As String)
            Dim fieldName As String

            If fileType = 0 Then
                fieldName = "Mp3Link"
            Else
                fieldName = "FileLink"
            End If
 
            ExecuteWithoutIdentity("UPDATE Songs SET " & fieldName & "=@P0 WHERE Id=@P1", fileName, songId)
 
        End Sub

        Public Function GetFileName(songId As Integer, fileType As Integer) As String
            Dim fieldName As String

            If fileType = 0 Then
                fieldName = "Mp3Link"
            Else
                fieldName = "FileLink"
            End If

            Return QueryScalar(Of String)("SELECT " & fieldName & " FROM Songs WHERE Id = @P0", songId)

        End Function

        Public Function SongCount() As Long
            Return QueryScalar(Of Long)("SELECT COUNT(*) From Songs WHERE DeletedDate is NULL ", Nothing)
        End Function

        Public Function BrowseSongs(params As BrowseSongsParams) As List(Of Song)
            Dim pNo = params.page
            Dim pSize = params.rows
            Dim pOrderBy = params.sidx
            Dim pDir = params.sord
            Dim intFirstRowPointer As Integer
            If pNo = 1 Then
                intFirstRowPointer = 1
            Else
                intFirstRowPointer = ((pNo - 1) * pSize) + 1
            End If

            Dim strSQL As String = "SELECT * " & _
                                   "FROM (" & _
                                   "SELECT *, ROW_NUMBER() OVER (ORDER BY " & pOrderBy & " " & pDir & ") AS RowNum " & _
                                   "FROM Songs WHERE DeletedDate Is Null" & _
                                   ") AS Songs_Page " & _
                                   "WHERE Songs_Page.RowNum BETWEEN " & intFirstRowPointer & "  AND " & (intFirstRowPointer + pSize - 1)


            Dim objList As List(Of Song) = Query(Of Song)(strSQL, Nothing)

            Return objList
        End Function

        Public Function SongById(id As Integer) As Song
            Return QuerySingle(Of Song)("SELECT * FROM Songs WHERE ID = @P0", id)
        End Function

        Public Function SearchSong(Params As SearchSongsParams, ByRef pCount As Long) As List(Of Song)

            Dim intFirstRowPointer As Integer
            If Params.page = 1 Then
                intFirstRowPointer = 1
            Else
                intFirstRowPointer = ((Params.page - 1) * Params.rows) + 1
            End If
            Dim orderBy As String = ""
            Dim strSearchFilter As String = GenerateSearchCriteria(Params, orderBy)

            Dim strSQL As String = "SELECT *  " & _
                                   "FROM (" & _
                                   "SELECT *, ROW_NUMBER() OVER (" & orderBy & ") AS RowNum " & _
                                   "FROM Songs " & _
                                   strSearchFilter & ") AS Songs_Page " & _
                                   "WHERE Songs_Page.RowNum BETWEEN " & intFirstRowPointer & "  AND " & (intFirstRowPointer + Params.rows - 1)


            Dim objList As List(Of Song) = Query(Of Song)(strSQL, Nothing)

            pCount = QueryScalar(Of Long)("SELECT COUNT(*) FROM Songs " & strSearchFilter, Nothing)

            Return objList
        End Function

        Private Function SQLQuote(pSQL As String) As String
            Return pSQL.Replace("'", "''")
        End Function

        Private Function GenerateSearchCriteria(Params As SearchSongsParams, ByRef pOrderBy As String) As String
            Dim strRetSQL As String = ""
            Dim strSearchTextArr() As String = Params.searchText.Split(" ")
            Dim strSearchField As String = ""
            Dim strSearchTextItem As String

            Select Case Params.searchField.ToLower
                Case "title"
                    strSearchField = "(Title like '%{!!}%')"
                    strRetSQL += strSearchField.Replace("{!!}", SQLQuote(Params.searchText))
                    pOrderBy = "ORDER BY " & Params.sidx & " " & Params.sord
                Case "artist"
                    strSearchField = "(Author1 like '%{!!}%' OR Author2 like '%{!!}%')"
                    strRetSQL += strSearchField.Replace("{!!}", SQLQuote(Params.searchText))
                    pOrderBy = "ORDER BY Author1,Author2 ASC"
                Case "keywords"
                    strSearchField = "(SongText like '%{!!}%')"
                    strRetSQL += " ("
                    For Each strSearchTextItem In strSearchTextArr
                        If strRetSQL <> " (" Then strRetSQL += " OR "
                        strRetSQL += strSearchField.Replace("{!!}", SQLQuote(strSearchTextItem))
                    Next
                    strRetSQL += ")"
                    pOrderBy = "ORDER BY Title ASC"
                Case "style"
                    strSearchField = "(Style like '%{!!}%')"
                    strRetSQL += strSearchField.Replace("{!!}", SQLQuote(Params.searchText))
                    pOrderBy = "ORDER BY Style ASC"
                Case "use"
                    strSearchField = "(Use1 like '%{!!}%' OR Use2 like '%{!!}%')"
                    strRetSQL += strSearchField.Replace("{!!}", SQLQuote(Params.searchText))
                    pOrderBy = "ORDER BY Use1,Use2 ASC"
                Case "favourites"
                    strSearchField = "(Fav > 0)"
                    strRetSQL += strSearchField
                    pOrderBy = "ORDER BY Fav DESC"
                Case "new"
                    strSearchField = "(LastModDate >= '" & Year(Now) & "0101')"
                    strRetSQL += strSearchField
                    pOrderBy = "ORDER BY LastModDate DESC"
            End Select

            If Params.searchFilter > 0 Then
                strRetSQL += " AND GroupId = " & Params.searchFilter
            End If

            Return "WHERE " & strRetSQL

        End Function

        '    string sql = "INSERT INTO CUSTOMERS(CUSTOMERID,COMPANYNAME,CONTACTNAME,COUNTRY) 
        '              VALUES(@P0,@P1,@P2,@P3)";
        'List<object> parameterList = new List<object>();
        'parameterList.Add("AAAAA");
        'parameterList.Add("Company 1");
        'parameterList.Add("Contact 1");
        'parameterList.Add("USA");
        'object[] parameters1 = parameterList.ToArray();
        'int result = db.Database.ExecuteSqlCommand(sql, parameters);
    End Class

End Namespace