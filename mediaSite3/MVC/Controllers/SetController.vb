Imports System.Net
Imports System.Web.Http
Imports mediaSite3.ViewModels
Imports mediaSite3.Models

Public Class SetController
    Inherits ApiController

    Dim SetSvc As New Services.SetService

    <ActionName("GetSets")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function GetSets(<FromUri()> Params As GetSetsParams) As List(Of SetList)
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SetSvc.GetSets(Params)
    End Function

    <ActionName("GetSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function GetSet(<FromUri()> Params As GetSetParams) As SetList
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SetSvc.GetSet(Params)
    End Function

    <ActionName("SaveSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function SaveSet(<FromUri()> Params As SaveSetParams) As ActionResult
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SetSvc.SaveSet(Params)
    End Function

    <ActionName("DeleteSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function DeleteSet(<FromUri()> Params As DeleteSetParams) As ActionResult
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SetSvc.DeleteSet(Params)
    End Function

    <ActionName("PublishSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function PublishSet(<FromUri()> Params As PublishSetParams) As ActionResult
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SetSvc.PublishSet(Params)
    End Function

    <ActionName("AddSongToSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function AddSongToSet(<FromUri()> Params As AddSongToSetParams) As List(Of SetListSong)
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SetSvc.AddSongToSet(Params)
    End Function

    <ActionName("RemoveSongFromSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function RemoveSongFromSet(<FromUri()> Params As RemoveSongFromSetParams) As List(Of SetListSong)
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SetSvc.RemoveSongFromSet(Params)
    End Function

    <ActionName("ReOrderSong")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function ReOrderSong(<FromUri()> Params As ReOrderSongParams) As List(Of SetListSong)
        If Params.AuthToken <> System.Configuration.ConfigurationManager.AppSettings("APIToken") Then Throw New Exception("Invalid API Token.")
        Return SetSvc.ReOrderSong(Params)
    End Function


End Class
