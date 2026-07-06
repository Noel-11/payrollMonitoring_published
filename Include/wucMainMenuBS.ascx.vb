Imports System.Data
Partial Class Include_wucMainMenuBS
    Inherits System.Web.UI.UserControl
    Dim _clsDB As New clsDatabase
    Dim _clsCMS As New clsCMS
    Dim menuWidth As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      
        'GetMenuData()
        If Session("PAYROLLModule") = Nothing Then
            Session.Abandon()
            Response.Redirect("~/Default.aspx")
        End If

        If IsNothing(Session("NavMenu")) Then
            GetMenuData()
        Else

            navMenu.Controls.Add(CType(Session("NavMenu"), LiteralControl))

        End If
    End Sub

    Private Sub addModuleMenu()
        menuWidth = menuWidth + 180
        Dim menuItem As New MenuItem("MODULES", "")
        menuItem.NavigateUrl = ""
       
    End Sub

    Private Sub GetMenuData()
        Dim dtMenu As New DataTable
        Dim htmlStringHeadButton As String = ""
        Dim htmlString As String = ""
        Dim glyphy As String = ""
        Dim preURL As String = ConfigurationManager.AppSettings("preUrl").ToString
        Dim pageURL As String = ""
        dtMenu = _clsCMS.browseSecureCMSMenuHeaderPermissionByModule(Session("UserId"), Session("UserRoleId"), Session("PAYROLLModule"))

        Dim lcList As New LiteralControl

        'If Session("PAYROLLModule") <> "SYSTEM ADMINISTRATION" Then

        'htmlString = "<li><a href='/Secured/Default.aspx' ><button id='btnModule' type='button' class='btn btn-lg btn-block ' style='background-color:maroon;color:yellow; border-color:black'><span class='glyphicon glyphicon-home'></span>&nbsp;HOME</button></a><ul class='dropdown-menu'></ul></li>"
        'navMenu.Controls.Add(lcList)
        '  End If


        For Each row As DataRow In dtMenu.Rows
            menuWidth = menuWidth + 180

            lcList = New LiteralControl

            pageURL = row("page_url").ToString().Replace("~/", preURL)
            Dim dtChildMenu As New DataTable
            dtChildMenu = _clsCMS.browseSecureCMSSubMenuPermission(Session("UserId"), Session("UserRoleId"), row("menu_id").ToString())

            Dim viewItem As New DataView(dtChildMenu)
            Dim split As String()
            glyphy = "glyphicon " & row("glyphicon").ToString
            split = row("tab_id").ToString.Split("_")

            If row("page_url").ToString <> "" Then
                htmlString = htmlString & "<li><a href='" & pageURL & "'><button id='" & split(0) & "' type='button' class='btn btn-lg btn-block btn-info' style='background-color:gold; color:black'><span class='" & glyphy & "'></span>&nbsp;" & row("menu_name").ToString() & "</button></a></li>"
            Else
                htmlString = htmlString & "<li class='dropdown'><a href='" & pageURL & "' class='dropdown-toggle' data-toggle='dropdown'><button id='" & split(0) & "' type='button' class='btn btn-lg btn-block btn-info' style='background-color:black; color:gold' ><span class='" & glyphy & "'></span>&nbsp;" & row("menu_name").ToString() & "</button></a><ul class='dropdown-menu'>"
            End If

            For Each childMenu As DataRowView In viewItem
                glyphy = "glyphicon " & childMenu("glyphicon").ToString
                pageURL = childMenu("page_url").ToString().Replace("~/", preURL)

                htmlString = htmlString & "<li><a href='" & pageURL & "'><button type='button' class='btn btn-block btn-info' style='background-color:gold; color:black'><span class='" & glyphy & "'></span>&nbsp;" & childMenu("menu_name").ToString() & "</button></a></li>"
            Next

            If row("page_url") <> "" Then
                ' lcList.Text = htmlString
            Else
                'lcList.Text = htmlString + "</ul></li>"
                htmlString = htmlString & "</ul></li>"
            End If

            'navMenu.Controls.Add(lcList)
        Next

      

        Dim dtMod As New DataTable

        dtMod = _clsDB.Fill_DataTable("SELECT * FROM tbl_admin_modules ORDER BY sort_order", "tbl_mods")



        htmlString = htmlString & "<li class='dropdown'><a href='' class='dropdown-toggle' data-toggle='dropdown'><button id='btnModule' type='button' class='btn btn-lg btn-block ' style='background-color:gold; color:black; border-color:black'><span class='glyphicon glyphicon-th-list'></span>&nbsp;" & Session("PAYROLLModule").ToString & "</button></a><ul class='dropdown-menu'>"

        glyphy = ""

        For Each rowMod As DataRow In dtMod.Rows

            pageURL = rowMod("page_url").ToString().Replace("~/", preURL)

            htmlString = htmlString & "<li><a href='" & pageURL & "'><button type='button' class='btn btn-block btn-default' style='background-color:black; color:gold'><span class='" & glyphy & "'></span>&nbsp;" & rowMod("module").ToString() & "</button></a></li>"
        Next


        htmlString = htmlString & "</ul></li>"


        lcList.Text = htmlString
        navMenu.Controls.Add(lcList)

        Session("NavMenu") = lcList
    End Sub
End Class
