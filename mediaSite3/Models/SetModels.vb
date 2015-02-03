Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Globalization
Imports System.Data.Entity

Namespace Models
     
    <Table("Set")> _
    Public Class [Set]
        <Key()> _
        <DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)> _
        Public Property SetId As Integer
        Public Property SermonTitle As String
        Public Property CityMessagePreText As String
        Public Property CityMessagePostText As String
        Public Property PerformanceDate As DateTime?
        Public Property PracticeDate As DateTime?
        Public Property LeaderID As Integer
        Public Property DateCreated As DateTime
        Public Property DatePublished As DateTime?
        Public Property DateDeleted As DateTime?
    End Class

    <Table("SetListSongs")> _
    Public Class SetListSongs
        Public Property SetId As Integer
        Public Property SongId As Integer
        Public Property Title As String
        Public Property Author1 As String
        Public Property Author2 As String
        Public Property SetOrder As Integer
        Public Property PrintNotes As Integer
        Public Property TransposeKey As String
        Public Property FontSize As Integer?
        Public Property LayoutType As Integer
    End Class

    <Table("SetListTeamMembers")> _
    Public Class SetListTeamMembers
        Public Property SetId As Integer
        Public Property UserId As Integer
        Public Property Position As Integer
    End Class


End Namespace