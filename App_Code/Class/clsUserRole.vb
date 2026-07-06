Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsUserRole
    Dim _clsDB As New clsDatabase


    Public Sub New()
        initialize()
    End Sub


    Public Property userRoleID As String
       
    Public Property userRoleName As String
       
    Public Property userRoleDescription As String
    

    Public Property isActive As String
      

    Public Property lastUser As String
       

    Public Sub initialize()
        _userRoleID = ""
        _userRoleName = ""
        _userRoleDescription = ""
        _isActive = ""
        _lastUser = ""
    End Sub

    Public Function browseUserRole(ByVal _criteria As String, Optional ByVal browse_type As String = "BROWSE") As DataTable
        Dim sql As String = ""
        sql = "SELECT user_role_id, user_role_name, user_role_description,is_active FROM tbl_admin_user_role"
        Select Case browse_type
            Case "BROWSE"
                sql = sql & " WHERE user_role_id<>1 AND (user_role_name LIKE '%" & _criteria & "%' OR user_role_description LIKE '%" & _criteria & "%') ORDER BY user_role_name "
        End Select
        Return _clsDB.Fill_DataTable(sql, "tbl_admin_user_role")
    End Function

    Public Sub saveUserRole()
        If _userRoleID = "" Then
            With _clsDB.dbUtility
                .fieldItems = "user_role_name,user_role_description,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_admin_user_role", "INSERT")
                .ADDPARAM_CMD_String("user_role_name", _userRoleName)
                .ADDPARAM_CMD_String("user_role_description", _userRoleDescription)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "user_role_name,user_role_description,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_admin_user_role", "UPDATE", "user_role_id")
                .ADDPARAM_CMD_String("user_role_name", _userRoleName)
                .ADDPARAM_CMD_String("user_role_description", _userRoleDescription)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .ADDPARAM_CMD_String("user_role_id", _userRoleID)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub

    Public Sub getUserRole(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_admin_user_role WHERE user_role_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _userRoleID = dt.Rows(0)("user_role_id")
            _userRoleName = dt.Rows(0)("user_role_name")
            _userRoleDescription = dt.Rows(0)("user_role_description")
            _isActive = dt.Rows(0)("is_active")
            _lastUser = dt.Rows(0)("last_user")
        Else
            initialize()
        End If
    End Sub


   
End Class
