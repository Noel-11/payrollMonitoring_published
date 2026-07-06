
Partial Class Include_wucNotification
    Inherits System.Web.UI.UserControl

    Public Sub showConfirmBox(ByVal msg As String)

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "notification", "myNotification('" & msg & "');", True)
    End Sub

End Class
