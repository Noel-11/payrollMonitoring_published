Imports System.Data
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports Microsoft.Reporting.WebForms
Partial Class Payroll_check
    Inherits System.Web.UI.Page

    Dim _dt As New DataTable

    Dim clsLibrary As New clsLibrary
    Dim _clsUser As New clsUser
    Dim _clsDB As New clsDatabase
    Dim _dtCV As New DataTable

    Dim _btnOK As New HtmlButton

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Session("SENDSMS") = _clsDB.Get_DB_Item("SELECT default_value FROM tbl_admin_system_default WHERE default_desc = 'notify_sms' LIMIT 1 ")
            'pnlPassword.Visible = False
            'pnlPin.Visible = False
        End If

        _btnOK = thisMsgBox.FindControl("btnMsgBoxNo")
        AddHandler _btnOK.ServerClick, AddressOf btnOK_Click

    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If thisMsgBox.getModalType = "REGISTERED" Then
            Response.Redirect("Payroll_check.aspx")
        End If

    End Sub

#Region "CHECK PAYROLL"

    Protected Sub btnRegRetrieve_Click(sender As Object, e As EventArgs) Handles btnRegRetrieve.ServerClick

        Dim _clsEmployee As New clsPayrollEmployeeInformation

        If _clsEmployee.validateLogin(txtRegEmployeeID.Text.Trim.Replace("'", "").Replace("--", "").Replace(";", ""), txtEmployeePassword.Text) = True Then

            getInfo()
        Else

            thisMsgBox.setError("Cannot Generate Payroll List", "Invalid ID or/and Password ")
            thisMsgBox.showConfirmBox()

            saveLogs(lblOffice.Text.Trim.ToUpper, GetIPAddress(), "INVALID ID/PASSWORD")

        End If

    End Sub

    Private Sub getInfo()

        Try
            Dim _clsDB As New clsDatabase

            Dim dt As New DataTable

            dt = _clsDB.Fill_DataTable("SELECT employee_id,contact_no FROM tbl_payroll_employee_information WHERE agency_id=" & txtRegEmployeeID.Text.Trim.Replace("'", "").Replace("--", "").Replace(";", "") & "  LIMIT 1")

            If dt.Rows.Count > 0 Then
                hfEmployeeID.Value = dt.Rows(0)("employee_id").ToString
                ' hfContactNo.Value = dt.Rows(0)("contact_no").ToString

                fillEmployeeInfo(hfEmployeeID.Value)

                fillGVStatus(hfEmployeeID.Value)

                saveLogs(lblOffice.Text.Trim.ToUpper, GetIPAddress(), "SEARCH FOUND")

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mdlPayroll", "var myModal = new bootstrap.Modal(document.getElementById('mdlPayroll'), {});  myModal.show();", True)

            Else
                hfEmployeeID.Value = ""
                hfContactNo.Value = ""

                saveLogs("", GetIPAddress(), "NOT FOUND")

                thisMsgBox.setError("NOT FOUND!", "Employee Record Not Found")
                thisMsgBox.showConfirmBox()
            End If

            'lblPinContact.Text = ""

            'If hfEmployeeID.Value = "" Then
            '    thisMsgBox.setError("NOT FOUND!", "Employee Record Not Found")
            '    pnlPassword.Visible = False
            '    pnlPin.Visible = False
            'Else

            '    If hfContactNo.Value <> "" Then
            '        thisMsgBox.setNotification("Record found!")
            '        'pnlPassword.Visible = True

            '        lblPinContact.Text = Left(hfContactNo.Value, 3) & "*****" & Right(hfContactNo.Value, 3)
            '        pnlPin.Visible = True
            '        sendCode()
            '    Else
            '        thisMsgBox.setError("Cannot Verify", "No Contact Number found! <br/> ask the accounting office to record your contact number.")
            '    End If

            'End If

        Catch ex As Exception
            thisMsgBox.setError(, ex.Message)
            thisMsgBox.showConfirmBox()
        End Try


    End Sub


    Private Sub saveLogs(ByVal _thisOffice As String, ByVal _thisIp As String, ByVal _thisStatus As String)

        With _clsDB.dbUtility
            .fieldItems = "employee_id,employee_lname,employee_fname,employee_mname,employee_ename,employee_office,ip_address,retrieve_status,retrieve_datetime"
            .sqlString = .getSQLStatement("tbl_employee_payroll_monitoring_logs", "INSERT")
            '_transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 11).ToUpper
            .ADDPARAM_CMD_String("employee_id", txtRegEmployeeID.Text.Trim.ToUpper)
            .ADDPARAM_CMD_String("employee_lname", txtRegLastName.Text.Trim.ToUpper)
            .ADDPARAM_CMD_String("employee_fname", txtRegFirstName.Text.Trim.ToUpper)
            .ADDPARAM_CMD_String("employee_mname", "")
            .ADDPARAM_CMD_String("employee_ename", txtExtName.Text.Trim.ToUpper)
            .ADDPARAM_CMD_String("employee_office", _thisOffice)
            .ADDPARAM_CMD_String("ip_address", _thisIp)
            .ADDPARAM_CMD_String("retrieve_status", _thisStatus)
            .ADDPARAM_CMD_String("retrieve_datetime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            .executeUsingCommandFromSQL(True)
        End With

    End Sub


    Private Sub fillEmployeeInfo(ByVal _thisId As String)

        Dim _clsInfo As New clsPayrollEmployeeInformation

        With _clsInfo
            .getPayrollEmployeeInformation(_thisId)
            lblOffice.Text = _clsDB.Get_DB_Item("SELECT department_name FROM tbl_payroll_ref_department WHERE department_id = '" & .department & "' LIMIT 1")
            lblName.Text = .lastName & ", " & .firstName & " " & .extName & " " & .middleName
            'lblFirstName.Text = .firstName
            'lblMiddleName.Text = .middleName
            'lblExtName.Text = .extName
            lblAgencyId.Text = .agencyId
            lblEmployeePosition.Text = _clsDB.Get_DB_Item("SELECT position_name FROM tbl_payroll_ref_employee_position WHERE position_id = '" & .employeePosition & "' LIMIT 1")
            lblEmployeePositionStep.Text = .employeePositionStep
            lblEmployeeStatus.Text = .employeeStatus
            'ddlIsActive.SelectedValue = .isActive
        End With

    End Sub

    Private Sub fillGVStatus(ByVal _thisId As String)

        Dim _clsPayrollTransEmp As New clsPayrollTransactionEmployees

        Dim dt As New DataTable

        dt = _clsPayrollTransEmp.browsePayrollTransactionEmployeesPayrollLedgerWithLAuditDateTimesHR(_thisId)

        _gvLedger.DataSource = dt
        _gvLedger.DataBind()

    End Sub

    Protected Sub cmdGVLogs(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim _clsLogs As New clsAuditTrail

        lblLogPeriod.Text = CType(sender, ImageButton).Attributes("period")
        lblLogRef.Text = CType(sender, ImageButton).Attributes("refCode")
        lblLogType.Text = CType(sender, ImageButton).Attributes("payrollType")
        lblLogClassification.Text = CType(sender, ImageButton).Attributes("empStatus")

        _gv.DataSource = _clsLogs.browseLogTransactions(e.CommandArgument.ToString, "PAYROLL PROCESSING")
        _gv.DataBind()

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pnlLogs", "var myModal = new bootstrap.Modal(document.getElementById('pnlLogs'), {});  myModal.show();", True)

    End Sub

#End Region

#Region "LEDGER"

    Protected Sub ibLogs_Click(sender As Object, e As ImageClickEventArgs) Handles ibLogs.Click
        Dim _clsLogs As New clsAuditTrail

        lblLogPeriod.Text = lblLedgerPeriod.Text
        lblLogRef.Text = lblLedgerRef.Text
        lblLogType.Text = lblLedgerType.Text
        lblLogClassification.Text = lblLedgerClassification.Text

        _gv.DataSource = _clsLogs.browseLogTransactions(hfPayrollId.Value, "PAYROLL PROCESSING")
        _gv.DataBind()

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pnlLogs", "var myModal = new bootstrap.Modal(document.getElementById('pnlLogs'), {});  myModal.show();", True)
    End Sub

    Protected Sub cmdGVCheck(ByVal sender As Object, ByVal e As CommandEventArgs)

        Dim _clsPayrollTransEmp As New clsPayrollTransactionEmployees

        lblLedgerEmpName.Text = lblName.Text.Trim.ToUpper
        lblLedgerPeriod.Text = CType(sender, ImageButton).Attributes("period")
        lblLedgerRef.Text = CType(sender, ImageButton).Attributes("refCode")
        lblLedgerType.Text = CType(sender, ImageButton).Attributes("payrollType")
        lblLedgerClassification.Text = CType(sender, ImageButton).Attributes("empStatus")

        hfPayrollId.Value = e.CommandArgument.ToString

        Dim dt As New DataTable

        dt = _clsPayrollTransEmp.browsePayrollTransactionEmployeesPayrollLedger(e.CommandArgument.ToString, hfEmployeeID.Value)

        _gvCheck.DataSource = dt
        _gvCheck.DataBind()

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mdlCheck", "var myModal = new bootstrap.Modal(document.getElementById('mdlCheck'), {});  myModal.show();", True)

    End Sub

    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim _clsDeductions As New clsPayrollTransactionDetails

            Dim thisHeaderId As String = DataBinder.Eval(e.Row.DataItem, "transaction_header_id")
            Dim thisTransEmp As String = DataBinder.Eval(e.Row.DataItem, "transaction_employees_id")
            Dim thisEmp As String = DataBinder.Eval(e.Row.DataItem, "employee_id")

            Dim thisAuditDate As String = DataBinder.Eval(e.Row.DataItem, "audited_date")

            Dim gvDeductions As GridView = TryCast(e.Row.FindControl("_gvDeductions"), GridView)
            Dim gvFixDeductions As GridView = TryCast(e.Row.FindControl("_gvFixDeductions"), GridView)
            Dim gvFixDeductions2 As GridView = TryCast(e.Row.FindControl("_gvFixDeductions2"), GridView)

            gvDeductions.DataSource = _clsDeductions.browsePayrollTransactionDetails2(thisHeaderId, thisTransEmp, "DEDUCTION")
            gvDeductions.DataBind()

            gvFixDeductions.DataSource = _clsDeductions.browsePayrollFixTransactionDetails(thisHeaderId, thisTransEmp, "SHARE")
            gvFixDeductions.DataBind()


            gvFixDeductions2.DataSource = _clsDeductions.browsePayrollFixTransactionDetails(thisHeaderId, thisTransEmp, "TAX")
            gvFixDeductions2.DataBind()

            Dim _gvAddIncome As GridView = TryCast(e.Row.FindControl("_gvAddIncome"), GridView)
            _gvAddIncome.DataSource = _clsDeductions.browsePayrollTransactionDetails2(thisHeaderId, thisTransEmp, "ADDITIONAL INCOME")
            _gvAddIncome.DataBind()

            Dim lblPostedDate As Label

            lblPostedDate = CType(e.Row.FindControl("lblPostedDate"), Label)

            Dim lblPostedBy As Label = CType(e.Row.FindControl("lblPostedBy"), Label)

            lblPostedBy.Text = _clsDB.Get_DB_Item("SELECT CONCAT('By: ',trans_by) FROM tbl_audit_trail WHERE trans_module = 'UPDATE' AND logged_changes = '(payroll_status)[ RECEIVED|POSTED ]' AND link_id = '" & thisHeaderId & "' LIMIT 1")

            If thisAuditDate <> "00/00/0000 12:00 AM" Then
                lblPostedDate.Text = thisAuditDate
            Else
                lblPostedDate.Text = _clsDB.Get_DB_Item("SELECT DATE_FORMAT(trans_datetime,'%m/%d/%Y %h:%i %p') FROM tbl_audit_trail WHERE trans_module = 'UPDATE' AND logged_changes = '(payroll_status)[ RECEIVED|POSTED ]' AND link_id = '" & thisHeaderId & "' LIMIT 1")
            End If

        End If

    End Sub

   

#End Region

#Region "FORGOT PASSWORD"

    Protected Sub aForgotP_ServerClick(sender As Object, e As EventArgs) Handles aForgotP.ServerClick

        pnlContact.Visible = True
        pnlPin.Visible = False
        pnlPassword.Visible = False

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mdlForgotPassword", "var myModal = new bootstrap.Modal(document.getElementById('mdlForgotPassword'), {});  myModal.show();", True)

    End Sub

    Protected Sub btnVerifyCel_ServerClick(sender As Object, e As EventArgs) Handles btnVerifyCel.ServerClick
        Try
            Dim _clsDB As New clsDatabase

            Dim dt As New DataTable

            dt = _clsDB.Fill_DataTable("SELECT employee_id,contact_no FROM tbl_payroll_employee_information WHERE contact_no = '" & txtContactNo.Text.Trim.Replace("'", "").Replace("--", "").Replace(";", "") & "' AND paycheck_password <> '' LIMIT 1")

            If dt.Rows.Count > 0 Then
                hfEmployeeID.Value = dt.Rows(0)("employee_id").ToString
                hfContactNo.Value = dt.Rows(0)("contact_no").ToString
            Else
                hfEmployeeID.Value = ""
                hfContactNo.Value = ""
            End If

            lblPinContact.Text = ""

            If hfEmployeeID.Value = "" Then
                thisMsgBox.setError("NOT FOUND!", "Contact Number Not Found or Not Registered!")
                pnlPassword.Visible = False
                pnlPin.Visible = False
            Else

                If hfContactNo.Value <> "" Then
                    thisMsgBox.setNotification("Record found!")
                    'pnlPassword.Visible = True

                    lblPinContact.Text = Left(hfContactNo.Value, 3) & "*****" & Right(hfContactNo.Value, 3)
                    pnlPin.Visible = True
                    sendCode()
                Else
                    thisMsgBox.setError("Cannot Verify", "No Contact Number found! <br/> Ask your payroll officer to record your contact number.")
                End If

            End If

        Catch ex As Exception
            thisMsgBox.setError(, ex.Message)
        End Try

        thisMsgBox.showConfirmBox()
    End Sub

    Protected Sub btnVerify_ServerClick(sender As Object, e As EventArgs) Handles btnVerify.ServerClick

        Dim dtCheck As New DataTable

        Dim sql As String = ""

        sql = "SELECT trans_id FROM tbl_paycheck_pin WHERE pin_code = '" & txtPinCode.Text.Trim.Replace("'", "").Replace("--", "").Replace(";", "") & "' AND expiration_date > '" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'  ORDER BY create_date DESC LIMIT 1"

        dtCheck = _clsDB.Fill_DataTable(sql, "tbl_paycheck_pin")

        If dtCheck.Rows.Count > 0 Then
            pnlPassword.Visible = True
            pnlPin.Visible = False
        Else
            pnlPassword.Visible = False
            pnlPin.Visible = True
            thisMsgBox.setError("Invalid Code!", "Authentication code Not found!")
            thisMsgBox.showConfirmBox()
        End If

    End Sub

    Protected Sub btnRegSave_Click(sender As Object, e As EventArgs) Handles btnRegister.ServerClick

        If txtRegPasword.Text <> txtRetypeRegPasword.Text Then
            thisMsgBox.setError("Invalid", "Password not match!")
        Else
            Dim _clsDB As New clsDatabase
            Dim _clsSecurity As New clsDimboMySQL.clsSecurity

            _clsDB.dbUtility.sqlString = "UPDATE tbl_payroll_employee_information SET paycheck_password='" & _clsSecurity.encryptString(txtRegPasword.Text) & "' WHERE employee_id='" & hfEmployeeID.Value & "'"
            _clsDB.dbUtility.executeUsingCommandFromSQL()

            thisMsgBox.setModalType("REGISTERED")
            thisMsgBox.setInfo("Info", "Retrieved Successfully! <br/> You can now check your Payroll.")
        End If

        thisMsgBox.showConfirmBox()

    End Sub

    Private Sub sendCode()

        Dim _clsPayslipPin As New clsPayCheckPin

        If hfContactNo.Value <> "" Then

            With _clsPayslipPin
                .employeeId = hfEmployeeID.Value
                .ipAddress = GetIPAddress()
                .pinCode = .getPin
                If Session("SENDSMS") = "Y" Then

                    Dim _smsMsg As String = ""

                    _smsMsg = "PMS Payroll Monitoring: Your authentication code is : " & .pinCode

                    Dim _clsSMSSaveDB As New clsSMSSaveDB

                    _clsSMSSaveDB.saveSMSDB("PMS PAYCHECK", hfContactNo.Value, _smsMsg)

                    .status = "SENT"

                Else
                    .status = "NOT SENT"
                End If

                .savePayCheckPin()
            End With

        End If


    End Sub

    Public Shared Function GetIPAddress() As String
        Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
        Dim sIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        If String.IsNullOrEmpty(sIPAddress) Then
            Return context.Request.ServerVariables("REMOTE_ADDR")
        Else
            Dim ipArray As String() = sIPAddress.Split(New [Char]() {","c})
            Return ipArray(0)
        End If
    End Function

#End Region

End Class
