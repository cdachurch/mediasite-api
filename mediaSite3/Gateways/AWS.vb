Imports Amazon.S3
Imports Amazon.S3.Model

Namespace Gateways

    Public Class AWS
        Implements IDisposable

        Private bucketName As String
        Private accessKey As String
        Private secretKey As String
        Private client As IAmazonS3
        Private responseList As ListBucketsResponse
        Private bucket As S3Bucket

        Public Sub New()

            bucketName = System.Configuration.ConfigurationManager.AppSettings("APIToken")
            accessKey = System.Configuration.ConfigurationManager.AppSettings("APIToken")
            secretKey = System.Configuration.ConfigurationManager.AppSettings("APIToken")
            client = New Amazon.S3.AmazonS3Client(accessKey, secretKey)

        End Sub

        Public Function UploadFile()

        End Function

        Public Function DownloadFile()

        End Function

        Public Function DeleteFile()

        End Function

        Public Function ReplaceFile()

        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                client.Dispose()

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace