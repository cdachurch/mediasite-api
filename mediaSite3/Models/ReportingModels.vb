Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Globalization
Imports System.Data.Entity

Namespace Models

    <Table("Activity Codes")> _
    Public Class ActivityCodes
        <Key()> _
        <DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)> _
        Public Property ActivityID As Integer
        Public Property Activity As String        
    End Class

    <Table("ActivityLog")> _
    Public Class ActivityLog
        <Key()> _
        <DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)> _
        Public Property DTStamp As DateTime
        Public Property UID As Integer
        Public Property ActionCode As Integer
        Public Property SongID As Integer
        Public Property SessionID As String
    End Class

End Namespace
