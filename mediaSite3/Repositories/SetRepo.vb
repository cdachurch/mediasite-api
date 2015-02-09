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

    End Function

    Public Function GetSet(Params As GetSetParams) As SetView

    End Function

    Public Function SaveSet(Params As SaveSetParams) As ActionResult

    End Function

    Public Function DeleteSet(Params As DeleteSetParams) As ActionResult

    End Function

    Public Function PublishSet(Params As PublishSetParams) As ActionResult

    End Function

    Public Function AddSongToSet(Params As AddSongToSetParams) As List(Of SetListSongs)

    End Function

    Public Function RemoveSongFromSet(Params As RemoveSongFromSetParams) As List(Of SetListSongs)

    End Function

    Public Function ReOrderSong(Params As ReOrderSongParams) As List(Of SetListSongs)

    End Function


End Class
