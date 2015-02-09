Namespace ViewModels

    Public Class SetView
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
        Public Property Songs As List(Of SetViewSongs)
        Public Property Team As List(Of SetViewTeam)
    End Class

    Public Class SetViewSongs
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

    Public Class SetViewTeam
        Public Property UserId As Integer
        Public Property Position As Integer
    End Class

End Namespace