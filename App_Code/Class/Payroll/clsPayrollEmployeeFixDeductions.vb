Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPayrollEmployeeFixDeductions


    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property transId As String

    Public Property employeeId As String

    Public Property fixDeductionId As String

    Public Property deductToEmployee As String

    Public Property deductAmount As String

#End Region

    Public Sub initialize()
        _transId = ""
        _employeeId = ""
        _fixDeductionId = ""
        _deductToEmployee = ""
        _deductAmount = "0.00"
    End Sub

    Public Function browsePayrollEmployeeFixDeductionsForLog(ByVal _thisID As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT trans_id, employee_id, fix_deduction_id, deduct_to_employee, deduction_amount FROM tbl_payroll_employee_fix_deductions " & _
        " WHERE trans_id='" & _thisID & "' LIMIT 1"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_fix_deductions")
    End Function

    Public Function browsePayrollEmployeeFixDeductions(ByVal _criteria As String) As DataTable

        Dim sql As String = ""

        sql = "SELECT trans_id, tbl_payroll_employee_fix_deductions.employee_id, deduction_name, CASE WHEN deduct_to_employee='Y' THEN 1 ELSE 0 END AS deduct_to_employee, " & _
              "(CASE WHEN deduction_amount <> '0.00' THEN deduction_amount ELSE '0.00' END) AS deduction_amount,computation_mode, " & _
              "(CASE WHEN fix_deduction_id = 'WITHHOLDING TAX' THEN 'FALSE' ELSE 'TRUE' END) AS canSaveIsDeduct, " & _
              "(CASE WHEN fix_deduction_id = 'WITHHOLDING TAX' THEN 'FALSE' ELSE 'TRUE' END) AS canSaveButton, " & _
              "(CASE WHEN fix_deduction_id = 'WITHHOLDING TAX' THEN 'FALSE' WHEN fix_deduction_id = 'GSIS PS' AND employee_payroll_classification <> 'JOB ORDER' THEN 'FALSE' WHEN fix_deduction_id = 'PAGIBIG PS' AND employee_payroll_classification <> 'JOB ORDER' THEN 'FALSE' WHEN fix_deduction_id = 'PHILHEALTH PS' AND employee_payroll_classification <> 'JOB ORDER' THEN 'FALSE' ELSE 'TRUE' END) AS canSaveAmount " & _
              "FROM tbl_payroll_employee_fix_deductions " & _
              "INNER JOIN tbl_payroll_ref_fix_deduction ON tbl_payroll_employee_fix_deductions.fix_deduction_id = tbl_payroll_ref_fix_deduction.deduction_id " & _
              "INNER JOIN tbl_payroll_employee_information ON tbl_payroll_employee_fix_deductions.employee_id = tbl_payroll_employee_information.employee_id " & _
              "WHERE  tbl_payroll_employee_fix_deductions.employee_id = '" & _criteria & "'"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_fix_deductions")

    End Function

    Public Function browsePayrollProcessEmployeeFixDeductions(ByVal _criteria As String) As DataTable

        Dim sql As String = ""

        sql = "SELECT trans_id, tbl_payroll_employee_fix_deductions.employee_id, deduction_name, CASE WHEN deduct_to_employee='Y' THEN 1 ELSE 0 END AS deduct_to_employee, " & _
              "deduction_amount,computation_mode, fix_deduction_id, deduction_payroll_name, deduction_type,tbl_payroll_ref_fix_deduction.agency_id, is_philhealth_exempt,deduction_code FROM tbl_payroll_employee_fix_deductions " & _
              "INNER JOIN tbl_payroll_employee_information ON tbl_payroll_employee_fix_deductions.employee_id = tbl_payroll_employee_information.employee_id " & _
              "INNER JOIN tbl_payroll_ref_fix_deduction ON  tbl_payroll_employee_fix_deductions.fix_deduction_id = tbl_payroll_ref_fix_deduction.deduction_id " & _
              "WHERE tbl_payroll_employee_fix_deductions.employee_id = '" & _criteria & "' AND deduct_to_employee = 'Y' "

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_fix_deductions")

    End Function


    Public Sub savePayrollEmployeeFixDeductions()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,employee_id,fix_deduction_id,deduct_to_employee"
                .sqlString = .getSQLStatement("tbl_payroll_employee_fix_deductions", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 11).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("fix_deduction_id", _fixDeductionId)
                .ADDPARAM_CMD_String("deduct_to_employee", _deductToEmployee)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "deduct_to_employee"
                .sqlString = .getSQLStatement("tbl_payroll_employee_fix_deductions", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("deduct_to_employee", _deductToEmployee)
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub
    Public Sub updatePayrollEmployeeFixDeductions()

        With _clsDB.dbUtility
            .fieldItems = "deduct_to_employee"
            .sqlString = .getSQLStatement("tbl_payroll_employee_fix_deductions", "UPDATE", "trans_id")
            .ADDPARAM_CMD_String("deduct_to_employee", _deductToEmployee)
            .ADDPARAM_CMD_String("trans_id", _transId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePayrollEmployeeFixDeductionsAmount()

        With _clsDB.dbUtility
            .fieldItems = "deduction_amount"
            .sqlString = .getSQLStatement("tbl_payroll_employee_fix_deductions", "UPDATE", "trans_id")
            .ADDPARAM_CMD_String("deduction_amount", _deductAmount)
            .ADDPARAM_CMD_String("trans_id", _transId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updatePayrollEmployeeFixDeductionsAmountByEmpId()

        With _clsDB.dbUtility
            .fieldItems = "deduction_amount"
            .sqlString = .getSQLStatement("tbl_payroll_employee_fix_deductions", "UPDATE", "trans_id")
            .ADDPARAM_CMD_String("deduction_amount", _deductAmount)
            .ADDPARAM_CMD_String("trans_id", _transId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub getPayrollEmployeeFixDeductions(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_employee_fix_deductions WHERE trans_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _fixDeductionId = dt.Rows(0)("fix_deduction_id").ToString
            _deductToEmployee = dt.Rows(0)("deduct_to_employee").ToString
        Else
            initialize()
        End If
    End Sub

    Public Sub updatePayrollEmployeeFixDeductionsIsDeduct()
        With _clsDB.dbUtility
            .fieldItems = "deduct_to_employee"
            .sqlString = .getSQLStatement("tbl_payroll_employee_fix_deductions", "UPDATE", "employee_id,fix_deduction_id", "AND")
            .ADDPARAM_CMD_String("deduction_amount", _deductToEmployee)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .ADDPARAM_CMD_String("fix_deduction_id", _fixDeductionId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub deletePayrollEmployeeFixDeductionsType(ByVal _empId As String, ByVal _thisDeduction As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_employee_fix_deductions WHERE employee_id='" & _empId & "' AND fix_deduction_id = '" & _thisDeduction & "'")
    End Sub

End Class
