Imports Microsoft.VisualBasic

Imports System.Data


Public Class clsPayrollPeriod

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property payrollPeriodId As String

    Public Property payrollPeriodMonth As String

    Public Property payrollPeriodYear As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

#End Region


    Public Sub initialize()
        _payrollPeriodId = ""
        _payrollPeriodMonth = ""
        _payrollPeriodYear = ""
        _isActive = "Y"
        _createUser = ""
        _createDate = ""
    End Sub


    Public Function browsePayrollPeriod(ByVal _thisYear As String, _thisMonth As String) As DataTable
        Dim sql As String = ""

        Dim where As String = ""

        If _thisMonth <> "" Then
            where += "WHERE month_id = '" & _thisMonth & "' "
        End If

        sql = "SELECT COALESCE(payroll_period_id,'') AS payroll_period_id,month_id,month_name, " & _
              "(CASE WHEN COALESCE(tbl_payroll_period.payroll_period_id,'') <> '' THEN 'ACTIVE' ELSE 'INACTIVE' END) AS month_status, " & _
              "(CASE WHEN COALESCE(tbl_payroll_period.payroll_period_id,'') <> '' THEN 'FALSE' ELSE 'TRUE' END) AS activeVisible, " & _
              "(CASE WHEN COALESCE(tbl_payroll_period.payroll_period_id,'') <> '' THEN 'TRUE' ELSE 'FALSE' END) AS inActiveVisible FROM tbl_ref_month " & _
              "LEFT JOIN tbl_payroll_period ON tbl_ref_month.month_id = tbl_payroll_period.payroll_period_month AND " & _
              "tbl_payroll_period.payroll_period_year = '" & _thisYear & "' " & where & _
              "ORDER BY month_id"


        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_period")
    End Function


    Public Function browsePayrollPeriodMonths(ByVal _thisYear As String) As DataTable
        Dim sql As String = ""

        sql = "SELECT COALESCE(payroll_period_id,'') AS payroll_period_id,month_id,month_name FROM tbl_ref_month " & _
              "LEFT JOIN tbl_payroll_period ON tbl_ref_month.month_id = tbl_payroll_period.payroll_period_month AND " & _
              "tbl_payroll_period.payroll_period_year = '" & _thisYear & "' " & _
              "WHERE COALESCE(payroll_period_id,'') <> '' " & _
              "ORDER BY month_id"


        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_period")
    End Function


    Public Sub savePayrollPeriod()
        'If payrollPeriodId = "" Then
        With _clsDB.dbUtility
            .fieldItems = "payroll_period_id,payroll_period_month,payroll_period_year,is_active,create_user,create_date"
            .sqlString = .getSQLStatement("tbl_payroll_period", "INSERT")
            payrollPeriodId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 5).ToUpper
            .ADDPARAM_CMD_String("payroll_period_id", _payrollPeriodId)
            .ADDPARAM_CMD_String("payroll_period_month", _payrollPeriodMonth)
            .ADDPARAM_CMD_String("payroll_period_year", _payrollPeriodYear)
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("create_user", _lastUser)
            .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
            .executeUsingCommandFromSQL(True)
        End With 'Else
        '    With _clsDB.dbUtility
        '        .fieldItems = "payroll_period_month,payroll_period_year,is_active"
        '        .sqlString = .getSQLStatement("tbl_payroll_period", "UPDATE", "payroll_period_id")
        '        .ADDPARAM_CMD_String("payroll_period_month", _payrollPeriodMonth)
        '        .ADDPARAM_CMD_String("payroll_period_year", _payrollPeriodYear)
        '        .ADDPARAM_CMD_String("is_active", _isActive)
        '        .ADDPARAM_CMD_String("payroll_period_id", _payrollPeriodId)
        '        .executeUsingCommandFromSQL(True)
        '    End With
        'End If

    End Sub


    Public Sub deletePeriod()
        _clsDB.Delete_Record("DELETE FROM tbl_payroll_period WHERE payroll_period_id = '" & _payrollPeriodId & "'")
    End Sub


    Public Sub getPayrollPeriod(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_period WHERE payroll_period_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _payrollPeriodId = dt.Rows(0)("payroll_period_id").ToString
            _payrollPeriodMonth = dt.Rows(0)("payroll_period_month").ToString
            _payrollPeriodYear = dt.Rows(0)("payroll_period_year").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub


End Class
