Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Globalization
Imports System.Data.Entity
Imports mediaSite3.Models
Imports mediaSite3.Utility

Public Class SetRepo
    Inherits dbUtil

    Public Sub New()
        MyBase.New("DevelopmentConnection")
    End Sub

    Public Function GetSet() As Object

    End Function

    Public Function SaveSet() As Object

    End Function

    Public Function DeleteSet() As Object

    End Function

    Public Function PublishSet() As Object

    End Function

    Public Function AddSongToSet() As Object

    End Function

    Public Function RemoveSongFromSet() As Object

    End Function

    Public Function ReOrderSong() As Object

    End Function

End Class
