Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http
Imports System.Net.Http.Formatting

Public Class WebApiConfig
    Public Shared Sub Register(ByVal config As HttpConfiguration)
        config.Routes.MapHttpRoute( _
           name:="DefaultApi", _
           routeTemplate:="{controller}/{action}", _
           defaults:=New With {.id = RouteParameter.Optional} _
       )
        config.Formatters.Remove(config.Formatters.XmlFormatter)
        config.Formatters.JsonFormatter.MediaTypeMappings.Add(New RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, True, "application/json"))

    End Sub
End Class