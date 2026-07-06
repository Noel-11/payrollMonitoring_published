Imports Microsoft.VisualBasic

Imports System.Data

Public Class clsPayrollEmployeeProcessSectorGroupDetails

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property groupId As String

    Public Property employeeId As String

    Public Property isActive As String

    Public Property createUser As String

    Public Property createDate As String

#End Region


    Public Sub initialize()
        _transId = ""
        _groupId = ""
        _employeeId = ""
        _isActive = ""
        _createUser = ""
        _createDate = ""
    End Sub


    Public Function browsePayrollEmployeeProcessSectorGroupDetails(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT trans_id, group_id, employee_id, is_active, create_user, create_date FROM tbl_payroll_employee_process_sector_group_details " & _
              "WHERE trans_id LIKE '%" & _criteria & "%' OR group_id LIKE '%" & _criteria & "%' OR employee_id LIKE '%" & _criteria & "%' OR is_active LIKE '%" & _criteria & "%' OR create_user LIKE '%" & _criteria & "%' OR create_date LIKE '%" & _criteria & "%' OR  ORDER BY "
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_employee_process_sector_group_details")
    End Function


    Public Sub savePayrollEmployeeProcessSectorGroupDetails()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,group_id,employee_id,is_active,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_payroll_employee_process_sector_group_details", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 10).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("group_id", _groupId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("create_user", _createUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "group_id,employee_id,is_active"
                .sqlString = .getSQLStatement("tbl_payroll_employee_process_sector_group_details", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("group_id", _groupId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub

    Public Sub removePayrollEmployeeProcessSectorGroupDetails(ByVal _thisId As String)

        _clsDB.Delete_Record("DELETE FROM tbl_payroll_employee_process_sector_group_details WHERE employee_id = '" & _thisId & "'")

    End Sub

    Public Sub getPayrollEmployeeProcessSectorGroupDetails(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_employee_process_sector_group_details WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _groupId = dt.Rows(0)("group_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _isActive = dt.Rows(0)("is_active").ToString
        Else
            initialize()
        End If
    End Sub


End Class
