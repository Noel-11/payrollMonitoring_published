Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsSystemInfo
   Dim _clsDB As New clsDatabase

    Private _defaultDescription As String
    Private _defaultValue As String


    Public WriteOnly Property defaultDescription As String
        Set(ByVal Value As String)
            Me._defaultDescription = Value
        End Set
    End Property

    Public WriteOnly Property defaultValue As String
        Set(ByVal Value As String)
            Me._defaultValue = Value
        End Set
    End Property

    Public Sub saveSystemInfo()
        With _clsDB.dbUtility
            .fieldItems = "default_desc,default_value"
            .sqlString = .getSQLStatement("tbl_admin_system_default", "INSERT")
            .ADDPARAM_CMD_String("default_desc", _defaultDescription)
            .ADDPARAM_CMD_String("default_value", _defaultValue)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Function getDefaultPassword() As String
        Return _clsDB.Get_DB_Item("SELECT default_value FROM tbl_admin_system_default WHERE default_desc='default password'")
    End Function
End Class
