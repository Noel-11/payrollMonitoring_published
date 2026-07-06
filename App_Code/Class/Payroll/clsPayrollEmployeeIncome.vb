Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollEmployeeIncome

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property employeeIncomeId As String

    Public Property employeeId As String

    Public Property compensationId As String

    Public Property incomeAmount As String

    Public Property incomeStart As String

    Public Property incomeEnd As String

    Public Property sortOrder As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region

    Public Sub initialize()
        _employeeIncomeId = ""
        _employeeId = ""
        _compensationId = ""
        _incomeAmount = ""
        _incomeStart = Nothing
        _incomeEnd = Nothing
        _sortOrder = "0"
        _isActive = "Y"
        _createUser = ""
        _createDate = ""
        _lastUser = ""
        _lastDate = ""
    End Sub


    Public Function browsePayrollEmployeeIncome(ByVal _thisId As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT employee_income_id, compensation_name, employee_id, income_amount, DATE_FORMAT(income_start,'%m/%d/%Y') AS income_start, DATE_FORMAT(income_end,'%m/%d/%Y') AS income_end FROM tbl_payroll_employee_income " & _
              "INNER JOIN tbl_payroll_ref_compensation ON tbl_payroll_employee_income.compensation_id = tbl_payroll_ref_compensation.compensation_id " & _
              "WHERE is_active = 'Y' AND employee_id = '" & _thisId & "' "
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_income")
    End Function


    Public Sub savePayrollEmployeeIncome()
        ' If employeeIncomeId = "" Then
        With _clsDB.dbUtility
            .fieldItems = "employee_income_id,employee_id,compensation_id,income_amount,income_start,income_end,sort_order,is_active,create_user,create_date"
            .sqlString = .getSQLStatement("tbl_payroll_employee_income", "INSERT")
            _employeeIncomeId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 10).ToUpper
            .ADDPARAM_CMD_String("employee_income_id", _employeeIncomeId)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .ADDPARAM_CMD_String("compensation_id", _compensationId)
            .ADDPARAM_CMD_String("income_amount", _incomeAmount)
            .ADDPARAM_CMD_String("income_start", _incomeStart)
            .ADDPARAM_CMD_String("income_end", _incomeEnd)
            .ADDPARAM_CMD_String("sort_order", _sortOrder)
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("create_user", _lastUser)
            .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
            .executeUsingCommandFromSQL(True)
        End With
        'Else
        '   
        'End If
    End Sub

    Public Sub updatePayrollEmployeeIncome()
        With _clsDB.dbUtility
            .fieldItems = "income_amount,income_start,income_end,last_user,last_date"
            .sqlString = .getSQLStatement("tbl_payroll_employee_income", "UPDATE", "employee_income_id")
            .ADDPARAM_CMD_String("income_amount", _incomeAmount)
            .ADDPARAM_CMD_String("income_start", _incomeStart)
            .ADDPARAM_CMD_String("income_end", _incomeEnd)
            .ADDPARAM_CMD_String("last_user", _lastUser)
            .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
            .ADDPARAM_CMD_String("employee_income_id", _employeeIncomeId)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub

    Public Sub deletePayrollEmployeeIncome(ByVal _id As String)
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_employee_income WHERE employee_income_id='" & _id & "' LIMIT 1")

    End Sub


    Public Sub getPayrollEmployeeIncome(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_employee_income WHERE employee_income_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _employeeIncomeId = dt.Rows(0)("employee_income_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _compensationId = dt.Rows(0)("compensation_id").ToString
            _incomeAmount = dt.Rows(0)("income_amount").ToString
            _incomeStart = dt.Rows(0)("income_start").ToString
            _incomeEnd = dt.Rows(0)("income_end").ToString
            _sortOrder = dt.Rows(0)("sort_order").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub

End Class
