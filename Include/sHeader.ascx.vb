
Partial Class Include_sHeader
    Inherits System.Web.UI.UserControl


    Protected Sub lnkLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogout.Click
        Session("accPaneIndex") = 0
        Session.Abandon()
        Response.Redirect("~/Default.aspx")
    End Sub

    Protected Sub lnkChangePassword_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles lnkChangePassword.Command
        Response.Redirect("~/Secured/SystemAdministration/adUserChangePassword.aspx?nUserId=" & Session("UserId"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserId") = Nothing Or Session("PAYROLLModule") = Nothing Then
            Session.Abandon()
            'Response.Redirect("~/public/pubAnnouncement.aspx")
            Response.Redirect("~/Default.aspx")
        End If
        lblUsername.Text = ": " + Session("UserName").ToString() & " (" & Session("DEPARTMENT_NAME") & ")"
    End Sub
End Class
