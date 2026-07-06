Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPayrollRefFundCharges
    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property fundChargeId As String

    Public Property department As String

    Public Property fundSource As String

    Public Property fundCharges As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region


    Public Sub initialize()
        _fundChargeId = ""
        _department = ""
        _fundSource = ""
        _fundCharges = ""
        _isActive = ""
        _createUser = ""
        _createDate = ""
        _lastUser = ""
        _lastDate = ""
    End Sub


    Public Function browsePayrollRefFundCharges(ByVal _criteria As String, ByVal _thisDepartment As String) As DataTable
        Dim sql As String = ""


        sql = "SELECT fund_charge_id, department, fund_source, fund_charges, is_active, create_user, create_date, last_user, last_date FROM tbl_payroll_ref_fund_charges " & _
              "WHERE fund_charges LIKE '" & _criteria & "%' AND department = '" & _thisDepartment & "' "

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_ref_fund_charges")
    End Function


    Public Sub savePayrollRefFundCharges()
        If fundChargeId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "fund_charge_id,department,fund_source,fund_charges,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_fund_charges", "INSERT")
                _fundChargeId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
                .ADDPARAM_CMD_String("fund_charge_id", _fundChargeId)
                .ADDPARAM_CMD_String("department", _department)
                .ADDPARAM_CMD_String("fund_source", _fundSource)
                .ADDPARAM_CMD_String("fund_charges", _fundCharges)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _lastUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "department,fund_source,fund_charges,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_fund_charges", "UPDATE", "fund_charge_id")
                .ADDPARAM_CMD_String("department", _department)
                .ADDPARAM_CMD_String("fund_source", _fundSource)
                .ADDPARAM_CMD_String("fund_charges", _fundCharges)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .ADDPARAM_CMD_String("fund_charge_id", _fundChargeId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub getPayrollRefFundCharges(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_ref_fund_charges WHERE fund_charge_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _fundChargeId = dt.Rows(0)("fund_charge_id").ToString
            _department = dt.Rows(0)("department").ToString
            _fundSource = dt.Rows(0)("fund_source").ToString
            _fundCharges = dt.Rows(0)("fund_charges").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub


End Class
