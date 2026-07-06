Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsUser
    Dim _clsDB As New clsDatabase

    Dim _clsSecurity As New clsDimboMySQL.clsSecurity
    Dim _clsSystemInfo As New clsSystemInfo

    Public Sub New()
        initialize()
    End Sub

    Public Property oldUserID As String
    Public Property userID As String
    Public Property userName As String
    Public Property userRoleID As String
    Public Property userPassword As String
    Public Property isOfficeUser As String
    Public Property UserOffice As String
    Public Property isActive As String
    Public Property createUser As String


    Public Sub initialize()
        _oldUserID = ""
        _userID = ""
        _userName = ""
        _userRoleID = ""
        _userPassword = ""
        _isOfficeUser = "Y"
        _UserOffice = ""
        _isActive = ""
        _createUser = ""
    End Sub

    Public Function browseUser(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT user_id, user_name, user_role_id, is_office_user, is_active FROM tbl_admin_user_info WHERE user_id LIKE '%" & _criteria & "%' OR user_name LIKE '%" & _criteria & "%' ORDER BY user_name "

        Return _clsDB.Fill_DataTable(sql, "tbl_admin_user_info")
    End Function

    Public Function browseUserSystem(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT user_id, user_name, is_office_user, is_active FROM tbl_admin_user_info WHERE create_user<>'INITIALIZED' AND (user_id LIKE '%" & _criteria & "%' OR user_name LIKE '%" & _criteria & "%') ORDER BY user_id "

        Return _clsDB.Fill_DataTable(sql, "tbl_admin_user_info")
    End Function

    Public Function browseUserPermissionSystem(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT user_id, user_name,is_office_user, is_active FROM tbl_admin_user_info WHERE is_active='Y' AND create_user<>'INITIALIZED' AND (user_id LIKE '%" & _criteria & "%' OR user_name LIKE '%" & _criteria & "%') ORDER BY user_id "

        Return _clsDB.Fill_DataTable(sql, "tbl_admin_user_info")
    End Function

    Public Function browseUserPermissionSystemOfficeUser(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT user_id, user_name,is_office_user, is_active FROM tbl_admin_user_info WHERE is_active='Y' AND create_user<>'INITIALIZED' AND (user_id LIKE '%" & _criteria & "%' OR user_name LIKE '%" & _criteria & "%') ORDER BY user_id "

        Return _clsDB.Fill_DataTable(sql, "tbl_admin_user_info")
    End Function
    Public Sub saveUser()
        If _oldUserID = "" Then
            With _clsDB.dbUtility
                .fieldItems = "user_id,user_name,user_role_id,user_password,is_office_user,user_office,is_active,create_user"
                .sqlString = .getSQLStatement("tbl_admin_user_info", "INSERT")
                .ADDPARAM_CMD_String("user_id", _userID)
                .ADDPARAM_CMD_String("user_name", _userName)
                .ADDPARAM_CMD_String("user_role_id", _userRoleID)
                .ADDPARAM_CMD_String("user_password", _clsSystemInfo.getDefaultPassword)
                .ADDPARAM_CMD_String("is_office_user", _isOfficeUser)
                .ADDPARAM_CMD_String("user_office", _UserOffice)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _createUser)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "user_id,user_name,user_role_id,is_active,create_user"
                .sqlString = .getSQLStatement("tbl_admin_user_info", "UPDATE", "user_id")
                .ADDPARAM_CMD_String("user_id", _userID)
                .ADDPARAM_CMD_String("user_name", _userName)
                .ADDPARAM_CMD_String("user_role_id", _userRoleID)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _createUser)
                .ADDPARAM_CMD_String("user_id2", _oldUserID)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub

    Public Sub saveOfficeUser()
        If _oldUserID = "" Then
            With _clsDB.dbUtility
                .fieldItems = "user_id,user_name,user_role_id,user_password,is_office_user,user_office,is_active,create_user"
                .sqlString = .getSQLStatement("tbl_admin_user_info", "INSERT")
                .ADDPARAM_CMD_String("user_id", _userID)
                .ADDPARAM_CMD_String("user_name", _userName)
                .ADDPARAM_CMD_String("user_role_id", _userRoleID)
                .ADDPARAM_CMD_String("user_password", _clsSystemInfo.getDefaultPassword)
                .ADDPARAM_CMD_String("is_office_user", _isOfficeUser)
                .ADDPARAM_CMD_String("user_office", _UserOffice)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _createUser)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "user_id,user_name,user_office,is_active,create_user"
                .sqlString = .getSQLStatement("tbl_admin_user_info", "UPDATE", "user_id")
                .ADDPARAM_CMD_String("user_id", _userID)
                .ADDPARAM_CMD_String("user_name", _userName)
                .ADDPARAM_CMD_String("user_office", _UserOffice)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _createUser)
                .ADDPARAM_CMD_String("user_id2", _oldUserID)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub

    Public Sub getUserInformation(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_admin_user_info WHERE user_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _userID = dt.Rows(0)("user_id").ToString
            _userName = dt.Rows(0)("user_name").ToString
            _userRoleID = dt.Rows(0)("user_role_id").ToString
            _userPassword = dt.Rows(0)("user_password").ToString
            _isOfficeUser = dt.Rows(0)("is_office_user").ToString
            _UserOffice = dt.Rows(0)("user_office").ToString
            _isActive = dt.Rows(0)("is_active").ToString
            _createUser = dt.Rows(0)("create_user").ToString
        Else
            initialize()
        End If
    End Sub

    Public Function validateUserLogon(ByVal _struserid As String, ByVal _strpassword As String) As Boolean
        getUserInformation(_struserid)
        Return _clsSecurity.validateSameText(_strpassword, userPassword)
    End Function

    Public Function verifyDefaultPassword(ByVal _strpassword As String) As Boolean
        Dim _clsSystemInfo As New clsSystemInfo
        Return _clsSecurity.validateSameText(_strpassword, _clsSystemInfo.getDefaultPassword)
    End Function

    Public Sub updatePassword()
        With _clsDB.dbUtility
            .fieldItems = "user_password"
            .sqlString = .getSQLStatement("tbl_admin_user_info", "UPDATE", "user_id")
            .ADDPARAM_CMD_String("user_password", _clsSecurity.encryptString(_userPassword))
            .ADDPARAM_CMD_String("user_id", _userID)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub resetPassword()
        With _clsDB.dbUtility
            .fieldItems = "user_password"
            .sqlString = .getSQLStatement("tbl_admin_user_info", "UPDATE", "user_id")
            .ADDPARAM_CMD_String("user_password", _clsSystemInfo.getDefaultPassword)
            .ADDPARAM_CMD_String("user_id", _userID)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Function userExists(ByVal _userID As String) As Boolean
        Return _clsDB.Get_DB_Item("SELECT count(user_id) FROM tbl_admin_user_info WHERE user_id='" & _userID & "'") > 0
    End Function


End Class
