Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Globalization
Imports System.Data.Entity
Imports mediaSite3.Models
Imports mediaSite3.Params
Imports mediaSite3.ViewModels

Public Class SetRepo
    Inherits dbUtil

    Public Sub New()
        MyBase.New("DevelopmentConnection")
    End Sub

    Public Function GetSets(Params As GetSetsParams) As List(Of SetList)
        Const sql = "SELECT * FROM SetList"
        Return Query(Of SetList)(sql, Nothing)
    End Function

    Public Function GetSet(Params As GetSetParams) As SetList
        Const sql = "SELECT * FROM SetList WHERE SetId = @P0"
        Return QuerySingle(Of SetList)(sql, Params.setId)
    End Function

    Public Function GetSetSongs(Params As GetSetParams) As List(Of SetListSong)
        Const sql = "SELECT * FROM SetListSongs WHERE SetId = @P0 ORDER BY SetOrder ASC"
        Return Query(Of SetListSong)(sql, Params.setId)
    End Function

    Public Function GetSetMembers(Params As GetSetParams) As List(Of SetListTeamMember)
        Const sql = "SELECT * FROM SetListTeamMembers WHERE SetId = @P0"
        Return Query(Of SetListTeamMember)(sql, Params.setId)
    End Function

    Public Function SaveSet(Params As SaveSetParams) As ActionResult

    End Function

    Public Function DeleteSet(Params As DeleteSetParams) As ActionResult

    End Function

    Public Function PublishSet(Params As PublishSetParams) As ActionResult

    End Function

    Public Function AddSongToSet(Params As AddSongToSetParams) As List(Of SetListSong)

    End Function

    Public Function RemoveSongFromSet(Params As RemoveSongFromSetParams) As List(Of SetListSong)

    End Function

    Public Function ReOrderSong(Params As ReOrderSongParams) As List(Of SetListSong)

    End Function


End Class
