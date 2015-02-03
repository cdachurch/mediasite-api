Imports System.Net
Imports System.Web.Http
Imports mediaSite3.ViewModels

Public Class SetController
    Inherits ApiController

    <ActionName("GetSets")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function GetSets(<FromUri()> Params As GetSetsParams) As Object

    End Function

    <ActionName("GetSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function GetSet(<FromUri()> Params As GetSetParams) As Object

    End Function

    <ActionName("SaveSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function SaveSet(<FromUri()> Params As SaveSetParams) As Object

    End Function

    <ActionName("DeleteSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function DeleteSet(<FromUri()> Params As DeleteSetParams) As Object

    End Function

    <ActionName("PublishSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function PublishSet(<FromUri()> Params As PublishSetParams) As Object

    End Function

    <ActionName("AddSongToSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function AddSongToSet(<FromUri()> Params As AddSongToSetParams) As Object

    End Function

    <ActionName("RemoveSongFromSet")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function RemoveSongFromSet(<FromUri()> Params As RemoveSongFromSetParams) As Object

    End Function

    <ActionName("ReOrderSong")> _
    <HttpGet()> _
    <WebPermission(System.Security.Permissions.SecurityAction.Demand)> _
    Public Function ReOrderSong(<FromUri()> Params As ReOrderSongParams) As Object

    End Function


End Class
