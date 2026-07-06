
Partial Class MasterPage_InitialAdmin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblDepartment.Text = Session("DEPARTMENT_NAME")

    End Sub

End Class

