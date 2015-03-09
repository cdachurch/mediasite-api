Imports System.Net
Imports System.Web.Http
Imports mediaSite3.Models
Imports mediaSite3.ViewModels
Imports Newtonsoft.Json
Imports mediaSite3.Params
Imports System.Net.WebRequestMethods
Imports System.Net.Http
Imports System.Net.Http.Headers

Public Class SongsController
    Inherits ApiController

    Dim SongSvc As New Services.SongsService

    <ActionName("BrowseJQGrid")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function BrowseJQGrid(<FromUri()> Params As BrowseSongsParams) As Dictionary(Of String, Object)

        If Params.AuthToken <> Configuration.Properties.Item("APIToken") Then Throw New Exception("Invalid API Token.")

        If Params.sidx = Nothing Then Params.sidx = "Title"
        If Params.page = 0 Then Params.page = 1
        If Params.rows = 0 Then Params.rows = 25
        If Params.sord = Nothing Then Params.sord = "ASC"
        Dim rows As New List(Of JQGridRow)
        Dim records As Integer = SongSvc.GetSongCount()
        For Each SongItem In SongSvc.BrowseSongs(Params)
            Dim cells() As String
            cells = {SongItem.Title, SongItem.Author1, SongItem.Author2}
            Dim Row As New JQGridRow() With {.id = SongItem.Id, .cell = New List(Of String)(cells)}
            rows.Add(Row)
        Next

        Dim objDictObj As New Dictionary(Of String, Object)
        objDictObj.Add("page", Params.page)
        objDictObj.Add("records", records)
        objDictObj.Add("total", Math.Ceiling(records / Params.rows))
        objDictObj.Add("userdata", Nothing)
        objDictObj.Add("rows", rows)
        Return objDictObj

    End Function

    <ActionName("Search")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function Search(<FromUri()> Params As SearchSongsParams) As Dictionary(Of String, Object)
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        If Params.sidx = Nothing Then Params.sidx = "Title"
        If Params.page = 0 Then Params.page = 1
        If Params.rows = 0 Then Params.rows = 25
        If Params.sord = Nothing Then Params.sord = "ASC"
        If Params.searchField = Nothing Then Params.searchField = "Title"
        If Params.searchText = Nothing Then Params.searchText = ""
        If Params.searchOper = Nothing Then Params.searchOper = "contains"

        Dim rows As New List(Of JQGridRow)
        Dim records As Long
        Dim SongColl = SongSvc.SearchSong(Params, records)
        For Each SongItem In SongColl
            Dim cells() As String
            cells = {SongItem.Title, SongItem.Author1, SongItem.Author2}
            Dim Row As New JQGridRow() With {.id = SongItem.Id, .cell = New List(Of String)(cells)}
            rows.Add(Row)
        Next

        Dim objDictObj As New Dictionary(Of String, Object)
        objDictObj.Add("page", Params.page)
        objDictObj.Add("records", records)
        objDictObj.Add("total", Math.Ceiling(records / Params.rows))
        objDictObj.Add("userdata", Nothing)
        objDictObj.Add("rows", rows)
        Return objDictObj

    End Function

    <ActionName("Search2")> _
   <HttpGet()> _
   <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function Search2(<FromUri()> Params As SearchSongsParams) As Dictionary(Of String, Object)
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        If Params.sidx = Nothing Then Params.sidx = "Title"
        If Params.page = 0 Then Params.page = 1
        If Params.rows = 0 Then Params.rows = 25
        If Params.sord = Nothing Then Params.sord = "ASC"
        If Params.searchField = Nothing Then Params.searchField = "Title"
        If Params.searchText = Nothing Then Params.searchText = ""
        If Params.searchOper = Nothing Then Params.searchOper = "contains"

        Dim rows As New List(Of MediaSiteGridRow)
        Dim records As Long
        Dim SongColl = SongSvc.SearchSong(Params, records)
        For Each SongItem In SongColl
            Dim Row As New MediaSiteGridRow() With {.id = SongItem.Id,
                                                    .author1 = SongItem.Author1,
                                                    .author2 = SongItem.Author2,
                                                    .ccli = SongItem.CCLI,
                                                    .copyDate = SongItem.CopyDate,
                                                    .createdDate = SongItem.CreatedDate,
                                                    .deletedDate = SongItem.DeletedDate,
                                                    .fileLink = SongItem.FileLink,
                                                    .fontSize = SongItem.FontSize,
                                                    .lastModifiedDate = SongItem.LastModifiedDate,
                                                    .mp3Link = SongItem.Mp3Link,
                                                    .notes = SongItem.Notes,
                                                    .publisher = SongItem.Publisher,
                                                    .reference = SongItem.Reference,
                                                    .sheetLayout = SongItem.SheetLayout,
                                                    .songKey = SongItem.SongKey,
                                                    .songOrder = SongItem.SongOrder,
                                                    .style = SongItem.Style,
                                                    .tempo = SongItem.Tempo,
                                                    .title = SongItem.Title,
                                                    .urlLink = SongItem.UrlLink,
                                                    .use1 = SongItem.Use1,
                                                    .use2 = SongItem.Use2,
                                                    .youTubeLink = SongItem.YouTubeLink
                                                  }
            rows.Add(Row)
        Next

        Dim objDictObj As New Dictionary(Of String, Object)
        objDictObj.Add("status", HttpStatusCode.OK)
        objDictObj.Add("totalRecords", records)
        objDictObj.Add("pageNumber", Params.page)
        objDictObj.Add("totalPages", Math.Ceiling(records / Params.rows))
        objDictObj.Add("results", rows)
        Return objDictObj

    End Function

    <ActionName("GetSingleSong")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function GetSingleSong(<FromUri()> Params As GetSongParams) As Song
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SongSvc.GetSong(Params.id)
    End Function

    <ActionName("SaveSong")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function SaveSong(<FromUri()> Params As SaveSongParams) As String
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SongSvc.SaveSong(Params.songData)
    End Function

    <ActionName("DeleteSong")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function DeleteSong(<FromUri()> Params As DeleteSongParams) As String
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SongSvc.DeleteSong(Params.id)
    End Function

    <ActionName("GetCols")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function GetCols() As Object

        Return {New With {.name = "Title", .index = "Title", .width = 200},
                New With {.name = "Author1", .index = "Author1", .width = 60},
                New With {.name = "Author2", .index = "Author2", .width = 60}}

    End Function

    <ActionName("UploadFile")> _
    <HttpPost()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function UploadFile(AuthToken As String, file As HttpPostedFileBase, songId As Integer, fileType As Integer) As HttpStatusCodeResult
        If AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Dim result = SongSvc.UploadFile(file, songId, fileType)
        If result.StatusCode = HttpStatusCode.OK Then
            SongSvc.SetFileName(songId, fileType, file.FileName)
        End If
    End Function

    <ActionName("DownloadFile")> _
   <HttpGet()> _
   <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function DownloadFile(AuthToken As String, fileId As String) As HttpResponseMessage
        If AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Dim fileData = SongSvc.DownloadFile(fileId, 0)
        Dim fileName = SongSvc.GetFileName(fileId, 0)
        Dim result As New HttpResponseMessage(HttpStatusCode.OK)
        result.Content = New StreamContent(fileData)
        Dim cd As New System.Net.Mime.ContentDisposition With {.FileName = fileName, .Inline = False}
        result.Content.Headers.ContentType = New MediaTypeHeaderValue("application/octet-stream")
        result.Content.Headers.Add("Content-Disposition", cd.ToString())
        Return result
    End Function

    <ActionName("StreamFile")> _
    <OverrideResultFilters> _
    <HttpGet()> _
   <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function StreamFile(AuthToken As String, fileId As String) As FileContentResult
        If AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Dim fileData = SongSvc.DownloadFile(fileId, 0)
        Dim fileName = SongSvc.GetFileName(fileId, 0)
        Return New FileContentResult(fileData.ToArray, "application/octet-stream") With {.FileDownloadName = fileName}
    End Function

    <ActionName("DeleteFile")> _
    <HttpPost()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Function DeleteFile(AuthToken As String, songId As String, fileType As Integer) As HttpStatusCodeResult
        If AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Dim result = SongSvc.DeleteFile(songId, fileType)
        If result.StatusCode = HttpStatusCode.OK Then
            SongSvc.SetFileName(songId, fileType, "")
        End If
    End Function

End Class
