Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.IO

<TestClass()> Public Class SongsTests

    <TestMethod()> Public Sub TestMethod1()

        Dim objAWS As New Gateways.AWS()
        Dim output = objAWS.DownloadFile("00000173.dat")
        Dim reader As New StreamReader(output)
        Dim value = reader.ReadToEnd
        Console.Write(value)

    End Sub

End Class