Imports System.Data
Partial Class Include_sFooter
    Inherits System.Web.UI.UserControl
    Dim _clsDB As New clsDatabase
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        'spanFDesc.InnerText = ConfigurationManager.AppSettings("fDesc").ToString

        'For Each ctrl As Control In pDesc.Controls
        '    If ctrl.ID <> "" Then
        '        MsgBox(ctrl.ID.ToString)
        '    End If

        'Next

        'If Session("UserName") <> "" Then
        '    liHelpdesk.Visible = True
        'Else
        '    liHelpdesk.Visible = False
        'End If

    End Sub
End Class
