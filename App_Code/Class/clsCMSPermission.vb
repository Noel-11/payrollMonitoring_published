Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsCMSPermission
     Dim _clsDB As New clsDatabase
    Dim dimboUtil As New clsDimboMySQL.clsUtilities

    Dim _strQueryBase As String = ""
    Dim _strQuery As String = ""

    Private _accessPermissionID As String
    Private _accessUserRoleID As String
    Private _accessUserID As String
    Private _accessMenuID As String
    Private _accessPageURL As String
    Private _canAccess As String
    Private _canCreate As String
    Private _canUpdate As String
    Private _canDelete As String
    Private _canReport As String
    Private _canExport As String
    Private _lastUser As String

    Public Property accessPermissionID As String
        Get
            Return Me._accessPermissionID
        End Get
        Set(ByVal Value As String)
            Me._accessPermissionID = Value
        End Set
    End Property

    Public Property accessUserRoleID As String
        Get
            Return Me._accessUserRoleID
        End Get
        Set(ByVal Value As String)
            Me._accessUserRoleID = Value
        End Set
    End Property

    Public Property accessUserID As String
        Get
            Return Me._accessUserID
        End Get
        Set(ByVal Value As String)
            Me._accessUserID = Value
        End Set
    End Property

    Public Property accessMenuID As String
        Get
            Return Me._accessMenuID
        End Get
        Set(ByVal Value As String)
            Me._accessMenuID = Value
        End Set
    End Property

    Public Property accessPageURL As String
        Get
            Return Me._accessPageURL
        End Get
        Set(ByVal Value As String)
            Me._accessPageURL = Value
        End Set
    End Property

    Public Property canAccess As String
        Get
            Return Me._canAccess
        End Get
        Set(ByVal Value As String)
            Me._canAccess = Value
        End Set
    End Property

    Public Property canCreate As String
        Get
            Return Me._canCreate
        End Get
        Set(ByVal Value As String)
            Me._canCreate = Value
        End Set
    End Property

    Public Property canUpdate As String
        Get
            Return Me._canUpdate
        End Get
        Set(ByVal Value As String)
            Me._canUpdate = Value
        End Set
    End Property

    Public Property canDelete As String
        Get
            Return Me._canDelete
        End Get
        Set(ByVal Value As String)
            Me._canDelete = Value
        End Set
    End Property

    Public Property canReport As String
        Get
            Return Me._canReport
        End Get
        Set(ByVal Value As String)
            Me._canReport = Value
        End Set
    End Property

    Public Property canExport As String
        Get
            Return Me._canExport
        End Get
        Set(ByVal Value As String)
            Me._canExport = Value
        End Set
    End Property

    Public Property lastUser As String
        Get
            Return Me._lastUser
        End Get
        Set(ByVal Value As String)
            Me._lastUser = Value
        End Set
    End Property

    Public Sub saveUserPermission()

        With _clsDB.dbUtility
            .fieldItems = "user_role_id,user_id,menu_id,page_url,can_access,can_create,can_update,can_delete,can_report,can_export,last_user,last_date"
            .sqlString = .getSQLStatement("tbl_admin_user_access_permission", "INSERT")
            .ADDPARAM_CMD_String("user_role_id", _accessUserRoleID)
            .ADDPARAM_CMD_String("user_id", _accessUserID)
            .ADDPARAM_CMD_String("menu_id", _accessMenuID)
            .ADDPARAM_CMD_String("page_url", _accessPageURL)
            .ADDPARAM_CMD_String("can_access", _canAccess)
            .ADDPARAM_CMD_String("can_create", _canCreate)
            .ADDPARAM_CMD_String("can_update", _canUpdate)
            .ADDPARAM_CMD_String("can_delete", _canDelete)
            .ADDPARAM_CMD_String("can_report", _canReport)
            .ADDPARAM_CMD_String("can_export", _canExport)
            .ADDPARAM_CMD_String("last_user", _lastUser)
            .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Function fnModuleUserPermissionParent(ByVal _struserid As String) As DataTable
        Dim _strQuery As String = ""
        _strQuery = "SELECT tbl_admin_cms_menu.menu_id, tbl_admin_cms_menu.menu_name, " & _
                    "CASE WHEN can_access='Y' THEN 'True' ELSE 'False' END as can_access, " & _
                    "CASE WHEN can_create='Y' THEN 'True' ELSE 'False' END as can_create, " & _
                    "CASE WHEN can_update='Y' THEN 'True' ELSE 'False' END as can_update, " & _
                    "CASE WHEN can_delete='Y' THEN 'True' ELSE 'False' END as can_delete, " & _
                    "CASE WHEN can_report='Y' THEN 'True' ELSE 'False' END as can_report, " & _
                    "CASE WHEN can_export='Y' THEN 'True' ELSE 'False' END as can_export " & _
                    "FROM tbl_admin_cms_menu LEFT OUTER JOIN tbl_admin_user_access_permission ON tbl_admin_cms_menu.menu_id = tbl_admin_user_access_permission.menu_id WHERE tbl_admin_cms_menu.parent_id = '0' AND tbl_admin_user_access_permission.user_id ='" & _struserid & "' " & _
                    "ORDER BY tbl_admin_cms_menu.sort_order"

        Return _clsDB.Fill_DataTable(_strQuery)
    End Function

    Public Function fnModuleUserPermissionParentCTO(ByVal _struserid As String) As DataTable
        Dim _strQuery As String = ""
        _strQuery = "SELECT tbl_admin_cms_menu.menu_id, tbl_admin_cms_menu.menu_name, " & _
                    "CASE WHEN can_access='Y' THEN 'True' ELSE 'False' END as can_access, " & _
                    "CASE WHEN can_create='Y' THEN 'True' ELSE 'False' END as can_create, " & _
                    "CASE WHEN can_update='Y' THEN 'True' ELSE 'False' END as can_update, " & _
                    "CASE WHEN can_delete='Y' THEN 'True' ELSE 'False' END as can_delete, " & _
                    "CASE WHEN can_report='Y' THEN 'True' ELSE 'False' END as can_report, " & _
                    "CASE WHEN can_export='Y' THEN 'True' ELSE 'False' END as can_export " & _
                    "FROM tbl_admin_cms_menu LEFT OUTER JOIN tbl_admin_user_access_permission ON tbl_admin_cms_menu.menu_id = tbl_admin_user_access_permission.menu_id WHERE tbl_admin_cms_menu.menu_id <> '1' AND tbl_admin_cms_menu.parent_id = '0' AND tbl_admin_user_access_permission.user_id ='" & _struserid & "' " & _
                    "ORDER BY tbl_admin_cms_menu.sort_order"

        Return _clsDB.Fill_DataTable(_strQuery)
    End Function

    Public Function fnModuleUserPermissionChild(ByVal _strparent As String, ByVal _struserid As String) As DataTable
        Dim _strQuery As String = ""
        _strQuery = "SELECT tbl_admin_cms_menu.menu_id, tbl_admin_cms_menu.menu_name, " & _
                    "CASE WHEN can_access='Y' THEN 'True' ELSE 'False' END as can_access, " & _
                    "CASE WHEN can_create='Y' THEN 'True' ELSE 'False' END as can_create, " & _
                    "CASE WHEN can_update='Y' THEN 'True' ELSE 'False' END as can_update, " & _
                    "CASE WHEN can_delete='Y' THEN 'True' ELSE 'False' END as can_delete, " & _
                    "CASE WHEN can_report='Y' THEN 'True' ELSE 'False' END as can_report, " & _
                    "CASE WHEN can_export='Y' THEN 'True' ELSE 'False' END as can_export " & _
                   "FROM tbl_admin_cms_menu LEFT OUTER JOIN tbl_admin_user_access_permission ON tbl_admin_cms_menu.menu_id = tbl_admin_user_access_permission.menu_id WHERE tbl_admin_cms_menu.parent_id = '" & _strparent & "' AND tbl_admin_user_access_permission.user_id ='" & _struserid & "'" & _
                   "ORDER BY tbl_admin_cms_menu.sort_order"

        Return _clsDB.Fill_DataTable(_strQuery)
    End Function

    Public Function fnModuleRolePermissionParent(ByVal _strroleid As String) As DataTable
        Dim _strQuery As String = ""
        _strQuery = "SELECT tbl_admin_cms_menu.menu_id, tbl_admin_cms_menu.menu_name, " & _
                    "CASE WHEN can_access='Y' THEN 'True' ELSE 'False' END as can_access, " & _
                    "CASE WHEN can_create='Y' THEN 'True' ELSE 'False' END as can_create, " & _
                    "CASE WHEN can_update='Y' THEN 'True' ELSE 'False' END as can_update, " & _
                    "CASE WHEN can_delete='Y' THEN 'True' ELSE 'False' END as can_delete, " & _
                    "CASE WHEN can_report='Y' THEN 'True' ELSE 'False' END as can_report, " & _
                    "CASE WHEN can_export='Y' THEN 'True' ELSE 'False' END as can_export " & _
                    "FROM tbl_admin_cms_menu LEFT OUTER JOIN tbl_admin_user_access_permission ON tbl_admin_cms_menu.menu_id = tbl_admin_user_access_permission.menu_id WHERE tbl_admin_cms_menu.parent_id = '0' AND tbl_admin_user_access_permission.user_role_id ='" & _strroleid & "' AND tbl_admin_user_access_permission.user_id='' " & _
                    "ORDER BY tbl_admin_cms_menu.sort_order"

        Return _clsDB.Fill_DataTable(_strQuery)
    End Function

    Public Function fnModuleRolePermissionChild(ByVal _strparent As String, ByVal _strroleid As String) As DataTable
        Dim _strQuery As String = ""
        _strQuery = "SELECT tbl_admin_cms_menu.menu_id, tbl_admin_cms_menu.menu_name, " & _
                    "CASE WHEN can_access='Y' THEN 'True' ELSE 'False' END as can_access, " & _
                    "CASE WHEN can_create='Y' THEN 'True' ELSE 'False' END as can_create, " & _
                    "CASE WHEN can_update='Y' THEN 'True' ELSE 'False' END as can_update, " & _
                    "CASE WHEN can_delete='Y' THEN 'True' ELSE 'False' END as can_delete, " & _
                    "CASE WHEN can_report='Y' THEN 'True' ELSE 'False' END as can_report, " & _
                    "CASE WHEN can_export='Y' THEN 'True' ELSE 'False' END as can_export " & _
                    "FROM tbl_admin_cms_menu LEFT OUTER JOIN tbl_admin_user_access_permission ON tbl_admin_cms_menu.menu_id = tbl_admin_user_access_permission.menu_id WHERE tbl_admin_cms_menu.parent_id = '" & _strparent & "' AND tbl_admin_user_access_permission.user_role_id ='" & _strroleid & "' AND tbl_admin_user_access_permission.user_id='' " & _
                    "ORDER BY tbl_admin_cms_menu.sort_order"

        Return _clsDB.Fill_DataTable(_strQuery)
    End Function

    Public Sub deleteUserPermission(ByVal _userid As String)
        _clsDB.Delete_Record("DELETE FROM tbl_admin_user_access_permission WHERE user_id='" & _userid & "'")
    End Sub
    Public Sub deleteUserPermissionByMenu()
        _clsDB.Delete_Record("DELETE FROM tbl_admin_user_access_permission WHERE user_id='" & _accessUserID & "' AND menu_id=" & _accessMenuID)
    End Sub
    Public Sub deleteRolePermission(ByVal _roleid As String)
        _clsDB.Delete_Record("DELETE FROM tbl_admin_user_access_permission WHERE user_role_id='" & _roleid & "' AND user_id=''")
    End Sub

    Public Function getMenuPermission(ByVal _strUserId As String, ByVal _strUserRoleId As String, ByVal _strPageURL As String) As Boolean()
        Dim _dtRole As New DataTable
        Dim _dtUser As New DataTable
        Dim _strQueryUser As String = "", _strQueryRole As String = "", _strQuery = ""
        Dim _strScript As String, _strScriptAdd As String


        Dim _bolControl(5) As Boolean
        _bolControl(0) = False ' Access
        _bolControl(1) = False ' Create
        _bolControl(2) = False ' Update
        _bolControl(3) = False ' Delete
        _bolControl(4) = False ' Report
        _bolControl(5) = False ' Export
        Try
            _strScript = _strPageURL.ToLower.Replace("add.a", ".a").Replace("list.a", ".a").Replace("view.a", ".a")
            _strScriptAdd = _strPageURL.ToLower
            _strQuery = "SELECT tbl_admin_user_access_permission.user_permission_id, tbl_admin_user_access_permission.user_role_id, tbl_admin_user_access_permission.user_id, tbl_admin_cms_menu.page_url, " & _
                        "CASE WHEN can_access='Y' THEN 'True' ELSE 'False' END as can_access, " & _
                        "CASE WHEN can_create='Y' THEN 'True' ELSE 'False' END as can_create, " & _
                        "CASE WHEN can_update='Y' THEN 'True' ELSE 'False' END as can_update, " & _
                        "CASE WHEN can_delete='Y' THEN 'True' ELSE 'False' END as can_delete, " & _
                        "CASE WHEN can_report='Y' THEN 'True' ELSE 'False' END as can_report, " & _
                        "CASE WHEN can_export='Y' THEN 'True' ELSE 'False' END as can_export " & _
                        " FROM tbl_admin_user_access_permission LEFT JOIN tbl_admin_cms_menu ON tbl_admin_cms_menu.menu_id = tbl_admin_user_access_permission.menu_id"

            '_strQueryUser = _strQuery & " WHERE (lower(tbl_admin_user_access_permission.page_url)='" & _strScript.ToString() & "' or lower(tbl_admin_user_access_permission.page_url)='" & _strScriptAdd.ToString() & "') and tbl_admin_user_access_permission.user_id='" & _strUserId & "' and tbl_admin_user_access_permission.user_role_id='" & _strUserRoleId & "'"
            '_strQueryRole = _strQuery & " WHERE (lower(tbl_admin_user_access_permission.page_url)='" & _strScript.ToString() & "' or lower(tbl_admin_user_access_permission.page_url)='" & _strScriptAdd.ToString() & "') and tbl_admin_user_access_permission.user_id='' and tbl_admin_user_access_permission.user_role_id='" & _strUserRoleId & "'"
            _strQueryUser = _strQuery & " WHERE (lower(tbl_admin_user_access_permission.page_url)='" & _strScript.ToString() & "' or lower(tbl_admin_user_access_permission.page_url)='" & _strScriptAdd.ToString() & "') AND (tbl_admin_user_access_permission.user_id='" & _strUserId & "') "
            _strQueryRole = _strQuery & " WHERE (lower(tbl_admin_user_access_permission.page_url)='" & _strScript.ToString() & "' or lower(tbl_admin_user_access_permission.page_url)='" & _strScriptAdd.ToString() & "') AND tbl_admin_user_access_permission.user_role_id='" & _strUserRoleId & "'"
            _dtUser = _clsDB.Fill_DataTable(_strQueryUser)
            _dtRole = _clsDB.Fill_DataTable(_strQueryRole)


            ' Get Data Table Permission()
            If _dtRole.Rows.Count > 0 Then
                _bolControl(0) = dimboUtil.stringToBoolean(_dtRole.Rows(0)("can_access"))
                _bolControl(1) = dimboUtil.stringToBoolean(_dtRole.Rows(0)("can_create"))
                _bolControl(2) = dimboUtil.stringToBoolean(_dtRole.Rows(0)("can_update"))
                _bolControl(3) = dimboUtil.stringToBoolean(_dtRole.Rows(0)("can_delete"))
                _bolControl(4) = dimboUtil.stringToBoolean(_dtRole.Rows(0)("can_report"))
                _bolControl(5) = dimboUtil.stringToBoolean(_dtRole.Rows(0)("can_export"))
            End If

            If _dtUser.Rows.Count > 0 Then
                _bolControl(0) = dimboUtil.stringToBoolean(_dtUser.Rows(0)("can_access"))
                _bolControl(1) = dimboUtil.stringToBoolean(_dtUser.Rows(0)("can_create"))
                _bolControl(2) = dimboUtil.stringToBoolean(_dtUser.Rows(0)("can_update"))
                _bolControl(3) = dimboUtil.stringToBoolean(_dtUser.Rows(0)("can_delete"))
                _bolControl(4) = dimboUtil.stringToBoolean(_dtUser.Rows(0)("can_report"))
                _bolControl(5) = dimboUtil.stringToBoolean(_dtUser.Rows(0)("can_export"))
            End If

        Catch ex As Exception
        End Try
        Return _bolControl
    End Function
End Class
