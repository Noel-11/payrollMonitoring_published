Imports Microsoft.VisualBasic

Imports System.Data

Public Class clsPayrollEmployeeAgencyId

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"

    Public Property employeeAgencyId As String

    Public Property employeeId As String

    Public Property agencyId As String

    Public Property agencyIdNo As String

    Public Property isDeduct As String

    Public Property agencyRemarks As String

    Public Property createUser As String

    Public Property createDate As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region

    Public Sub initialize()

        _employeeAgencyId = ""
        _employeeId = ""
        _agencyId = ""
        _agencyIdNo = ""
        _isDeduct = ""
        _agencyRemarks = ""
        _createUser = ""
        _createDate = ""
        _lastUser = ""
        _lastDate = ""

    End Sub

    Public Function browsePayrollEmployeeAgencyId(ByVal _thisId As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT employee_agency_id,tbl_ref_agency.agency_id,CONCAT(agency_name,' (',id_code,')') AS agency_name, agency_id_no, id_limit, " & _
              "(CASE WHEN COALESCE(tbl_payroll_employee_agency_id.is_deduct,'N') = 'Y' THEN 'TRUE' ELSE 'FALSE' END) AS is_deduct,employee_id,agency_remarks  FROM tbl_ref_agency " & _
              "LEFT JOIN tbl_payroll_employee_agency_id ON tbl_ref_agency.agency_id = tbl_payroll_employee_agency_id.agency_id AND " & _
              "tbl_payroll_employee_agency_id.employee_id = '" & _thisId & "' " & _
              "WHERE need_id = 'Y' " & _
              "ORDER BY agency_name"
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_agency_id")
    End Function


    Public Function getIsDeductCode(ByVal _thisDeduction As String, ByVal _thisEmployee As String) As String

        Dim sql As String

        Dim dt As New DataTable

        sql = "SELECT COALESCE(tbl_payroll_employee_agency_id.is_deduct,'Y') AS is_deduct, COALESCE(agency_remarks,'') AS agency_remarks FROM tbl_payroll_ref_deduction " & _
              "LEFT JOIN tbl_payroll_employee_agency_id ON tbl_payroll_ref_deduction.agency_id = tbl_payroll_employee_agency_id.agency_id AND " & _
              "tbl_payroll_employee_agency_id.employee_id = '" & _thisEmployee & "' " & _
              "WHERE tbl_payroll_ref_deduction.deduction_id = '" & _thisDeduction & "' AND deduction_type = 'SHARE' " & _
              "LIMIT 1 "

        dt = _clsDB.Fill_DataTable(sql)

        If dt.Rows.Count > 0 Then

            _agencyRemarks = IIf(dt.Rows(0)("agency_remarks") <> "", dt.Rows(0)("agency_remarks"), "N/A")

            Return dt.Rows(0)(0)
        Else

            Return "Y"
        End If

    End Function

    Public Sub savePayrollEmployeeAgencyId()

        If employeeAgencyId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "employee_agency_id,employee_id,agency_id,agency_id_no,is_deduct,agency_remarks,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_payroll_employee_agency_id", "INSERT")
                _employeeAgencyId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
                .ADDPARAM_CMD_String("employee_agency_id", _employeeAgencyId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("agency_id", _agencyId)
                .ADDPARAM_CMD_String("agency_id_no", _agencyIdNo)
                .ADDPARAM_CMD_String("is_deduct", _isDeduct)
                .ADDPARAM_CMD_String("agency_remarks", _agencyRemarks)
                .ADDPARAM_CMD_String("create_user", _lastUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "employee_id,agency_id,agency_id_no,is_deduct,agency_remarks,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_payroll_employee_agency_id", "UPDATE", "employee_agency_id")
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("agency_id", _agencyId)
                .ADDPARAM_CMD_String("agency_id_no", _agencyIdNo)
                .ADDPARAM_CMD_String("is_deduct", _isDeduct)
                .ADDPARAM_CMD_String("agency_remarks", _agencyRemarks)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .ADDPARAM_CMD_String("employee_agency_id", _employeeAgencyId)
                .executeUsingCommandFromSQL(True)
            End With
        End If

    End Sub


    Public Sub updateIsDeduct()

        With _clsDB.dbUtility
            .fieldItems = "is_deduct,agency_remarks,last_user,last_date"
            .sqlString = .getSQLStatement("tbl_payroll_employee_agency_id", "UPDATE", "employee_id,agency_id", "AND")
            .ADDPARAM_CMD_String("is_deduct", _isDeduct)
            .ADDPARAM_CMD_String("agency_remarks", _agencyRemarks)
            .ADDPARAM_CMD_String("last_user", _lastUser)
            .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
            .ADDPARAM_CMD_String("employee_id", _employeeId)
            .ADDPARAM_CMD_String("agency_id", _agencyId)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub

    Public Sub deletePayrollAgency(ByVal thisAgency As String, ByVal thisEmployee As String)

        _clsDB.Delete_Record("DELETE FROM tbl_payroll_employee_agency_id WHERE agency_id='" & thisAgency & "' AND employee_id='" & thisEmployee & "'")

    End Sub

    Public Sub getPayrollEmployeeAgencyId(ByVal _id As String)

        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_employee_agency_id WHERE employee_agency_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _employeeAgencyId = dt.Rows(0)("employee_agency_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _agencyId = dt.Rows(0)("agency_id").ToString
            _agencyIdNo = dt.Rows(0)("agency_id_no").ToString
            _agencyRemarks = dt.Rows(0)("agency_remarks").ToString
        Else
            initialize()
        End If

    End Sub


End Class
