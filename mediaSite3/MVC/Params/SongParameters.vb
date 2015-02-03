Imports mediaSite3.Models

Namespace Params

    Public Class SongView
        Public Property id As Integer
        Public Property Title As String
        Public Property Author1 As String
        Public Property Author2 As String
    End Class

    Public Class BrowseSongsParams
        Inherits baseParams
        Public Property page As Integer
        Public Property rows As Integer
        Public Property sidx As String
        Public Property sord As String
        Public Property jqGridID As String
        Public Sub New()
            page = 1
            rows = 25
            sidx = "Title"
            sord = "ASC"
            jqGridID = "jqGrid"
        End Sub
    End Class

    Public Class SearchSongsParams
        Inherits baseParams
        Public Property jqGridID As String
        Public Property _search As Boolean
        Public Property nd As String
        Public Property rows As Integer
        Public Property page As Integer
        Public Property sidx As String
        Public Property sord As String
        Public Property searchField As String
        Public Property searchText As String
        Public Property searchOper As String
        Public Property searchFilter As Integer
        Public Property filters As String


        Public Sub New()
            jqGridID = "jqGrid"
            _search = True
            rows = 25
            page = 1
            sidx = "Title"
            sord = "ASC"
            searchField = "Title"
            searchText = ""
            searchOper = "eq"
            filters = ""
        End Sub
    End Class

    Public Class GetSongParams
        Inherits baseParams
        Public Property id As Integer
    End Class

    Public Class SaveSongParams
        Inherits baseParams
        Public Property songData As Song
    End Class

    Public Class DeleteSongParams
        Inherits baseParams
        Public Property id As Integer
    End Class


End Namespace