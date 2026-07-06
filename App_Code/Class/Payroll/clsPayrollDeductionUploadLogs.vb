Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollDeductionUploadLogs

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property deductionId As String

    Public Property deductionMonth As String

    Public Property deductionYear As String

    Public Property uploadDate As String

    Public Property uploadUser As String

    Public Property uploadStatus As String

    Public Property uploadRemarks As String

#End Region

    Public Sub initialize()
        _transId = ""
        _deductionId = ""
        _deductionMonth = "0"
        _deductionYear = "0"
        _uploadDate = ""
        _uploadUser = ""
        _uploadStatus = ""
        _uploadRemarks = ""
    End Sub


    'Public Function browsePayrollDeductionUploadLogs(ByVal _criteria As String) As DataTable
    '    Dim sql As String = ""
    '    sql = "SELECT trans_id, deduction_id, upload_date, upload_user, upload_status FROM tbl_payroll_deduction_upload_logs " & _
    '    " WHERE trans_id LIKE '%" & _criteria & "%' OR deduction_id LIKE '%" & _criteria & "%' OR upload_date LIKE '%" & _criteria & "%' OR upload_user LIKE '%" & _criteria & "%' OR upload_status LIKE '%" & _criteria & "%' OR  ORDER BY "
    '    Return _clsDB.Fill_DataTable(sql, "tbl_payroll_deduction_upload_logs")
    'End Function

    Public Sub savePayrollDeductionUploadLogs()
        'If transId = "" Then
        With _clsDB.dbUtility
            .fieldItems = "trans_id,deduction_id,deduction_month,deduction_year,upload_user,upload_status,upload_remarks"
            .sqlString = .getSQLStatement("tbl_payroll_deduction_upload_logs", "INSERT")
            _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 10).ToUpper
            .ADDPARAM_CMD_String("trans_id", _transId)
            .ADDPARAM_CMD_String("deduction_id", _deductionId)
            .ADDPARAM_CMD_String("deduction_month", _deductionMonth)
            .ADDPARAM_CMD_String("deduction_year", _deductionYear)
            .ADDPARAM_CMD_String("upload_user", _uploadUser)
            .ADDPARAM_CMD_String("upload_status", _uploadStatus)
            .ADDPARAM_CMD_String("upload_remarks", _uploadRemarks)
            .executeUsingCommandFromSQL(True)
        End With
        'Else
        '    With _clsDB.dbUtility
        '        .fieldItems = "deduction_id,upload_date,upload_user,upload_status"
        '        .sqlString = .getSQLStatement("tbl_payroll_deduction_upload_logs", "UPDATE", "trans_id")
        '        .ADDPARAM_CMD_String("deduction_id", _deductionId)
        '        .ADDPARAM_CMD_String("upload_date", _uploadDate)
        '        .ADDPARAM_CMD_String("upload_user", _uploadUser)
        '        .ADDPARAM_CMD_String("upload_status", _uploadStatus)
        '        .ADDPARAM_CMD_String("trans_id", _transId)
        '        .executeUsingCommandFromSQL(True)
        '    End With
        'End If
    End Sub


    Public Sub getPayrollDeductionUploadLogs(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_payroll_deduction_upload_logs WHERE trans_id='" & _id & "'")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _deductionId = dt.Rows(0)("deduction_id").ToString
            _uploadDate = dt.Rows(0)("upload_date").ToString
            _uploadUser = dt.Rows(0)("upload_user").ToString
            _uploadStatus = dt.Rows(0)("upload_status").ToString
        Else
            initialize()
        End If
    End Sub


End Class
