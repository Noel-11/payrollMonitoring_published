Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsPayrollTransactionLogs

    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"

    Public Property transId As String
    Public Property transactionHeaderId As String
    Public Property referenceCode As String
    Public Property payrollStatus As String
    Public Property payrollRemarks As String
    Public Property createUser As String
    Public Property userOffice As String
    Public Property createDatetime As String

#End Region

    Public Sub initialize()

        transId = ""
        transactionHeaderId = ""
        referenceCode = ""
        payrollStatus = ""
        payrollRemarks = ""
        createUser = ""
        userOffice = ""
        createDatetime = Nothing

    End Sub

    Public Function browsePayrollTransactionHeaderForLog(ByVal _thisID As String) As DataTable

        Dim sql As String = ""

        sql = "SELECT * FROM tbl_payroll_transaction_logs " & _
              "WHERE transaction_header_id = '" & _thisID & "' " & _
              "ORDER BY create_datetime DESC "

        Return _clsDB.Fill_DataTable(sql, "tbl_payroll_transaction_header")

    End Function


    Public Sub savePayrollTransactionLogs()

        With _clsDB.dbUtility
            .fieldItems = "trans_id,transaction_header_id,reference_code,payroll_status,payroll_remarks,create_user,user_office"
            .sqlString = .getSQLStatement("tbl_payroll_transaction_logs", "INSERT")
            _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 5).ToUpper
            .ADDPARAM_CMD_String("trans_id", transId)
            .ADDPARAM_CMD_String("transaction_header_id", transactionHeaderId)
            .ADDPARAM_CMD_String("reference_code", referenceCode)
            .ADDPARAM_CMD_String("payroll_status", payrollStatus)
            .ADDPARAM_CMD_String("payroll_remarks", payrollRemarks)
            .ADDPARAM_CMD_String("create_user", createUser)
            .ADDPARAM_CMD_String("user_office", userOffice)
            '.ADDPARAM_CMD_String("create_datetime", userOffice)
            .executeUsingCommandFromSQL(True)
        End With

    End Sub


End Class
