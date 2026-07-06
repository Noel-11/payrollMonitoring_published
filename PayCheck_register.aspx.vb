Imports System.Data
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports Microsoft.Reporting.WebForms
Partial Class PaySlip_register
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
            pnlPassword.Visible = False
            pnlPin.Visible = False
        End If

        _btnOK = thisMsgBox.FindControl("btnMsgBoxNo")
        AddHandler _btnOK.ServerClick, AddressOf btnOK_Click

    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If thisMsgBox.getModalType = "REGISTERED" Then
            Response.Redirect("Payroll_check.aspx")
        End If

    End Sub

#Region "Register"

    Protected Sub btnRegRetrieve_Click(sender As Object, e As EventArgs) Handles btnRegRetrieve.ServerClick
        getRegEmployeeDetails()
    End Sub

    Public Sub getRegEmployeeDetails()
        Try
            Dim _clsDB As New clsDatabase

            Dim dt As New DataTable

            dt = _clsDB.Fill_DataTable("SELECT employee_id,contact_no FROM tbl_payroll_employee_information WHERE agency_id=" & txtRegEmployeeID.Text.Trim.Replace("'", "").Replace("--", "").Replace(";", "") & " AND last_name='" & txtRegLastName.Text.Trim.ToUpper.Replace("'", "").Replace("--", "").Replace(";", "") & "' AND first_name='" & txtRegFirstName.Text.Trim.ToUpper.Replace("'", "").Replace("--", "").Replace(";", "") & "' LIMIT 1")

            If dt.Rows.Count > 0 Then
                hfEmployeeID.Value = dt.Rows(0)("employee_id").ToString
                hfContactNo.Value = dt.Rows(0)("contact_no").ToString
            Else
                hfEmployeeID.Value = ""
                hfContactNo.Value = ""
            End If

            lblPinContact.Text = ""

            If hfEmployeeID.Value = "" Then
                thisMsgBox.setError("NOT FOUND!", "Employee Record Not Found")
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
                    thisMsgBox.setError("Cannot Verify", "No Contact Number found! <br/> ask the accounting office to record your contact number.")
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
            'Dim _clsSecurity As New clsDimboMySQL.clsSecurity

            '_clsDB.dbUtility.sqlString = "UPDATE tbl_payroll_employee_information SET paycheck_password='" & _clsSecurity.encryptString(txtRegPasword.Text.Trim) & "' WHERE employee_id='" & hfEmployeeID.Value & "'"
            '_clsDB.dbUtility.executeUsingCommandFromSQL()

            Dim _clsEmpInfo As New clsPayrollEmployeeInformation

            With _clsEmpInfo
                .employeeId = hfEmployeeID.Value
                .payCheckPassword = txtRegPasword.Text.Trim
                .updatePaycheckPass()
            End With

            thisMsgBox.setModalType("REGISTERED")
            thisMsgBox.setInfo("Info", "Registered Successfully! <br/> You can now check your Payroll.")
        End If

        thisMsgBox.showConfirmBox()

    End Sub

#End Region


#Region "PINCODE"

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
