Imports System.Reflection


Public Class ObjectFu


    Public Shared Function ObjectMorph(Of T)(pObj As Object) As T
        Dim ResultObj As T = GetType(T).GetConstructor(New System.Type() {}).Invoke(New Object() {})
        Dim ResultProperties = ResultObj.GetType().GetProperties()
        Dim SourceProperties = pObj.GetType().GetProperties()

        If Not (pObj Is Nothing) Then
            For Each objProperty In SourceProperties
                Dim objPropName = objProperty.Name
                Dim objValue = objProperty.GetValue(pObj, Nothing)

                If Not (ResultObj.GetType().GetProperty(objPropName) Is Nothing) Then
                    If ResultObj.GetType().GetProperty(objPropName).PropertyType Is objProperty.PropertyType Then
                        If ResultObj.GetType().GetProperty(objPropName).CanWrite Then
                            If objProperty.GetType().GetProperty("Count") Is Nothing Then
                                ResultObj.GetType().GetProperty(objPropName).SetValue(ResultObj, objValue, Nothing)
                            Else
                                ResultObj.GetType().GetProperty(objPropName).SetValue(ResultObj, ObjectMorphList(Of Object)(objValue), Nothing)
                            End If
                        End If
                    End If
                End If

            Next
        End If

        Return ResultObj

    End Function

    Public Shared Function ObjectMorphList(Of T)(pObjList As IEnumerable(Of Object)) As List(Of T)
        Dim ResultListOfObj As New List(Of T)
        If Not (pObjList Is Nothing) Then
            If pObjList.Count > 0 Then
                For Each SourceObj In pObjList
                    ResultListOfObj.Add(ObjectMorph(Of T)(SourceObj))
                Next
            End If
        End If

        Return ResultListOfObj

    End Function

End Class
