Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPayrollRefCompensation


    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property compensationId As String

    Public Property compensationName As String

    Public Property compensationPayrollName As String

    Public Property payrollName As String

    Public Property compensationFrequency As String

    Public Property compensationBaseOn As String

    Public Property compensationAmount As String

    Public Property compensationCode As String

    Public Property compensationAccountCode As String

#End Region


    Public Sub initialize()
        _compensationId = ""
        _compensationName = ""
        _compensationPayrollName = ""
        _payrollName = ""
        _compensationFrequency = ""
        _compensationBaseOn = ""
        _compensationAmount = ""
        _compensationCode = ""
        _compensationAccountCode = ""
    End Sub


    Public Function browsePayrollRefCompensationForLog(ByVal _thisID As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT compensation_id, compensation_name, compensation_payroll_name, payroll_name, compensation_frequency, compensation_base_on, compensation_amount, compensation_code, compensation_account_code FROM tbl_payroll_ref_compensation " & _
               " WHERE compensation_id='" & _thisID & "' LIMIT 1"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_ref_compensation")
    End Function


    Public Function browsePayrollRefCompensation(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT compensation_id, compensation_name, compensation_payroll_name, payroll_name, compensation_frequency, compensation_base_on, compensation_amount, compensation_code, compensation_account_code FROM tbl_payroll_ref_compensation " & _
              " WHERE  compensation_name LIKE '%" & _criteria & "%' ORDER BY compensation_name"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_ref_compensation")
    End Function


    Public Sub savePayrollRefCompensation()
        If compensationId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "compensation_id,compensation_name,compensation_payroll_name,payroll_name,compensation_frequency,compensation_base_on,compensation_amount,compensation_code,compensation_account_code"
                .sqlString = .getSQLStatement("tbl_payroll_ref_compensation", "INSERT")
                _compensationId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 11).ToUpper
                .ADDPARAM_CMD_String("compensation_id", _compensationId)
                .ADDPARAM_CMD_String("compensation_name", _compensationName)
                .ADDPARAM_CMD_String("compensation_payroll_name", _compensationPayrollName)
                .ADDPARAM_CMD_String("payroll_name", _payrollName)
                .ADDPARAM_CMD_String("compensation_frequency", _compensationFrequency)
                .ADDPARAM_CMD_String("compensation_base_on", _compensationBaseOn)
                .ADDPARAM_CMD_String("compensation_amount", _compensationAmount)
                .ADDPARAM_CMD_String("compensation_code", _compensationCode)
                .ADDPARAM_CMD_String("compensation_account_code", _compensationAccountCode)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "compensation_name,compensation_payroll_name,payroll_name,compensation_frequency,compensation_base_on,compensation_amount,compensation_code,compensation_account_code"
                .sqlString = .getSQLStatement("tbl_payroll_ref_compensation", "UPDATE", "compensation_id")
                .ADDPARAM_CMD_String("compensation_name", _compensationName)
                .ADDPARAM_CMD_String("compensation_payroll_name", _compensationPayrollName)
                .ADDPARAM_CMD_String("payroll_name", _payrollName)
                .ADDPARAM_CMD_String("compensation_frequency", _compensationFrequency)
                .ADDPARAM_CMD_String("compensation_base_on", _compensationBaseOn)
                .ADDPARAM_CMD_String("compensation_amount", _compensationAmount)
                .ADDPARAM_CMD_String("compensation_code", _compensationCode)
                .ADDPARAM_CMD_String("compensation_account_code", _compensationAccountCode)
                .ADDPARAM_CMD_String("compensation_id", _compensationId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub getPayrollRefCompensation(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_ref_compensation WHERE compensation_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _compensationId = dt.Rows(0)("compensation_id").ToString
            _compensationName = dt.Rows(0)("compensation_name").ToString
            _compensationPayrollName = dt.Rows(0)("compensation_payroll_name").ToString
            _payrollName = dt.Rows(0)("payroll_name").ToString
            _compensationFrequency = dt.Rows(0)("compensation_frequency").ToString
            _compensationBaseOn = dt.Rows(0)("compensation_base_on").ToString
            _compensationAmount = dt.Rows(0)("compensation_amount").ToString
            _compensationCode = dt.Rows(0)("compensation_code").ToString
            _compensationAccountCode = dt.Rows(0)("compensation_account_code").ToString
        Else
            initialize()
        End If
    End Sub

End Class
