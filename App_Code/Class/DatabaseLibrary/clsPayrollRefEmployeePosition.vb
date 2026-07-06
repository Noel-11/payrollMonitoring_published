Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollRefEmployeePosition

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property positionId As String

    Public Property positionName As String

    Public Property salaryGrade As String

    Public Property isPlantilla As String

    Public Property isSector As String

    Public Property positionRate As String

    Public Property status As String

    Public Property lastUser As String

    Public Property lastDate As String

    Public Property createUser As String

    Public Property createDate As String

#End Region


    Public Sub initialize()
        _positionId = ""
        _positionName = ""
        _salaryGrade = "0"
        _isPlantilla = ""
        _isSector = "N"
        _positionRate = "0"
        _status = ""
        _lastUser = ""
        _lastDate = ""
        _createUser = ""
        _createDate = ""
    End Sub


    Public Function browsePayrollRefEmployeePosition(ByVal _criteria As String) As DataTable

        Dim sql As String = ""

        Dim _sqlWhere As String = ""

        If _criteria <> "" Then
            _sqlWhere += "AND position_name LIKE '%" & _criteria & "%' "
        End If

        sql = "SELECT position_id, position_name, salary_grade, is_plantilla, position_rate, status, last_user, last_date, create_user, create_date FROM tbl_payroll_ref_employee_position " & _
              "WHERE  position_name <> '' " & _sqlWhere & _
              "ORDER BY position_name ASC "

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_ref_employee_position")

    End Function


    Public Sub savePayrollRefEmployeePosition()
        If positionId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "position_id,position_name,salary_grade,is_plantilla,is_sector,position_rate,status,create_user,create_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_employee_position", "INSERT")
                _positionId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
                .ADDPARAM_CMD_String("position_id", _positionId)
                .ADDPARAM_CMD_String("position_name", _positionName)
                .ADDPARAM_CMD_String("salary_grade", _salaryGrade)
                .ADDPARAM_CMD_String("is_plantilla", _isPlantilla)
                .ADDPARAM_CMD_String("is_sector", _isSector)
                .ADDPARAM_CMD_String("position_rate", _positionRate)
                .ADDPARAM_CMD_String("status", _status)
                .ADDPARAM_CMD_String("create_user", lastUser)
                .ADDPARAM_CMD_String("create_date", DateTime.Now.ToString)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "position_name,salary_grade,is_plantilla,is_sector,position_rate,status,last_user,last_date"
                .sqlString = .getSQLStatement("tbl_payroll_ref_employee_position", "UPDATE", "position_id")
                .ADDPARAM_CMD_String("position_name", _positionName)
                .ADDPARAM_CMD_String("salary_grade", _salaryGrade)
                .ADDPARAM_CMD_String("is_plantilla", _isPlantilla)
                .ADDPARAM_CMD_String("is_sector", _isSector)
                .ADDPARAM_CMD_String("position_rate", _positionRate)
                .ADDPARAM_CMD_String("status", _status)
                .ADDPARAM_CMD_String("last_user", _lastUser)
                .ADDPARAM_CMD_String("last_date", DateTime.Now.ToString)
                .ADDPARAM_CMD_String("position_id", _positionId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub


    Public Sub getPayrollRefEmployeePosition(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_ref_employee_position WHERE position_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _positionId = dt.Rows(0)("position_id").ToString
            _positionName = dt.Rows(0)("position_name").ToString
            _salaryGrade = dt.Rows(0)("salary_grade").ToString
            _isPlantilla = dt.Rows(0)("is_plantilla").ToString
            _isSector = dt.Rows(0)("is_sector").ToString
            _positionRate = dt.Rows(0)("position_rate").ToString
            _status = dt.Rows(0)("status").ToString
            _lastUser = dt.Rows(0)("last_user").ToString
        Else
            initialize()
        End If
    End Sub


End Class
