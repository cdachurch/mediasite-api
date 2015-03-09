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

            bucketName = System.Configuration.ConfigurationManager.AppSettings("AWSbucketname")
            accessKey = System.Configuration.ConfigurationManager.AppSettings("AWSAccessKey")
            secretKey = System.Configuration.ConfigurationManager.AppSettings("AWSSecretKey")
            client = New Amazon.S3.AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.USWest2)

        End Sub

        Public Function UploadFile(pFileName As String, pFileStream As System.IO.Stream) As System.Net.HttpStatusCode

            If pFileName.Length > 0 Then
                Using client
                    Dim putObjectRequest As New PutObjectRequest() With {.BucketName = bucketName,
                                                                         .CannedACL = S3CannedACL.Private,
                                                                         .Key = pFileName,
                                                                         .InputStream = pFileStream}
                    UploadFile = client.PutObject(putObjectRequest).HttpStatusCode
                End Using
            Else
                Return Net.HttpStatusCode.NoContent
            End If

        End Function

        Public Function DownloadFile(pFileName As String) As System.IO.MemoryStream


            Using client
                Dim getObjectRequest As New GetObjectRequest() With {.BucketName = bucketName, .Key = pFileName}
                Using response As GetObjectResponse = client.GetObject(getObjectRequest)
                    Using responseStream As System.IO.Stream = response.ResponseStream

                        Dim objCrypto As New Crypto()
                        Dim decryptedResponseStream = objCrypto.EncryptDecryptStream(responseStream, Crypto.CryptoAction.actionDecrypt)
                        decryptedResponseStream.Position = 0
                        Return decryptedResponseStream
                    End Using
                End Using
            End Using

        End Function

        Public Function DeleteFile(pFileName As String) As System.Net.HttpStatusCode

            Using client
                Dim deleteObjectRequest = New DeleteObjectRequest() With {.BucketName = bucketName, .Key = pFileName}
                Return client.DeleteObject(deleteObjectRequest).HttpStatusCode
            End Using

        End Function

        Public Function FileExists(pFileName As String) As Boolean

            Dim s3FileInfo As New Amazon.S3.IO.S3FileInfo(client, bucketName, pFileName)
            Return s3FileInfo.Exists

        End Function

        Public Function ReplaceFile(pFileName As String, pFileStream As System.IO.Stream) As System.Net.HttpStatusCode

            DeleteFile(pFileName)

            Return UploadFile(pFileName, pFileStream)

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