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

End Namespace