Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Globalization
Imports System.Data.Entity

Namespace Models

    <Table("Bookmarks")> _
    Public Class Bookmarks
        Public Property songid As Integer
        Public Property userid As Integer
    End Class

    <Table("Groups")> _
    Public Class Groups
        <Key()> _
        <DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)> _
        Public Property Id As Integer
        Public Property Name As String
        Public Property Description As String
    End Class

    <Table("Songs")> _
    Public Class Song
        <Key()> _
        <DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)> _
        Public Property Id As Integer
        Public Property Title As String
        Public Property Author1 As String
        Public Property Author2 As String
        Public Property CopyDate As String
        Public Property Publisher As String
        Public Property UrlLink As String
        Public Property Notes As String
        Public Property YouTubeLink As String
        Public Property SongOrder As String
        Public Property SongEncoding As Integer
        Public Property SongData As Object
        Public Property Mp3Link As String
        Public Property SongKey As String
        Public Property CCLI As Long
        Public Property Use1 As String
        Public Property Use2 As String
        Public Property Tempo As String
        Public Property Style As String
        Public Property Reference As String
        Public Property FileLink As String
        Public Property FontSize As Integer
        Public Property SheetLayout As Integer
        Public Property CreatedDate As DateTime?
        Public Property LastModifiedDate As DateTime?
        Public Property DeletedDate As DateTime?        
    End Class

 


End Namespace
