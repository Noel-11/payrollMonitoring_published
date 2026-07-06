Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPayrollRefDeduction


    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property deductionId As String

    Public Property itemType As String

    Public Property deductionType As String

    Public Property deductionName As String

    Public Property deductionPayrollName As String

    Public Property deductionCode As String

    Public Property deductionAccountCode As String

    Public Property payrollColumnNumber As String

    Public Property sortOrder As String

    Public Property isRemoveAll As String

    Public Property deductionRole As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property agencyId As String

    Public Property minAmount As String

#End Region

    Public Sub initialize()
        _deductionId = ""
        _itemType = ""
        _deductionType = ""
        _deductionName = ""
        _deductionPayrollName = ""
        _deductionCode = ""
        _deductionAccountCode = ""
        _payrollColumnNumber = "0"
        _sortOrder = "0"
        _isActive = ""
        _createUser = ""
        _createDate = ""
        _agencyId = ""
        _isRemoveAll = "N"
        _deductionRole = "OPTIONAL"
        _minAmount = "0.00"
    End Sub


    Public Function browsePayrollRefDeductionForLog(ByVal _thisID As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT  deduction_type, deduction_name, deduction_payroll_name, deduction_code, deduction_account_code, sort_order, is_active FROM tbl_payroll_ref_deduction " & _
        " WHERE deduction_id='" & _thisID & "' LIMIT 1"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_ref_deduction")
    End Function


    Public Function browsePayrollRefDeduction(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT deduction_id, deduction_type, deduction_name, deduction_payroll_name, agency_name, deduction_code, " & _
              "deduction_account_code, sort_order, is_remove_all,tbl_payroll_ref_deduction.is_active FROM tbl_payroll_ref_deduction " & _
              "LEFT JOIN tbl_ref_agency ON tbl_payroll_ref_deduction.agency_id = tbl_ref_agency.agency_id " & _
              " WHERE  deduction_name LIKE '%" & _criteria & "%' ORDER BY sort_order"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_ref_deduction")
    End Function


    Public Sub savePayrollRefDeduction()
        If deductionId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "deduction_id,deduction_type,agency_id,deduction_name,deduction_payroll_name,deduction_code,deduction_account_code,payroll_column_number,sort_order,is_remove_all,deduction_role,min_amount,is_active,create_user"
                .sqlString = .getSQLStatement("tbl_payroll_ref_deduction", "INSERT")
                _deductionId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 4).ToUpper
                .ADDPARAM_CMD_String("deduction_id", _deductionId)
                .ADDPARAM_CMD_String("deduction_type", _deductionType)
                .ADDPARAM_CMD_String("agency_id", _agencyId)
                .ADDPARAM_CMD_String("deduction_name", _deductionName)
                .ADDPARAM_CMD_String("deduction_payroll_name", _deductionPayrollName)
                .ADDPARAM_CMD_String("deduction_code", _deductionCode)
                .ADDPARAM_CMD_String("deduction_account_code", _deductionAccountCode)
                .ADDPARAM_CMD_String("payroll_column_number", _payrollColumnNumber)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("is_remove_all", _isRemoveAll)
                .ADDPARAM_CMD_String("deduction_role", _deductionRole)
                .ADDPARAM_CMD_String("min_amount", _minAmount)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _createUser)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "deduction_type,agency_id,deduction_name,deduction_payroll_name,deduction_code,deduction_account_code,payroll_column_number,sort_order,is_remove_all,deduction_role,min_amount,is_active"
                .sqlString = .getSQLStatement("tbl_payroll_ref_deduction", "UPDATE", "deduction_id")
                .ADDPARAM_CMD_String("deduction_type", _deductionType)
                .ADDPARAM_CMD_String("agency_id", _agencyId)
                .ADDPARAM_CMD_String("deduction_name", _deductionName)
                .ADDPARAM_CMD_String("deduction_payroll_name", _deductionPayrollName)
                .ADDPARAM_CMD_String("deduction_code", _deductionCode)
                .ADDPARAM_CMD_String("deduction_account_code", _deductionAccountCode)
                .ADDPARAM_CMD_String("payroll_column_number", _payrollColumnNumber)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("is_remove_all", _isRemoveAll)
                .ADDPARAM_CMD_String("deduction_role", _deductionRole)
                .ADDPARAM_CMD_String("min_amount", _minAmount)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("deduction_id", _deductionId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub

    Public Sub getPayrollRefDeduction(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_ref_deduction WHERE deduction_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _deductionId = dt.Rows(0)("deduction_id").ToString
            _itemType = dt.Rows(0)("item_type").ToString
            _deductionType = dt.Rows(0)("deduction_type").ToString
            _deductionName = dt.Rows(0)("deduction_name").ToString
            _deductionPayrollName = dt.Rows(0)("deduction_payroll_name").ToString
            _deductionCode = dt.Rows(0)("deduction_code").ToString
            _deductionAccountCode = dt.Rows(0)("deduction_account_code").ToString
            _payrollColumnNumber = dt.Rows(0)("payroll_column_number").ToString
            _agencyId = dt.Rows(0)("agency_id").ToString
            _sortOrder = dt.Rows(0)("sort_order").ToString
            _isRemoveAll = dt.Rows(0)("is_remove_all").ToString
            _deductionRole = dt.Rows(0)("deduction_role").ToString
            _minAmount = dt.Rows(0)("min_amount").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub

End Class
