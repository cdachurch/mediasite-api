Namespace ViewModels

    Public Class JQGridView
        Public Property page As Integer
        Public Property total As Integer
        Public Property records As Integer
        Public Property rows As List(Of JQGridRow)
        Public Property userdata As Object
    End Class

    Public Class JQGridRow
        Public Property id As String
        Public Property cell As List(Of String)
    End Class

    Public Class MediaSiteGridRow
        Public Property id As Integer
        Public Property title As String
        Public Property author1 As String
        Public Property author2 As String
        Public Property copyDate As String
        Public Property publisher As String
        Public Property urlLink As String
        Public Property notes As String
        Public Property youTubeLink As String
        Public Property songOrder As String
        'Public Property SongEncoding As Integer
        'Public Property SongData As Object
        Public Property mp3Link As String
        Public Property songKey As String
        Public Property ccli As String
        Public Property use1 As String
        Public Property use2 As String
        Public Property tempo As String
        Public Property style As String
        Public Property reference As String
        Public Property fileLink As String
        Public Property fontSize As Integer
        Public Property sheetLayout As Integer
        Public Property createdDate As DateTime?
        Public Property lastModifiedDate As DateTime?
        Public Property deletedDate As DateTime?
    End Class

End Namespace