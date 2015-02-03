Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Entity
Imports System.Reflection


Public Class dbUtil
    Inherits DbContext

    Public Sub New(pConnName As String)
        MyBase.New(pConnName)
    End Sub

    Public Function Query(Of T)(pSQL As String, ByVal ParamArray pParamArray() As Object) As List(Of T)
        Dim objDt As New DataTable
        Dim objSda As New SqlClient.SqlDataAdapter
        Dim objCmd As SqlClient.SqlCommand = MyBase.Database.Connection.CreateCommand

        Try
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = pSQL
            If Not (pParamArray Is Nothing) Then
                objCmd.Parameters.AddRange(ConvertParams(pParamArray))
            End If

            objSda.SelectCommand = objCmd
            objSda.Fill(objDt)

            Dim tmp As List(Of T) = GetType(List(Of T)).GetConstructor(New System.Type() {}).Invoke(New Object() {})
            BindData(Of T)(tmp, objDt)

            Query = tmp

        Catch ex As Exception
            Query = Nothing
        Finally
            objSda.Dispose()
            objCmd.Dispose()
            objDt.Dispose()
        End Try

    End Function

    Public Function QuerySingle(Of T)(pSQL As String, ByVal ParamArray pParamArray() As Object) As T

        Dim objDt As New DataTable
        Dim objSda As New SqlClient.SqlDataAdapter
        Dim objCmd As SqlClient.SqlCommand = MyBase.Database.Connection.CreateCommand

        Try
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = pSQL
            If Not (pParamArray Is Nothing) Then
                objCmd.Parameters.AddRange(ConvertParams(pParamArray))
            End If

            objSda.SelectCommand = objCmd
            objSda.Fill(objDt)

            Dim tmp As T = GetType(T).GetConstructor(New System.Type() {}).Invoke(New Object() {})
            BindData(Of T)(tmp, objDt)

            QuerySingle = tmp

        Catch ex As Exception
            QuerySingle = Nothing
        Finally
            objSda.Dispose()
            objCmd.Dispose()
            objDt.Dispose()
        End Try


    End Function

    Public Function QueryScalar(Of T)(pSQL As String, ByVal ParamArray pParamArray() As Object) As T

        Dim objCmd As SqlClient.SqlCommand = MyBase.Database.Connection.CreateCommand
        If objCmd.Connection.State = ConnectionState.Closed Then objCmd.Connection.Open()

        Try
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = pSQL
            If Not (pParamArray Is Nothing) Then
                objCmd.Parameters.AddRange(ConvertParams(pParamArray))
            End If
            Dim tmp As T
            tmp = objCmd.ExecuteScalar()

            QueryScalar = tmp

        Catch ex As Exception
            QueryScalar = Nothing
        Finally
            objCmd.Dispose()
        End Try

    End Function

    Public Function ExecuteWithoutIdentity(pSql As String, ByVal ParamArray pParamArray() As Object) As Integer
        Dim objCmd As SqlClient.SqlCommand = MyBase.Database.Connection.CreateCommand

        Try
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = pSql
            If Not (pParamArray Is Nothing) Then
                objCmd.Parameters.AddRange(ConvertParams(pParamArray))
            End If

            ExecuteWithoutIdentity = objCmd.ExecuteNonQuery()

        Catch ex As Exception
            ExecuteWithoutIdentity = 0
        Finally
            objCmd.Dispose()
        End Try
    End Function

    Public Function ExecuteWithIdentity(pSql As String, ByVal ParamArray pParamArray() As Object) As Object
        Dim objCmd As SqlClient.SqlCommand = MyBase.Database.Connection.CreateCommand

        Try
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = pSql & "; SELECT SCOPE_IDENTITY()"
            If Not (pParamArray Is Nothing) Then
                objCmd.Parameters.AddRange(ConvertParams(pParamArray))
            End If

            ExecuteWithIdentity = objCmd.ExecuteScalar()

        Catch ex As Exception
            ExecuteWithIdentity = 0
        Finally
            objCmd.Dispose()
        End Try
    End Function

    Private Function ConvertParams(pParamArray() As Object) As SqlClient.SqlParameter()
        Dim Params As New List(Of SqlClient.SqlParameter)
        Dim objParam As Object
        Dim intCount = 0

        For Each objParam In pParamArray
            Params.Add(New SqlClient.SqlParameter("@P" & intCount & "", objParam))
            intCount += 1
        Next

        Return Params.ToArray()

    End Function

    Private Sub BindData(Of T)(ByRef pObj As Object, pobjDt As DataTable)
        Dim obj As T = GetType(T).GetConstructor(New System.Type() {}).Invoke(New Object() {})
        Dim objPropertyInfoColl = obj.GetType().GetProperties()

        Dim objDR As DataRow
        For Each objDR In pobjDt.Rows

            Dim tmpObj = GetType(T).GetConstructor(New System.Type() {}).Invoke(New Object() {})
            For Each objProperty In objPropertyInfoColl
                Dim objPropName = objProperty.Name
                If objDR.Table.Columns.Contains(objPropName) Then
                    Dim objPropDbValue = objDR.Item(objPropName)
                    If Not (IsDBNull(objDR.Item(objPropName))) Then tmpObj.GetType().GetProperty(objPropName).SetValue(tmpObj, objPropDbValue, Nothing)
                End If
            Next
            If pObj.GetType().GetProperty("Count") Is Nothing Then
                pObj = tmpObj
            Else
                Dim objArgs(0) As Object
                objArgs(0) = tmpObj
                pObj.GetType().InvokeMember("Add", BindingFlags.DeclaredOnly Or BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.InvokeMethod, Nothing, pObj, objArgs)
            End If

        Next

    End Sub

End Class
