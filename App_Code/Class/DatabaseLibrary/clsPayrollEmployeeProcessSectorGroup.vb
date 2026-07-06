Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollEmployeeProcessSectorGroup

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"

    Public Property transId As String

    Public Property department As String

    Public Property groupDesc As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

#End Region

    Public Sub initialize()
        _transId = ""
        _department = ""
        _groupDesc = ""
        _isActive = ""
        _createUser = ""
        _createDate = ""
    End Sub


    Public Function browsePayrollEmployeeProcessSectorGroup(ByVal _criteria As String, ByVal _thisDepartment As String) As DataTable

        Dim sql As String = ""
        Dim sqlWhere As String = ""

        If _criteria <> "" Then
            sqlWhere = " AND group_desc LIKE '%" & _criteria & "%' "
        End If

        sql = "SELECT trans_id, department, group_desc, is_active, create_user, create_date FROM tbl_payroll_employee_process_sector_group " & _
              "WHERE department = '" & _thisDepartment & "' " & sqlWhere & _
              "ORDER BY group_desc"

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_process_sector_group")

    End Function


    Public Sub savePayrollEmployeeProcessSectorGroup()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,department,group_desc,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_payroll_employee_process_sector_group", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 5).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("department", _department)
                .ADDPARAM_CMD_String("group_desc", _groupDesc)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _createUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "department,group_desc,is_active"
                .sqlString = .getSQLStatement("tbl_payroll_employee_process_sector_group", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("department", _department)
                .ADDPARAM_CMD_String("group_desc", _groupDesc)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub getPayrollEmployeeProcessSectorGroup(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_employee_process_sector_group WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _department = dt.Rows(0)("department").ToString
            _groupDesc = dt.Rows(0)("group_desc").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub


End Class
