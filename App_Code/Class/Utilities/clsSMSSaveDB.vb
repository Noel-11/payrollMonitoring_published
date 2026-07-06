Imports Microsoft.VisualBasic

Public Class clsSMSSaveDB

    Public dbUtility As New clsDimboMySQL.clsMYSQLDatabase

    Public Sub New()

        'dbUtility.Initialize_DB_MYSQL_WEB("Server=localhost;Initial Catalog='db_sms_server';Persist Security Info=no;User Name='root';Password='ictlgucdo';default command timeout=360;Port=3306;Allow Zero Datetime=true")

        dbUtility.Initialize_DB_MYSQL_WEB("Server=172.16.1.101;Initial Catalog='db_sms_server';Persist Security Info=no;User Name='vaccsms';Password='vaccsms';default command timeout=360;Port=3602;Allow Zero Datetime=true")

        'dbUtility.Initialize_DB_MYSQL_WEB("Server=localhost;Initial Catalog='db_sms_server';Persist Security Info=no;User Name='sms';Password='sms';default command timeout=360;Port=3306;Allow Zero Datetime=true")

    End Sub

    Public Sub saveSMSDB(thisSender As String, thisRecipient As String, thisMessage As String)
        Dim recepient() As String = thisRecipient.Split(",")
        For Each r As String In recepient
            If r.Trim.Length = 11 Then
                With dbUtility
                    .fieldItems = "trans_id,received_id,recipient,recipient_message,priority,send_status,send_number,send_date,send_time"
                    .sqlString = .getSQLStatement("tbl_sms_for_sending", "INSERT")
                    .ADDPARAM_CMD_String("trans_id", DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper)
                    .ADDPARAM_CMD_String("received_id", thisSender)
                    .ADDPARAM_CMD_String("recipient", r)
                    .ADDPARAM_CMD_String("recipient_message", thisMessage & " SYSTEM GENERATED SMS. DO NOT REPLY.")
                    .ADDPARAM_CMD_String("priority", "999")
                    .ADDPARAM_CMD_String("send_status", "SEND")
                    .ADDPARAM_CMD_String("send_number", thisSender)
                    .ADDPARAM_CMD_String("send_date", DateTime.Now.Date.ToString("yyyy-MM-dd"))
                    .ADDPARAM_CMD_String("send_time", DateTime.Now.ToString("HH:mm:ss"))
                    .executeUsingCommandFromSQL(True)
                End With
            End If
        Next
    End Sub

End Class
