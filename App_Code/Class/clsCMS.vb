Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsCMS
    Dim _clsDB As New clsDatabase

    Dim _strQueryBase As String = ""
    Dim _strQuery As String = ""

    Public Sub New()
        initialize()
    End Sub

    Public Property menuID As String

    Public Property menuParentID As String

    Public Property menuName As String

    Public Property menuPageURL As String

    Public Property menuLevel As String

    Public Property menuSortOrder As String

    Public Property isPublic As String

    Public Property systemModule As String
    Public Property glyphicon As String
    Public Property isActive As String

    Public Property lastUser As String

    Public Sub initialize()
        _menuParentID = 0
        _menuName = ""
        _menuPageURL = ""
        _menuLevel = 0
        _menuSortOrder = 0
        _systemModule = ""
        _isPublic = "N"
        _isActive = "N"
        _glyphicon = ""
    End Sub

    Public Sub saveCMSMenu()

        If _menuID = "" Then

            With _clsDB.dbUtility
                .fieldItems = "parent_id,menu_name,page_url,menu_level,sort_order,is_public,module,glyphicon,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_admin_cms_menu", "INSERT")
                .ADDPARAM_CMD_String("parent_id", _menuParentID)
                .ADDPARAM_CMD_String("menu_name", _menuName)
                .ADDPARAM_CMD_String("page_url", _menuPageURL)
                .ADDPARAM_CMD_String("menu_level", _menuLevel)
                .ADDPARAM_CMD_String("sort_order", _menuSortOrder)
                .ADDPARAM_CMD_String("is_public", _isPublic)
                .ADDPARAM_CMD_String("module", _systemModule)
                .ADDPARAM_CMD_String("glyphicon", _glyphicon)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "parent_id,menu_name,page_url,menu_level,sort_order,is_public,glyphicon,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_admin_cms_menu", "UPDATE", "menu_id")
                .ADDPARAM_CMD_String("parent_id", _menuParentID)
                .ADDPARAM_CMD_String("menu_name", _menuName)
                .ADDPARAM_CMD_String("page_url", _menuPageURL)
                .ADDPARAM_CMD_String("menu_level", _menuLevel)
                .ADDPARAM_CMD_String("sort_order", _menuSortOrder)
                .ADDPARAM_CMD_String("is_public", _isPublic)
                .ADDPARAM_CMD_String("glyphicon", _glyphicon)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .ADDPARAM_CMD_String("menu_id", _menuID)
                .executeUsingCommandFromSQL(True)
            End With
        End If

    End Sub


    Public Function browseSecureCMSMenuHeaderPermission(ByVal _strUserId As String, ByVal _strUserRoleId As String) As DataTable
        Dim _dtPermission As New DataTable


        _strQuery = "SELECT DISTINCT tbl_admin_cms_menu.menu_id, tbl_admin_cms_menu.menu_name, tbl_admin_cms_menu.page_url, tbl_admin_cms_menu.sort_order, tbl_admin_cms_menu.menu_level, tbl_admin_cms_menu.tab_id,tbl_admin_cms_menu.glyphicon " & _
                    "FROM tbl_admin_user_access_permission INNER JOIN tbl_admin_cms_menu ON tbl_admin_user_access_permission.menu_id = tbl_admin_cms_menu.menu_id " & _
                    "WHERE ((tbl_admin_user_access_permission.user_id='' AND tbl_admin_user_access_permission.user_role_id='" & _strUserRoleId & "') OR tbl_admin_user_access_permission.user_id='" & _strUserId & "') AND (tbl_admin_user_access_permission.can_access='Y' AND tbl_admin_cms_menu.parent_id='0' AND tbl_admin_cms_menu.menu_level=1 AND tbl_admin_cms_menu.is_public='N' AND tbl_admin_cms_menu.is_active='Y') ORDER BY tbl_admin_cms_menu.sort_order"

        _dtPermission = _clsDB.Fill_DataTable(_strQuery)

        Return _dtPermission

    End Function

    Public Function browseSecureCMSMenuHeaderPermissionByModule(ByVal _strUserId As String, ByVal _strUserRoleId As String, _Module As String) As DataTable
        Dim _dtPermission As New DataTable

        _strQuery = "SELECT DISTINCT tbl_admin_cms_menu.menu_id, tbl_admin_cms_menu.menu_name, tbl_admin_cms_menu.page_url, tbl_admin_cms_menu.sort_order, tbl_admin_cms_menu.menu_level, tbl_admin_cms_menu.tab_id,glyphicon " & _
                    "FROM tbl_admin_user_access_permission INNER JOIN tbl_admin_cms_menu ON tbl_admin_user_access_permission.menu_id = tbl_admin_cms_menu.menu_id " & _
                    "WHERE ((tbl_admin_user_access_permission.user_id='' AND tbl_admin_user_access_permission.user_role_id='" & _strUserRoleId & "') OR tbl_admin_user_access_permission.user_id='" & _strUserId & "') AND (tbl_admin_user_access_permission.can_access='Y' AND tbl_admin_cms_menu.parent_id='0' AND tbl_admin_cms_menu.menu_level=1 AND tbl_admin_cms_menu.is_public='N' AND tbl_admin_cms_menu.is_active='Y' AND tbl_admin_cms_menu.module='" & _Module & "') ORDER BY tbl_admin_cms_menu.sort_order"

        _dtPermission = _clsDB.Fill_DataTable(_strQuery)

        Return _dtPermission

    End Function

    Public Function browseSecureCMSSubMenuPermission(ByVal _strUserId As String, ByVal _strUserRoleId As String, ByVal _strParentId As String) As DataTable
        Dim _dtPermission As New DataTable

        _strQuery = "SELECT DISTINCT tbl_admin_cms_menu.menu_id, tbl_admin_cms_menu.menu_name, tbl_admin_cms_menu.page_url, tbl_admin_cms_menu.sort_order, tbl_admin_cms_menu.menu_level,COALESCE(tbl_admin_cms_menu.glyphicon,'') AS glyphicon " & _
                    "FROM tbl_admin_user_access_permission INNER JOIN tbl_admin_cms_menu ON tbl_admin_user_access_permission.menu_id = tbl_admin_cms_menu.menu_id " & _
                    "WHERE ((tbl_admin_user_access_permission.user_id='' AND tbl_admin_user_access_permission.user_role_id='" & _strUserRoleId & "') OR tbl_admin_user_access_permission.user_id='" & _strUserId & "') AND (tbl_admin_user_access_permission.can_access='Y' AND tbl_admin_cms_menu.parent_id='" & _strParentId & "' AND tbl_admin_cms_menu.menu_level=2 AND tbl_admin_cms_menu.is_public='N' AND tbl_admin_cms_menu.is_active='Y') ORDER BY tbl_admin_cms_menu.sort_order"

        _dtPermission = _clsDB.Fill_DataTable(_strQuery)

        Return _dtPermission
    End Function
    Public Function browseCMSMenuByModule(thisModule As String, ByVal _strmenuid As String, ByVal _strparent_id As String, ByVal _strSearch As String) As DataTable
        _strQueryBase = "SELECT tbl_admin_cms_menu.menu_id, tbl_admin_cms_menu.menu_name, tbl_admin_cms_menu.sort_order, tbl_admin_cms_menu.is_public, tbl_admin_cms_menu.is_active " & _
                        "FROM tbl_admin_cms_menu "
        _strQuery = _strQueryBase & " WHERE tbl_admin_cms_menu.parent_id=" & _strparent_id


        If _strparent_id = "0" Then
            _strQuery = _strQuery & " AND module='" & thisModule & "'"
        End If

        If _strmenuid <> "" Then
            _strQuery = _strQueryBase & " WHERE tbl_admin_cms_menu.menu_id=" & _strmenuid
        Else
            If _strSearch <> "" Then
                _strQuery = _strQueryBase & " WHERE tbl_admin_cms_menu.menu_name LIKE '%" & _strSearch & "%'"
            End If
        End If

        Return _clsDB.Fill_DataTable(_strQuery & " ORDER BY tbl_admin_cms_menu.is_public, tbl_admin_cms_menu.sort_order")
    End Function
    Public Function fnModuleMenu_BrowseMenu(ByVal _strUserId As String, ByVal _strUserRoleId As String, ByVal _strParentId As String, ByVal _intLevel As Integer, ByVal _intIsPublic As Integer, Optional ByVal _intPageMenu As String = "0") As DataTable
        Dim _dtRoles As New DataTable


        _strQuery = "SELECT DISTINCT tbl_system_modules.system_module_desc FROM tbl_admin_cms_menu INNER JOIN tbl_system_modules ON tbl_admin_cms_menu.hrmis_module = tbl_system_modules.system_module_desc INNER JOIN tbl_admin_user_access_permission ON tbl_admin_cms_menu.menu_id = tbl_admin_user_access_permission.menu_id " & _
                    "WHERE ((user_id='' AND tbl_admin_user_access_permission.user_role_id='" & _strUserRoleId & "') OR (tbl_admin_user_access_permission.user_id='" & _strUserId & "' AND tbl_admin_user_access_permission.user_role_id='" & _strUserRoleId & "')) and tbl_admin_user_access_permission.can_access='True' and (tbl_admin_cms_menu.parent_id='" & _strParentId & "' and tbl_admin_cms_menu.menu_level=" & _intLevel.ToString() & " and tbl_admin_cms_menu.ispublic='False' and tbl_admin_cms_menu.ispublish='True')"


        _dtRoles = _clsDB.Fill_DataTable(_strQuery)


        Return _dtRoles
    End Function


    Public Sub getCMSMenu(ByVal thisMenuID As String)
        Dim dt As New DataTable

        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_admin_cms_menu WHERE menu_id=" & thisMenuID)

        If dt.Rows.Count > 0 Then
            _menuParentID = dt.Rows(0)("parent_id")
            _menuName = dt.Rows(0)("menu_name").ToString
            _menuPageURL = dt.Rows(0)("page_url").ToString
            _menuLevel = dt.Rows(0)("menu_level")
            _menuSortOrder = dt.Rows(0)("sort_order").ToString
            _systemModule = dt.Rows(0)("module").ToString
            _glyphicon = dt.Rows(0)("glyphicon").ToString
            _isPublic = dt.Rows(0)("is_public").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub


    Public Function browseCMSMenu(ByVal _strmenuid As String, ByVal _strparent_id As String, ByVal _strSearch As String) As DataTable
        _strQueryBase = "SELECT tbl_admin_cms_menu.menu_id, tbl_admin_cms_menu.menu_name, tbl_admin_cms_menu.sort_order, tbl_admin_cms_menu.is_public, tbl_admin_cms_menu.is_active " & _
                        "FROM tbl_admin_cms_menu "
        _strQuery = _strQueryBase & " WHERE tbl_admin_cms_menu.parent_id=" & _strparent_id
        If _strmenuid <> "" Then
            _strQuery = _strQueryBase & " WHERE tbl_admin_cms_menu.menu_id=" & _strmenuid
        Else
            If _strSearch <> "" Then
                _strQuery = _strQueryBase & " WHERE tbl_admin_cms_menu.menu_name LIKE '%" & _strSearch & "%'"
            End If
        End If

        Return _clsDB.Fill_DataTable(_strQuery & " ORDER BY tbl_admin_cms_menu.is_public, tbl_admin_cms_menu.sort_order")
    End Function


    Public Function browseParentCMSMenu() As DataTable
        Dim _strQuery As String = ""
        _strQuery = "SELECT tbl_admin_cms_menu.menu_id,tbl_admin_cms_menu.menu_name,tbl_admin_cms_menu.page_url " & _
                    "FROM tbl_admin_cms_menu WHERE is_public='N' AND is_active='Y' AND tbl_admin_cms_menu.parent_id ='0' " & _
                    " ORDER BY tbl_admin_cms_menu.sort_order"

        Return _clsDB.Fill_DataTable(_strQuery)
    End Function

    Public Function browseParentCMSMenuSystem() As DataTable
        Dim _strQuery As String = ""


        _strQuery = "SELECT tbl_admin_cms_menu.menu_id,tbl_admin_cms_menu.menu_name,tbl_admin_cms_menu.page_url " & _
                    "FROM tbl_admin_cms_menu WHERE  is_public='N' AND is_active='Y' AND (tbl_admin_cms_menu.parent_id='0' AND menu_id<>'100') " & _
                    " ORDER BY tbl_admin_cms_menu.sort_order"

        Return _clsDB.Fill_DataTable(_strQuery)
    End Function

    Public Function browseChildCMSMenu(ByVal _strparent As String) As DataTable
        Dim _strQuery As String = ""
        _strQuery = "SELECT tbl_admin_cms_menu.menu_id,tbl_admin_cms_menu.menu_name,tbl_admin_cms_menu.page_url " & _
                    "FROM tbl_admin_cms_menu WHERE is_public='N' AND is_active='Y' AND tbl_admin_cms_menu.parent_id =" & _strparent & _
                    " ORDER BY tbl_admin_cms_menu.sort_order"


        Return _clsDB.Fill_DataTable(_strQuery)
    End Function
    Public Function browseParentCMSMenuSystemByModule(thisModule As String) As DataTable
        Dim _strQuery As String = ""
        _strQuery = "SELECT tbl_admin_cms_menu.menu_id,tbl_admin_cms_menu.menu_name,tbl_admin_cms_menu.page_url " & _
                    "FROM tbl_admin_cms_menu WHERE module='" & thisModule & "' AND  tbl_admin_cms_menu.menu_id<>'1' AND is_public='N' AND is_active='Y' AND tbl_admin_cms_menu.parent_id ='0' " & _
                    " ORDER BY tbl_admin_cms_menu.sort_order"

        Return _clsDB.Fill_DataTable(_strQuery)
    End Function



End Class
