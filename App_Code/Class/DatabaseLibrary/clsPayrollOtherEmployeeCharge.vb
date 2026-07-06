Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollOtherEmployeeCharge

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property chargeId As String

    Public Property chargeDepartmentFrom As String

    Public Property chargeDepartmentTo As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region


    Public Sub initialize()
        _transId = ""
        _chargeId = ""
        _chargeDepartmentFrom = ""
        _chargeDepartmentTo = ""
        _isActive = ""
        _createUser = ""
        _createDate = ""
        _lastUser = ""
        _lastDate = ""
    End Sub


    Public Function browsePayrollOtherEmployeeCharge(ByVal _thisId As String) As DataTable
        Dim sql As String = ""

        sql = "SELECT tbl_payroll_other_employee_charge.trans_id, charge_id, charge_department_from, charge_department_to, department_name FROM tbl_payroll_other_employee_charge " & _
              "INNER JOIN tbl_payroll_ref_department ON tbl_payroll_other_employee_charge.charge_department_to = tbl_payroll_ref_department.department_id " & _
              "WHERE charge_id = '" & _thisId & "' AND tbl_payroll_other_employee_charge.is_active = 'Y'"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_other_employee_charge")

    End Function


    Public Sub savePayrollOtherEmployeeCharge()
        With _clsDB.dbUtility
            .fieldItems = "trans_id,charge_id,charge_department_from,charge_department_to,is_active,create_user,create_date"
            .sqlString = .getSQLStatement("tbl_payroll_other_employee_charge", "INSERT")
            _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
            .ADDPARAM_CMD_String("trans_id", _transId)
            .ADDPARAM_CMD_String("charge_id", _chargeId)
            .ADDPARAM_CMD_String("charge_department_from", _chargeDepartmentFrom)
            .ADDPARAM_CMD_String("charge_department_to", _chargeDepartmentTo)
            .ADDPARAM_CMD_String("is_active", "Y")
            .ADDPARAM_CMD_String("create_user", _lastUser)
            .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub updateIsActive()

        With _clsDB.dbUtility
            .fieldItems = "is_active,last_user,last_date"
            .sqlString = .getSQLStatement("tbl_payroll_other_employee_charge", "UPDATE", "trans_id")
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("last_user", _lastUser)
            .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
            .ADDPARAM_CMD_String("trans_id", _transId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub


    Public Sub getPayrollOtherEmployeeCharge(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_other_employee_charge WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _chargeId = dt.Rows(0)("charge_id").ToString
            _chargeDepartmentFrom = dt.Rows(0)("charge_department_from").ToString
            _chargeDepartmentTo = dt.Rows(0)("charge_department_to").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub



End Class
