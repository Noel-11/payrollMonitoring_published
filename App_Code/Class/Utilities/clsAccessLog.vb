Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsAccessLog


    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

    Private _transId As String
    Private _ipAddress As String
    Private _userAgent As String
    Private _computerName As String
    Private _userName As String
    Private _accessDatetime As String

#Region "Properties"
    Public Property transId As String
        Get
            Return Me._transId
        End Get
        Set(ByVal Value As String)
            Me._transId = Value
        End Set
    End Property

    Public Property ipAddress As String
        Get
            Return Me._ipAddress
        End Get
        Set(ByVal Value As String)
            Me._ipAddress = Value
        End Set
    End Property

    Public Property userAgent As String
        Get
            Return Me._userAgent
        End Get
        Set(ByVal Value As String)
            Me._userAgent = Value
        End Set
    End Property

    Public Property computerName As String
        Get
            Return Me._computerName
        End Get
        Set(ByVal Value As String)
            Me._computerName = Value
        End Set
    End Property

    Public Property userName As String
        Get
            Return Me._userName
        End Get
        Set(ByVal Value As String)
            Me._userName = Value
        End Set
    End Property

    Public Property accessDatetime As String
        Get
            Return Me._accessDatetime
        End Get
        Set(ByVal Value As String)
            Me._accessDatetime = Value
        End Set
    End Property

#End Region


    Public Sub initialize()
        _transId = ""
        _ipAddress = ""
        _userAgent = ""
        _computerName = ""
        _userName = ""
        _accessDatetime = ""
    End Sub


    Public Function browseAccessLog(ByVal _criteria As String, Optional ByVal browse_type As String = "BROWSE") As DataTable
        Dim sql As String = ""
        sql = "SELECT trans_id, ip_address, user_agent, computer_name, user_name, access_datetime, FROM tbl_access_log " & _
        " WHERE trans_id LIKE '%" & _criteria & "%' OR ip_address LIKE '%" & _criteria & "%' OR user_agent LIKE '%" & _criteria & "%' OR computer_name LIKE '%" & _criteria & "%' OR user_name LIKE '%" & _criteria & "%' OR access_datetime LIKE '%" & _criteria & "%' OR  ORDER BY "
        Return _clsDB.Fill_DataTable(sql, "tbl_access_log")
    End Function


    Public Sub saveAccessLog()

        With _clsDB.dbUtility
            .fieldItems = "trans_id,ip_address,user_agent,computer_name,user_name,access_datetime"
            .sqlString = .getSQLStatement("tbl_access_log", "INSERT")
            _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
            .ADDPARAM_CMD_String("trans_id", _transId)
            .ADDPARAM_CMD_String("ip_address", _ipAddress)
            .ADDPARAM_CMD_String("user_agent", _userAgent)
            .ADDPARAM_CMD_String("computer_name", _computerName)
            .ADDPARAM_CMD_String("user_name", _userName)
            .ADDPARAM_CMD_String("access_datetime", DateTime.Now.ToString)
            .executeUsingCommandFromSQL(True)
        End With
      
    End Sub


    Public Sub getAccessLog(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_access_log WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _ipAddress = dt.Rows(0)("ip_address").ToString
            _userAgent = dt.Rows(0)("user_agent").ToString
            _computerName = dt.Rows(0)("computer_name").ToString
            _userName = dt.Rows(0)("user_name").ToString
            _accessDatetime = dt.Rows(0)("access_datetime").ToString
        Else
            initialize()
        End If
    End Sub

End Class
