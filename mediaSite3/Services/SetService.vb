Imports mediaSite3.ViewModels
Imports mediaSite3.Models
Imports mediaSite3.Repositories
Imports mediaSite3.Params

Namespace Services

    Public Class SetService
        Implements IDisposable

        Private _songRepo As New SongRepo
        Private _setRepo As New SetRepo
        Private _userRepo As New UserRepo

        Public Function GetSets(Params As GetSetsParams) As List(Of SetList)

            'Get List of Sets
            Return _setRepo.GetSets(Params)

        End Function

        Public Function GetSet(Params As GetSetParams) As SetList

            'Get Set
            Dim setObj = _setRepo.GetSet(Params)

            'Get Songs for Set
            setObj.Songs = _setRepo.GetSetSongs(Params)

            'Get Team members for Set
            setObj.TeamMembers = _setRepo.GetSetMembers(Params)

            Return setObj

        End Function

        Public Function SaveSet(Params As SaveSetParams) As ActionResult

            'Update Set/Songs/TeamMember Tables


        End Function

        Public Function DeleteSet(Params As DeleteSetParams) As ActionResult

            ''Delete Set 

        End Function

        Public Function PublishSet(Params As PublishSetParams) As ActionResult

        End Function

        Public Function AddSongToSet(Params As AddSongToSetParams) As List(Of SetListSong)

        End Function

        Public Function RemoveSongFromSet(Params As RemoveSongFromSetParams) As List(Of SetListSong)

        End Function

        Public Function ReOrderSong(Params As ReOrderSongParams) As List(Of SetListSong)

        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
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
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace
