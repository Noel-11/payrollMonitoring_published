Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollRefIncomeDetails

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property incomeDetailsId As String

    Public Property incomeId As String

    Public Property compensationId As String

    Public Property deductionCode As String

    Public Property compensationName As String

    Public Property compensationAgency As String

    Public Property sortOrder As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

    Public Property isActive As String

#End Region


    Public Sub initialize()

        _incomeDetailsId = ""
        _incomeId = ""
        _compensationId = ""
        _deductionCode = ""
        _compensationName = ""
        _compensationAgency = ""
        _sortOrder = ""
        _createUser = ""
        _createDate = ""
        _lastUser = ""
        _lastDate = ""
        _isActive = "Y"

    End Sub

    Public Function browsePayrollRefIncomeDetails(ByVal _thisId As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT * FROM tbl_payroll_ref_income_details " & _
              "WHERE income_id = '" & _thisId & "' AND is_active = 'Y' "
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_ref_income_details")
    End Function


    Public Sub savePayrollRefIncomeDetails()
        If incomeDetailsId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "income_details_id,income_id,compensation_id,deduction_code,compensation_name,compensation_agency,sort_order,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_income_details", "INSERT")
                _incomeDetailsId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 5).ToUpper
                .ADDPARAM_CMD_String("income_details_id", _incomeDetailsId)
                .ADDPARAM_CMD_String("income_id", _incomeId)
                .ADDPARAM_CMD_String("compensation_id", _compensationId)
                .ADDPARAM_CMD_String("deduction_code", _deductionCode)
                .ADDPARAM_CMD_String("compensation_name", _compensationName)
                .ADDPARAM_CMD_String("compensation_agency", _compensationAgency)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _lastUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "income_id,compensation_id,deduction_code,compensation_name,compensation_agency,sort_order,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_income_details", "UPDATE", "income_details_id")
                .ADDPARAM_CMD_String("income_id", _incomeId)
                .ADDPARAM_CMD_String("compensation_id", _compensationId)
                .ADDPARAM_CMD_String("deduction_code", _deductionCode)
                .ADDPARAM_CMD_String("compensation_name", _compensationName)
                .ADDPARAM_CMD_String("compensation_agency", _compensationAgency)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .ADDPARAM_CMD_String("income_details_id", _incomeDetailsId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub updateIsActive()

        With _clsDB.dbUtility
            .fieldItems = "is_active,last_user,last_date"
            .sqlString = .getSQLStatement("tbl_payroll_ref_income_details", "UPDATE", "income_details_id")
            .ADDPARAM_CMD_String("is_active", _isActive)
            .ADDPARAM_CMD_String("last_user", _lastUser)
            .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
            .ADDPARAM_CMD_String("income_details_id", _incomeDetailsId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub


    Public Sub getPayrollRefIncomeDetails(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_ref_income_details WHERE income_details_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _incomeDetailsId = dt.Rows(0)("income_details_id").ToString
            _incomeId = dt.Rows(0)("income_id").ToString
            _compensationId = dt.Rows(0)("compensation_id").ToString
            _deductionCode = dt.Rows(0)("deduction_code").ToString
            _compensationName = dt.Rows(0)("compensation_name").ToString
            _compensationAgency = dt.Rows(0)("compensation_agency").ToString
            _sortOrder = dt.Rows(0)("sort_order").ToString
        Else
            initialize()
        End If
    End Sub


End Class
