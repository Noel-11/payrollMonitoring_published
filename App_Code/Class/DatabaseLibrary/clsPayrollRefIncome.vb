Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollRefIncome

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property incomeId As String

    Public Property incomeDesc As String

    Public Property incomeCode As String

    Public Property incomeType As String

    Public Property incomeClassification As String

    Public Property listType As String

    Public Property incomeMode As String

    Public Property sortOrder As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region


    Public Sub initialize()
        _incomeId = ""
        _incomeDesc = ""
        _incomeCode = ""
        _incomeType = ""
        _incomeClassification = ""
        _listType = ""
        _incomeMode = ""
        _sortOrder = ""
        _isActive = ""
        _createUser = ""
        _createDate = ""
        _lastUser = ""
        _lastDate = ""

    End Sub


    Public Function browsePayrollRefIncome(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT * FROM tbl_payroll_ref_income " & _
              " WHERE income_desc LIKE '" & _criteria & "%' ORDER BY sort_order"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_ref_income")
    End Function


    Public Sub savePayrollRefIncome()
        If incomeId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "income_id,income_desc,income_code,income_type,income_mode,sort_order,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_income", "INSERT")
                _incomeId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 5).ToUpper
                .ADDPARAM_CMD_String("income_id", _incomeId)
                .ADDPARAM_CMD_String("income_desc", _incomeDesc)
                .ADDPARAM_CMD_String("income_code", _incomeCode)
                .ADDPARAM_CMD_String("income_type", _incomeType)
                .ADDPARAM_CMD_String("income_mode", _incomeMode)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _lastUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "income_desc,income_code,income_type,income_mode,sort_order,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_income", "UPDATE", "income_id")
                .ADDPARAM_CMD_String("income_desc", _incomeDesc)
                .ADDPARAM_CMD_String("income_code", _incomeCode)
                .ADDPARAM_CMD_String("income_type", _incomeType)
                .ADDPARAM_CMD_String("income_mode", _incomeMode)
                .ADDPARAM_CMD_String("sort_order", _sortOrder)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .ADDPARAM_CMD_String("income_id", _incomeId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub getPayrollRefIncome(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_ref_income WHERE income_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _incomeId = dt.Rows(0)("income_id").ToString
            _incomeDesc = dt.Rows(0)("income_desc").ToString
            _incomeCode = dt.Rows(0)("income_code").ToString
            _incomeType = dt.Rows(0)("income_type").ToString
            _incomeMode = dt.Rows(0)("income_mode").ToString
            _sortOrder = dt.Rows(0)("sort_order").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub

End Class
