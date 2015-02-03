Imports Newtonsoft.Json

Public Class JsonNetResult
    Inherits ActionResult

    Public Property ContentEncoding As Encoding
    Public Property ContentType As String
    Public Data As Object
    Public SerializerSettings As JsonSerializerSettings
    Public Formatting As Formatting

    Public Sub New()
        SerializerSettings = New JsonSerializerSettings
    End Sub

    Public Overrides Sub ExecuteResult(context As ControllerContext)
        If context Is Nothing Then
            Throw New ArgumentNullException("context")
        End If

        Dim response As HttpResponseBase = context.HttpContext.Response

        If Not (String.IsNullOrEmpty(ContentType)) Then
            response.ContentType = ContentType
        Else
            response.ContentType = "application/json"
        End If

        If Not (ContentEncoding Is Nothing) Then response.ContentEncoding = ContentEncoding

        If Not (Data Is Nothing) Then
            Dim writer As JsonTextWriter = New JsonTextWriter(response.Output) With {.Formatting = Formatting}
            Dim serializer As JsonSerializer = JsonSerializer.Create(SerializerSettings)
            serializer.Serialize(writer, Data)
            writer.Flush()
        End If

    End Sub

End Class
