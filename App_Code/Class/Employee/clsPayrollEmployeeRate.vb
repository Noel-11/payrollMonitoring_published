Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollEmployeeRate

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property transId As String

    Public Property employeeId As String

    Public Property ratePosition As String

    Public Property ratePositionDesc As String

    Public Property rateStep As String

    Public Property rateAmount As String

    Public Property ratePeriodFrom As String

    Public Property ratePeriodTo As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

#End Region

    Public Sub initialize()
        _transId = ""
        _employeeId = ""
        _ratePosition = ""
        _ratePositionDesc = ""
        _rateStep = "1"
        _rateAmount = "0"
        _ratePeriodFrom = Nothing
        _ratePeriodTo = Nothing
        _isActive = "Y"
        _createUser = ""
        _createDate = ""
    End Sub


    Public Function browsePayrollEmployeeRate(ByVal _thisId As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT trans_id, employee_id, rate_position,rate_position_desc, rate_step, rate_amount, rate_period_from, rate_period_to, is_active, create_user, create_date FROM tbl_payroll_employee_rate " & _
              "WHERE is_active = 'Y' AND employee_id = '" & _thisId & "' ORDER BY rate_period_to DESC "
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_rate")
    End Function

    Public Sub savePayrollEmployeeRate()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,employee_id,rate_position,rate_position_desc,rate_step,rate_amount,rate_period_from,rate_period_to,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_payroll_employee_rate", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("rate_position", _ratePosition)
                .ADDPARAM_CMD_String("rate_position_desc", _ratePositionDesc)
                .ADDPARAM_CMD_String("rate_step", _rateStep)
                .ADDPARAM_CMD_String("rate_amount", _rateAmount)
                .ADDPARAM_CMD_String("rate_period_from", _ratePeriodFrom)
                .ADDPARAM_CMD_String("rate_period_to", _ratePeriodTo)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _lastUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "employee_id,rate_position,rate_position_desc,rate_step,rate_amount,rate_period_from,rate_period_to,is_active"
                .sqlString = .getSQLStatement("tbl_payroll_employee_rate", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("rate_position", _ratePosition)
                .ADDPARAM_CMD_String("rate_position_desc", _ratePositionDesc)
                .ADDPARAM_CMD_String("rate_step", _rateStep)
                .ADDPARAM_CMD_String("rate_amount", _rateAmount)
                .ADDPARAM_CMD_String("rate_period_from", _ratePeriodFrom)
                .ADDPARAM_CMD_String("rate_period_to", _ratePeriodTo)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub

    Public Sub updateIsActive()

        With _clsDB.dbUtility
            .fieldItems = "is_active"
            .sqlString = .getSQLStatement("tbl_payroll_employee_rate", "UPDATE", "trans_id")
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("trans_id", _transId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub


    Public Sub getPayrollEmployeeRate(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_employee_rate WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _ratePosition = dt.Rows(0)("rate_position").ToString
            _rateStep = dt.Rows(0)("rate_step").ToString
            _rateAmount = dt.Rows(0)("rate_amount").ToString
            _ratePeriodFrom = dt.Rows(0)("rate_period_from").ToString
            _ratePeriodTo = dt.Rows(0)("rate_period_to").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub

End Class
