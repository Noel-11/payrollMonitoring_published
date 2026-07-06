Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsPayrollRefDepartment

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property departmentId As String

    Public Property departmentName As String

    Public Property isActive As String

    Public Property lastUser As String

    Public Property lastDate As String

#End Region

    Public Sub initialize()
        _departmentId = ""
        _departmentName = ""
        _isActive = ""
        _lastUser = ""
        _lastDate = ""
    End Sub


    Public Function browsePayrollRefDepartment(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT department_id, department_name, is_active, last_user, last_date FROM tbl_payroll_ref_department " & _
             " WHERE department_name LIKE '%" & _criteria & "%' ORDER BY department_name "
        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_ref_department")
    End Function


    Public Sub savePayrollRefDepartment()
        If departmentId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "department_id,department_name,is_active"
                .sqlString = .getSQLStatement("tbl_payroll_ref_department", "INSERT")
                _departmentId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 10).ToUpper
                .ADDPARAM_CMD_String("department_id", _departmentId)
                .ADDPARAM_CMD_String("department_name", _departmentName)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "department_name,is_active,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_department", "UPDATE", "department_id")
                .ADDPARAM_CMD_String("department_name", _departmentName)
                .ADDPARAM_CMD_String("is_active", _isActive)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .ADDPARAM_CMD_String("department_id", _departmentId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub getPayrollRefDepartment(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_ref_department WHERE department_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _departmentId = dt.Rows(0)("department_id").ToString
            _departmentName = dt.Rows(0)("department_name").ToString
            _isActive = dt.Rows(0)("is_active").ToString
            _lastUser = dt.Rows(0)("last_user").ToString
        Else
            initialize()
        End If
    End Sub


End Class
