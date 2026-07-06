Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPayrollEmployeeDeductions


    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property employeeDeductionId As String

    Public Property employeeId As String

    Public Property deductionId As String

    Public Property deductionReferenceCode As String

    Public Property deductionDetails As String

    Public Property deductionAmount As String

    Public Property deductionMode As String

    Public Property deductTo As String

    Public Property deductionStart As String

    Public Property deductionEnd As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

#End Region

    Public Sub initialize()
        _employeeDeductionId = ""
        _employeeId = ""
        _deductionId = ""
        _deductionReferenceCode = ""
        _deductionDetails = ""
        _deductionAmount = ""
        _deductionMode = ""
        _deductTo = "SALARY"
        _deductionStart = ""
        _deductionEnd = ""
        _isActive = "Y"
        _createUser = ""
        _createDate = ""
    End Sub


    Public Function browsePayrollEmployeeDeductionsForLog(ByVal _thisID As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT employee_deduction_id, employee_id, deduction_id, deduction_details, deduction_amount, deduction_mode, deduction_start, deduction_end, is_active, create_user, create_date FROM tbl_payroll_employee_deductions " & _
        " WHERE employee_deduction_id='" & _thisID & "' LIMIT 1"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_deductions")
    End Function


    Public Function browsePayrollEmployeeDeductions(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT employee_deduction_id,  deduction_name, deduction_details, deduction_amount, deduction_mode, deduction_start, deduction_end, " & _
              "(CASE WHEN deduction_role = 'OPTIONAL' THEN 'TRUE' ELSE 'FALSE' END) AS isOptional,deduction_role FROM tbl_payroll_employee_deductions " & _
              "INNER JOIN tbl_payroll_ref_deduction ON tbl_payroll_employee_deductions.deduction_id= tbl_payroll_ref_deduction.deduction_id " & _
              " WHERE  employee_id = '" & _criteria & "' AND tbl_payroll_employee_deductions.is_active = 'Y' "
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_deductions")
    End Function


    Public Sub savePayrollEmployeeDeductions()
        If employeeDeductionId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "employee_deduction_id,deduction_reference_code,employee_id,deduction_id,deduction_details,deduction_amount,deduction_mode,deduct_to,deduction_start,deduction_end,is_active,create_user"
                .sqlString = .getSQLStatement("tbl_payroll_employee_deductions", "INSERT")
                _employeeDeductionId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 11).ToUpper
                .ADDPARAM_CMD_String("employee_deduction_id", _employeeDeductionId)
                .ADDPARAM_CMD_String("deduction_reference_code", _deductionReferenceCode)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("deduction_id", _deductionId)
                .ADDPARAM_CMD_String("deduction_details", _deductionDetails)
                .ADDPARAM_CMD_String("deduction_amount", _deductionAmount)
                .ADDPARAM_CMD_String("deduction_mode", _deductionMode)
                .ADDPARAM_CMD_String("deduct_to", _deductTo)
                .ADDPARAM_CMD_String("deduction_start", _deductionStart)
                .ADDPARAM_CMD_String("deduction_end", _deductionEnd)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _createUser)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "deduction_reference_code,deduction_details,deduction_amount,deduction_mode,deduct_to,deduction_start,deduction_end,is_active"
                .sqlString = .getSQLStatement("tbl_payroll_employee_deductions", "UPDATE", "employee_deduction_id")
                .ADDPARAM_CMD_String("deduction_reference_code", _deductionReferenceCode)
                .ADDPARAM_CMD_String("deduction_details", _deductionDetails)
                .ADDPARAM_CMD_String("deduction_amount", _deductionAmount)
                .ADDPARAM_CMD_String("deduction_mode", _deductionMode)
                .ADDPARAM_CMD_String("deduct_to", _deductTo)
                .ADDPARAM_CMD_String("deduction_start", _deductionStart)
                .ADDPARAM_CMD_String("deduction_end", _deductionEnd)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("employee_deduction_id", _employeeDeductionId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub updatePayrollEmployeeDeductionsRemittance()
        With _clsDB.dbUtility
            .fieldItems = "deduction_amount,deduction_end"
            .sqlString = .getSQLStatement("tbl_payroll_employee_deductions", "UPDATE", "employee_deduction_id")
            .ADDPARAM_CMD_String("deduction_amount", _deductionAmount)
            .ADDPARAM_CMD_String("deduction_end", _deductionEnd)
            .ADDPARAM_CMD_String("employee_deduction_id", _employeeDeductionId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub updateMandatoryDeduction()

        With _clsDB.dbUtility
            .fieldItems = "deduction_reference_code,deduction_start,deduction_end,deduction_amount"
            .sqlString = .getSQLStatement("tbl_payroll_employee_deductions", "UPDATE", "employee_deduction_id")
            .ADDPARAM_CMD_String("deduction_reference_code", _deductionReferenceCode)
            .ADDPARAM_CMD_String("deduction_start", _deductionStart)
            .ADDPARAM_CMD_String("deduction_end", _deductionEnd)
            .ADDPARAM_CMD_String("deduction_amount", _deductionAmount)
            .ADDPARAM_CMD_String("employee_deduction_id", _employeeDeductionId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updateMandatoryDeductionIsActive()

        With _clsDB.dbUtility
            .fieldItems = "is_active"
            .sqlString = .getSQLStatement("tbl_payroll_employee_deductions", "UPDATE", "employee_id,deduction_id", "AND")
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .ADDPARAM_CMD_String("deduction_id", _deductionId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub getPayrollEmployeeDeductions(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_employee_deductions WHERE employee_deduction_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _employeeDeductionId = dt.Rows(0)("employee_deduction_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _deductionId = dt.Rows(0)("deduction_id").ToString
            _deductionDetails = dt.Rows(0)("deduction_details").ToString
            _deductionAmount = dt.Rows(0)("deduction_amount").ToString
            _deductionMode = dt.Rows(0)("deduction_mode").ToString
            _deductionStart = dt.Rows(0)("deduction_start").ToString
            _deductionEnd = dt.Rows(0)("deduction_end").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub

    Public Sub deletePayrollEmployeeDeductions(ByVal _id As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_employee_deductions WHERE employee_deduction_id='" & _id & "' LIMIT 1")
    End Sub


    Public Sub deletePayrollEmployeeDeductionsType(ByVal _empId As String, ByVal _thisDeduction As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_employee_deductions WHERE employee_id='" & _empId & "' AND deduction_id = '" & _thisDeduction & "'")
    End Sub

End Class
