
Partial Class MasterPage_Admin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ctl00$form-select", "$('.ddlSel').select2({theme: 'bootstrap-5',dropdownAutoWidth:'true',});", True)

        'Modal DDL

        ' ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ctl00$form-select2", "$('.ddlSelM').select2({theme: 'bootstrap-5',dropdownParent: $('.modalHead'),width: $( this ).data( 'width' ) ? $( this ).data( 'width' ) : $( this ).hasClass( 'w-100' ) ? '100%' : 'style',placeholder: $( this ).data( 'placeholder' ),});", True)

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ctl00$form-select2", "$('.ddlSelM').select2({theme: 'bootstrap-5',dropdownParent: $('.modalHead'),dropdownAutoWidth:'true',});", True)

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ctl00$form-select3", "$('.ddlSelM2').select2({theme: 'bootstrap-5',dropdownParent: $('.modalHead2'),dropdownAutoWidth:'true',});", True)


        'User Details

        spanUserName.InnerText = Session("UserName")
        spanUserNameDetails.InnerText = Session("UserName")
        spanOffice.InnerText = Session("DEPARTMENT_NAME")

    End Sub


    Protected Sub aSignout_ServerClick(sender As Object, e As EventArgs) Handles aSignout.ServerClick
        Session.Abandon()
        Response.Redirect("~/default.aspx")
    End Sub


    Protected Sub aChangePassword_ServerClick(sender As Object, e As EventArgs) Handles aChangePassword.ServerClick
        Response.Redirect("~/Secured/SystemAdministration/adUserChangePassword.aspx")
    End Sub
End Class

