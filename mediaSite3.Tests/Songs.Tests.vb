Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class SongsTests

    <TestMethod()> Public Sub TestMethod1()

        Dim objAWS As New Gateways.AWS()
        Dim output = objAWS.DownloadFile("00004520.dat")

        Console.Write(output)

    End Sub

End Class