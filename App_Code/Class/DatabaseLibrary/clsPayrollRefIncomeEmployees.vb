Imports Microsoft.VisualBasic

Imports System.Data

Public Class clsPayrollRefIncomeEmployees
    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property transId As String

    Public Property incomeId As String

    Public Property employeeId As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region


    Public Sub initialize()
        _transId = ""
        _incomeId = ""
        _employeeId = ""
        _isActive = ""
        _createUser = ""
        _createDate = ""
        _lastUser = ""
        _lastDate = ""
    End Sub


    Public Function browsePayrollRefIncomeEmployees(ByVal _thisId As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT tbl_payroll_ref_income_employees.trans_id, CONCAT(last_name,', ',first_name,' ',ext_name,' ',middle_name) AS Employee_name, department_name  FROM tbl_payroll_ref_income_employees " & _
              "INNER JOIN tbl_payroll_employee_information ON tbl_payroll_ref_income_employees.employee_id = tbl_payroll_employee_information.employee_id " & _
              "INNER JOIN tbl_payroll_ref_department ON tbl_payroll_employee_information.department = tbl_payroll_ref_department.department_id " & _
              "WHERE income_id = '" & _thisId & "' AND is_active = 'Y' "
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_ref_income_employees")
    End Function


    Public Sub savePayrollRefIncomeEmployees()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,income_id,employee_id,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_income_employees", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("income_id", _incomeId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _lastUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "income_id,employee_id,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_income_employees", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("income_id", _incomeId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub getPayrollRefIncomeEmployees(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_ref_income_employees WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _incomeId = dt.Rows(0)("income_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub


End Class
