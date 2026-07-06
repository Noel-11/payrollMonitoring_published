Imports System.Data

Partial Class Include_sMainMenu
    Inherits System.Web.UI.UserControl

    Dim _clsCMS As New clsCMS
    Dim _dt As DataTable, _dtSub As DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        loadMenuHeader()
        If Not IsNothing(Session("accPaneIndex")) Then
            accMainMenu.SelectedIndex = Integer.Parse(Session("accPaneIndex").ToString())
        Else
            accMainMenu.SelectedIndex = 0
        End If
        loadMenuSelected(accMainMenu.SelectedIndex)

    End Sub

    Private Sub loadMenuSelected(ByVal _intMenuIndex As Integer)
        If _intMenuIndex >= 0 Then
            Try
                accMainMenu.Panes.Item(_intMenuIndex).HeaderContainer.BackImageUrl = "~/images/MenuS2.png"
            Catch ex As Exception : End Try
        End If
    End Sub

    Private Sub loadMenuHeader()

        Dim _strMenu, _strMenuID, _pageURL As String

        _dt = _clsCMS.browseSecureCMSMenuHeaderPermission(Session("UserId"), Session("UserRoleId"))

        '_dt.DefaultView.Sort = "sortorder  ASC"

        If _dt.Rows.Count > 0 Then

            For f As Integer = 0 To _dt.Rows.Count - 1
                _strMenuID = _dt.Rows(f)("menu_id").ToString()
                _strMenu = _dt.Rows(f)("menu_name").ToString()
                _pageURL = _dt.Rows(f)("page_url").ToString()

                Dim _paneMenu = New AjaxControlToolkit.AccordionPane()
                _paneMenu.ID = _dt.Rows(f)("menu_id").ToString() & Guid.NewGuid.ToString
                _paneMenu.HeaderContainer.ID = _dt.Rows(f)("menu_id").ToString() & Guid.NewGuid.ToString
                _paneMenu.HeaderContainer.BackImageUrl = "~/images/MenuS1.png"
                _paneMenu.HeaderContainer.Height = 22
                _paneMenu.ContentContainer.BorderColor = System.Drawing.ColorTranslator.FromHtml("#000000")


                Dim _lnkMenu As New LinkButton
                _lnkMenu.SkinID = "lnkMenu"
                _lnkMenu.ID = f.ToString()
                _lnkMenu.Text = _strMenu.ToString()
                _lnkMenu.CommandName = f.ToString()
                _lnkMenu.CommandArgument = _pageURL.ToString() + "?nmenu=" + _dt.Rows(f)("menu_id").ToString()
                AddHandler _lnkMenu.Command, AddressOf MenuItem_OnClick

                If _pageURL = "" Then
                    _paneMenu.HeaderContainer.Controls.Add(New LiteralControl(_strMenu.ToString()))
                Else
                    _paneMenu.HeaderContainer.Controls.Add(_lnkMenu)
                End If
                _paneMenu.ContentContainer.Controls.Add(loadMenuContainer(_strMenuID, f))
                Me.accMainMenu.Panes.Add(_paneMenu)

            Next
        End If

    End Sub

    Private Function loadMenuContainer(ByVal _strMenuId As String, ByVal _intPaneIndex As Integer) As Table

        _dtSub = _clsCMS.browseSecureCMSSubMenuPermission(Session("UserId"), Session("UserRoleId"), _strMenuId)

        Dim _tblContainer As New Table
        _tblContainer.SkinID = "tblMenuContainer"

        If _dtSub.Rows.Count > 0 Then
            For f As Integer = 0 To _dtSub.Rows.Count - 1
                Dim _tRow As New TableRow
                Dim _tCel As New TableCell
                Dim _lnkMenuSub As New LinkButton
                _lnkMenuSub.SkinID = "lnkButton"
                _lnkMenuSub.ID = f.ToString()
                _lnkMenuSub.Text = _dtSub.Rows(f)("menu_name").ToString()
                _lnkMenuSub.CommandName = _intPaneIndex
                _lnkMenuSub.CommandArgument = _dtSub.Rows(f)("page_url").ToString() + "?nmenu=" + _dtSub.Rows(f)("menu_id").ToString()
                AddHandler _lnkMenuSub.Command, AddressOf MenuItem_OnClick
                _tCel.Controls.Add(_lnkMenuSub)
                _tRow.Cells.Add(_tCel)
                _tblContainer.Rows.Add(_tRow)
            Next
        Else
            _tblContainer.Height = 0
        End If

        Return _tblContainer

    End Function

    Protected Sub MenuItem_OnClick(ByVal sender As Object, ByVal e As CommandEventArgs)
        Session("accPaneIndex") = Integer.Parse(e.CommandName.ToString())
        Response.Redirect(e.CommandArgument.ToString())
        'Response.Redirect(e.CommandArgument.ToString() & "&bCrumb=1")
    End Sub


End Class
