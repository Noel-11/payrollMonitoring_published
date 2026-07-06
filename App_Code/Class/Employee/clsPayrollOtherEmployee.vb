Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollOtherEmployee

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property otherEmpId As String

    Public Property employeeId As String

    Public Property otherEmployeeName As String

    Public Property otherEmployeePosition As String

    Public Property otherEmployeePositionStep As String

    Public Property otherEmployeePayrollClassification As String

    Public Property departmentFrom As String

    Public Property departmentTo As String

    Public Property lastUser As String

    Public Property createUser As String

    Public Property createDate As String

#End Region

    Public Sub initialize()
        _otherEmpId = ""
        _employeeId = ""
        _otherEmployeeName = ""
        _otherEmployeePosition = ""
        _otherEmployeePositionStep = ""
        _otherEmployeePayrollClassification = ""
        _departmentFrom = ""
        _departmentTo = ""
        _createUser = ""
        _createDate = ""
        _lastUser = ""
    End Sub

    Public Function browsePayrollOtherEmployee(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT other_emp_id, employee_id, other_employee_name, other_employee_position, other_employee_position_step, other_employee_payroll_classification, department_from, create_user, create_date, FROM tbl_payroll_other_employee " & _
        " WHERE other_emp_id LIKE '%" & _criteria & "%' OR employee_id LIKE '%" & _criteria & "%' OR other_employee_name LIKE '%" & _criteria & "%' OR other_employee_position LIKE '%" & _criteria & "%' OR other_employee_position_step LIKE '%" & _criteria & "%' OR other_employee_payroll_classification LIKE '%" & _criteria & "%' OR department_from LIKE '%" & _criteria & "%' OR create_user LIKE '%" & _criteria & "%' OR create_date LIKE '%" & _criteria & "%' OR  ORDER BY "
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_other_employee")
    End Function

    Public Sub savePayrollOtherEmployee()
        If otherEmpId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "other_emp_id,employee_id,other_employee_name,other_employee_position,other_employee_position_step,other_employee_payroll_classification,department_from,department_to,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_payroll_other_employee", "INSERT")
                _otherEmpId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 10).ToUpper
                .ADDPARAM_CMD_String("other_emp_id", _otherEmpId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("other_employee_name", _otherEmployeeName)
                .ADDPARAM_CMD_String("other_employee_position", _otherEmployeePosition)
                .ADDPARAM_CMD_String("other_employee_position_step", _otherEmployeePositionStep)
                .ADDPARAM_CMD_String("other_employee_payroll_classification", _otherEmployeePayrollClassification)
                .ADDPARAM_CMD_String("department_from", _departmentFrom)
                .ADDPARAM_CMD_String("department_to", _departmentTo)
                .ADDPARAM_CMD_String("create_user", _lastUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "other_employee_name,other_employee_position,other_employee_position_step,other_employee_payroll_classification,department_from,department_to"
                .sqlString = .getSQLStatement("tbl_payroll_other_employee", "UPDATE", "employee_id")
                .ADDPARAM_CMD_String("other_employee_name", _otherEmployeeName)
                .ADDPARAM_CMD_String("other_employee_position", _otherEmployeePosition)
                .ADDPARAM_CMD_String("other_employee_position_step", _otherEmployeePositionStep)
                .ADDPARAM_CMD_String("other_employee_payroll_classification", _otherEmployeePayrollClassification)
                .ADDPARAM_CMD_String("department_from", _departmentFrom)
                .ADDPARAM_CMD_String("department_to", _departmentTo)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .executeUsingCommandFromSQL(True)
            End With
        End If

    End Sub

    Public Sub updatePayrollOtherDepartmentEmp()

        With _clsDB.dbUtility
            .fieldItems = "other_employee_name,other_employee_position,other_employee_position_step,other_employee_payroll_classification,department_from"
            .sqlString = .getSQLStatement("tbl_payroll_other_employee", "UPDATE", "employee_id")
            .ADDPARAM_CMD_String("other_employee_name", _otherEmployeeName)
            .ADDPARAM_CMD_String("other_employee_position", _otherEmployeePosition)
            .ADDPARAM_CMD_String("other_employee_position_step", _otherEmployeePositionStep)
            .ADDPARAM_CMD_String("other_employee_payroll_classification", _otherEmployeePayrollClassification)
            .ADDPARAM_CMD_String("department_from", _departmentFrom)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub deletePayrollOtherEmployee(ByVal _thisId As String)

        _clsDB.Delete_Record("DELETE FROM tbl_payroll_other_employee WHERE employee_id = '" & _thisId & "' LIMIT 1")

    End Sub

    Public Sub getPayrollOtherEmployee(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_other_employee WHERE employee_id ='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _otherEmpId = dt.Rows(0)("other_emp_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _otherEmployeeName = dt.Rows(0)("other_employee_name").ToString
            _otherEmployeePosition = dt.Rows(0)("other_employee_position").ToString
            _otherEmployeePositionStep = dt.Rows(0)("other_employee_position_step").ToString
            _otherEmployeePayrollClassification = dt.Rows(0)("other_employee_payroll_classification").ToString
            _departmentFrom = dt.Rows(0)("department_from").ToString
            _departmentTo = dt.Rows(0)("department_to").ToString
        Else
            initialize()
        End If
    End Sub


End Class
