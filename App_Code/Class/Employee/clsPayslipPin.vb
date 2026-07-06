Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayslipPin
    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property employeeId As String

    Public Property pinCode As String

    Public Property ipAddress As String

    Public Property status As String

    Public Property createUser As String

    Public Property createDate As String

#End Region


    Public Sub initialize()
        _transId = ""
        _employeeId = ""
        _pinCode = ""
        _ipAddress = ""
        _status = ""
        _createUser = ""
        _createDate = ""
    End Sub


    Public Function browsePayslipPin(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT trans_id, employee_id, pin_code, ip_address, status, create_user, create_date, FROM tbl_payslip_pin " & _
        " WHERE trans_id LIKE '%" & _criteria & "%' OR employee_id LIKE '%" & _criteria & "%' OR pin_code LIKE '%" & _criteria & "%' OR ip_address LIKE '%" & _criteria & "%' OR status LIKE '%" & _criteria & "%' OR create_user LIKE '%" & _criteria & "%' OR create_date LIKE '%" & _criteria & "%' OR  ORDER BY "
        Return _clsDB.Fill_DataTable(sql, "tbl_payslip_pin")
    End Function



    Public Function getPin() As String

        Dim dtCheck As New DataTable
        Dim pinCode As String = ""

        Do
            pinCode = _clsDB.Get_DB_Item("SELECT FLOOR(100000 + RAND() * 899999) AS random_number LIMIT 1")

            dtCheck = _clsDB.Fill_DataTable("SELECT trans_id FROM tbl_payslip_pin WHERE pin_code = '" & pinCode & "' LIMIT 1 ")
        Loop While dtCheck.Rows.Count > 0


        Return pinCode

    End Function


    Public Sub savePayslipPin()
        ' If transId = "" Then
        ' _pinCode = getPin()

        If _pinCode <> "" Then
            With _clsDB.dbUtility
                .fieldItems = "employee_id,pin_code,ip_address,status,create_user,expiration_date"
                .sqlString = .getSQLStatement("tbl_payslip_pin", "INSERT")
                '_transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
                '.ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("employee_id", _employeeId)
                .ADDPARAM_CMD_String("pin_code", _pinCode)
                .ADDPARAM_CMD_String("ip_address", _ipAddress)
                .ADDPARAM_CMD_String("status", _status)
                .ADDPARAM_CMD_String("create_user", _createUser)
                .ADDPARAM_CMD_String("expiration_date", DateTime.Now.AddHours(24).ToString("yyyy-MM-dd HH:mm:ss"))
                .executeUsingCommandFromSQL(True)
            End With

        End If

    End Sub

  

    Public Sub getPayslipPin(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payslip_pin WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _employeeId = dt.Rows(0)("employee_id").ToString
            _pinCode = dt.Rows(0)("pin_code").ToString
            _ipAddress = dt.Rows(0)("ip_address").ToString
            _status = dt.Rows(0)("status").ToString
        Else
            initialize()
        End If
    End Sub


End Class
