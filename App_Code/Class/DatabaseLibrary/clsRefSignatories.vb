Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsRefSignatories

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property transId As String

    Public Property department As String

    Public Property sigModule As String

    Public Property sigForm As String

    Public Property sigName As String

    Public Property sigPosition As String

    Public Property sigType As String

#End Region

    Public Sub initialize()
        _transId = ""
        _department = ""
        _sigModule = ""
        _sigForm = ""
        _sigName = ""
        _sigPosition = ""
        _sigType = ""
    End Sub

    Public Function browseRefSignatories(ByVal _criteria As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT trans_id, department, sig_module, sig_form, sig_name, sig_position, sig_type, FROM tbl_ref_signatories " & _
              " WHERE trans_id LIKE '%" & _criteria & "%' OR department LIKE '%" & _criteria & "%' OR sig_module LIKE '%" & _criteria & "%' OR sig_form LIKE '%" & _criteria & "%' OR sig_name LIKE '%" & _criteria & "%' OR sig_position LIKE '%" & _criteria & "%' OR sig_type LIKE '%" & _criteria & "%' OR  ORDER BY "
        Return _clsDB.Fill_DataTable(sql, "tbl_ref_signatories")
    End Function

    Public Sub saveRefSignatories()
        If transId = "" Then
            With _clsDB.dbUtility
                .fieldItems = "trans_id,department,sig_module,sig_form,sig_name,sig_position,sig_type"
                .sqlString = .getSQLStatement("tbl_ref_signatories", "INSERT")
                _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
                .ADDPARAM_CMD_String("trans_id", _transId)
                .ADDPARAM_CMD_String("department", _department)
                .ADDPARAM_CMD_String("sig_module", _sigModule)
                .ADDPARAM_CMD_String("sig_form", _sigForm)
                .ADDPARAM_CMD_String("sig_name", _sigName)
                .ADDPARAM_CMD_String("sig_position", _sigPosition)
                .ADDPARAM_CMD_String("sig_type", _sigType)
                .executeUsingCommandFromSQL(True)
            End With
        Else
            With _clsDB.dbUtility
                .fieldItems = "department,sig_module,sig_form,sig_name,sig_position,sig_type"
                .sqlString = .getSQLStatement("tbl_ref_signatories", "UPDATE", "trans_id")
                .ADDPARAM_CMD_String("department", _department)
                .ADDPARAM_CMD_String("sig_module", _sigModule)
                .ADDPARAM_CMD_String("sig_form", _sigForm)
                .ADDPARAM_CMD_String("sig_name", _sigName)
                .ADDPARAM_CMD_String("sig_position", _sigPosition)
                .ADDPARAM_CMD_String("sig_type", _sigType)
                .ADDPARAM_CMD_String("trans_id", _transId)
                .executeUsingCommandFromSQL(True)
            End With
        End If
    End Sub

    Public Sub getRefSignatories(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_ref_signatories WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _department = dt.Rows(0)("department").ToString
            _sigModule = dt.Rows(0)("sig_module").ToString
            _sigForm = dt.Rows(0)("sig_form").ToString
            _sigName = dt.Rows(0)("sig_name").ToString
            _sigPosition = dt.Rows(0)("sig_position").ToString
            _sigType = dt.Rows(0)("sig_type").ToString
        Else
            initialize()
        End If
    End Sub


End Class
