Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsAuditTrailModules


    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub


#Region "Properties"
    Public Property transId As String

    Public Property transModule As String

    Public Property transSubModule As String

    Public Property transType As String

    Public Property transProcess As String

    Public Property transDetails As String


    Public Property transBy As String

    Public Property logDate As String


#End Region


    Public Sub initialize()
        _transId = ""
        _transModule = ""
        _transSubModule = ""
        _transType = ""
        _transProcess = ""
        _transDetails = ""
        _transBy = ""
        _logDate = ""
    End Sub




    Public Sub saveAuditTrailModules()
        With _clsDB.dbUtility
            .fieldItems = "trans_id,trans_module,trans_submodule,trans_type,trans_process,trans_details,trans_by"
            .sqlString = .getSQLStatement("tbl_audit_trail_modules", "INSERT")
            _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
            .ADDPARAM_CMD_String("trans_id", _transId)
            .ADDPARAM_CMD_String("trans_module", _transModule)
            .ADDPARAM_CMD_String("trans_submodule", _transSubModule)
            .ADDPARAM_CMD_String("trans_type", _transType)
            .ADDPARAM_CMD_String("trans_process", _transProcess)
            .ADDPARAM_CMD_String("trans_details", _transDetails)
            .ADDPARAM_CMD_String("trans_by", _transBy)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub


   
End Class
