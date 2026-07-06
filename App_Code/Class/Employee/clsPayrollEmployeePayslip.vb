Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPayrollEmployeePayslip

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property employeeId As String

    Public Property employeeName As String

    Public Property employeePosition As String

    Public Property employeeDepartment As String

    Public Property employeeRate As String

    Public Property employeeStatus As String

    Public Property payslipPeriod As String

    Public Property payslipGross As String

    Public Property payslipNetIncome As String

    Public Property payslipCode As Byte()

    Public Property isActive As String

    Public Property createDate As String

    Public Property createUser As String

    Public Property lastUser As String



#End Region


    Public Sub initialize()
        _transId = ""
        _employeeId = ""
        _employeeName = ""
        _employeePosition = ""
        _employeeDepartment = ""
        _employeeRate = ""
        _employeeStatus = ""
        _payslipPeriod = ""
        _payslipCode = Nothing
        _payslipGross = "0.00"
        _payslipNetIncome = "0.00"
        _isActive = "Y"
        _createDate = ""
        _createUser = ""
        _lastUser = ""
    End Sub


    Public Function browsePayrollEmployeePayslip(ByVal _criteria As String) As DataTable
        Dim sql As String = ""

        sql = "SELECT trans_id, employee_id, employee_name, employee_position, employee_department, employee_rate, employee_status, " & _
              "payslip_period, payslip_code, is_active, create_date, create_user FROM tbl_payroll_employee_payslip " & _
              "WHERE is_active = 'Y' "

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_payslip")
    End Function


    Public Sub savePayrollEmployeePayslip()
        ' If transId = "" Then

        With _clsDB.dbUtility
            .fieldItems = "trans_id,employee_id,employee_name,employee_position,employee_department,employee_rate,employee_status,payslip_period,payslip_gross,payslip_net_income,is_active,create_date,create_user"
            .sqlString = .getSQLStatement("tbl_payroll_employee_payslip", "INSERT")
            _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
            .ADDPARAM_CMD_String("trans_id", _transId)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .ADDPARAM_CMD_String("employee_name", _employeeName)
            .ADDPARAM_CMD_String("employee_position", _employeePosition)
            .ADDPARAM_CMD_String("employee_department", _employeeDepartment)
            .ADDPARAM_CMD_String("employee_rate", _employeeRate)
            .ADDPARAM_CMD_String("employee_status", _employeeStatus)
            .ADDPARAM_CMD_String("payslip_period", _payslipPeriod)
            .ADDPARAM_CMD_String("payslip_gross", _payslipGross)
            .ADDPARAM_CMD_String("payslip_net_income", _payslipNetIncome)
            ' .ADDPARAM_CMD_String("payslip_code", _payslipCode)
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
            .ADDPARAM_CMD_String("create_user", _lastUser)
            .executeUsingCommandFromSQL(True)
        End With

        'Else
        '    With _clsDB.dbUtility
        '        .fieldItems = "employee_id,employee_name,employee_position,employee_department,employee_rate,employee_status,payslip_period,payslip_code,is_active"
        '        .sqlString = .getSQLStatement("tbl_payroll_employee_payslip", "UPDATE", "trans_id")
        '        .ADDPARAM_CMD_String("employee_id", _employeeId)
        '        .ADDPARAM_CMD_String("employee_name", _employeeName)
        '        .ADDPARAM_CMD_String("employee_position", _employeePosition)
        '        .ADDPARAM_CMD_String("employee_department", _employeeDepartment)
        '        .ADDPARAM_CMD_String("employee_rate", _employeeRate)
        '        .ADDPARAM_CMD_String("employee_status", _employeeStatus)
        '        .ADDPARAM_CMD_String("payslip_period", _payslipPeriod)
        '        .ADDPARAM_CMD_String("payslip_code", _payslipCode)
        '        .ADDPARAM_CMD_String("is_active", _isActive)
        '        .ADDPARAM_CMD_String("trans_id", _transId)
        '        .executeUsingCommandFromSQL(True)
        '    End With
        'End If
    End Sub

    Public Sub updatePayslipCode()

        If _transId <> "" Then
            Try
                Dim dt As New DataTable
                Dim sql As String = ""

                sql = "SELECT AES_ENCRYPT('" & _transId & "','PMSCDOPAYSLIP_2024') AS payslipCode"

                dt = _clsDB.Fill_DataTable(sql)

                _payslipCode = dt.Rows(0)("payslipCode")

                With _clsDB.dbUtility
                    .fieldItems = "payslip_code"
                    .sqlString = .getSQLStatement("tbl_payroll_employee_payslip", "UPDATE", "trans_id")
                    .ADDPARAM_CMD_String("payslip_code", _payslipCode)
                    .ADDPARAM_CMD_String("trans_id", _transId)
                    ' .executeUsingCommandFromSQL(True)
                End With
            Catch ex As Exception

            End Try

        End If

    End Sub


    Public Sub getPayrollEmployeePayslip(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_employee_payslip WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _employeeName = dt.Rows(0)("employee_name").ToString
            _employeePosition = dt.Rows(0)("employee_position").ToString
            _employeeDepartment = dt.Rows(0)("employee_department").ToString
            _employeeRate = dt.Rows(0)("employee_rate").ToString
            _employeeStatus = dt.Rows(0)("employee_status").ToString
            _payslipPeriod = dt.Rows(0)("payslip_period").ToString
            ' _payslipCode = dt.Rows(0)("payslip_code").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub


End Class
