Imports mediaSite3.ViewModels
Imports mediaSite3.Models
Imports mediaSite3.Repositories
Imports mediaSite3.Params

Namespace Services
    Public Class SongsService
        Implements IDisposable

        Private _songRepo As New SongRepo

        Public Function GetSongCount()
            Return _songRepo.SongCount()
        End Function

        Public Function BrowseSongs(Params As BrowseSongsParams) As List(Of Song)
            Return _songRepo.BrowseSongs(Params)
        End Function

        Public Function GetSong(id As Integer) As Song
            Dim objSong As Song
            objSong = _songRepo.SongById(id)

            Dim objMediaCodec As New MediaCodec(Nothing, id, objSong.Title, objSong.SongKey)
            objSong.SongData = objMediaCodec.Decode(objSong.SongData, objSong.SongEncoding)
            objMediaCodec = Nothing

            Return objSong
        End Function

        Public Function SearchSong(Params As SearchSongsParams, ByRef pCount As Long) As List(Of Song)
            Return _songRepo.SearchSong(Params, pCount)
        End Function

        Public Function SaveSong(SongData As Song) As String
            Return ""
        End Function

        Public Function DeleteSong(id As Integer) As String
            Return ""
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    _songRepo.Dispose()
                End If

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