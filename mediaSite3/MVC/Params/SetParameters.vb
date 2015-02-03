Imports mediaSite3.Params

Namespace ViewModels

    Public Class GetSetsParams
        Inherits baseParams
        Public Property setId As Integer
        Public Property leaderFilter As String
        Public Property dateFrom As Date
        Public Property dateTo As Date
        Public Property order As String
    End Class

    Public Class GetSetParams
        Inherits baseParams
        Public Property setId As Integer
    End Class

    Public Class SaveSetParams
        Inherits baseParams

    End Class

    Public Class DeleteSetParams
        Inherits baseParams
        Public Property setId As Integer
    End Class

    Public Class PublishSetParams
        Inherits baseParams
        Public Property setId As Integer
    End Class

    Public Class AddSongToSetParams
        Inherits baseParams
        Public Property setId As Integer
        Public Property songId As Integer
        Public Property orderNo As Integer
        Public Property key As String
        Public Property fontSize As Integer
    End Class

    Public Class RemoveSongFromSetParams
        Inherits baseParams
        Public Property setId As Integer
        Public Property songId As Integer
    End Class

    Public Class ReOrderSongParams
        Inherits baseParams
        Public Property setId As Integer
        Public Property songList As New List(Of ReOrderSongParams_SongList)
    End Class

    Public Class ReOrderSongParams_SongList
        Public Property songId As Integer
        Public Property orderNo As Integer
    End Class
End Namespace