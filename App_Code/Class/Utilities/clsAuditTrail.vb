Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsAuditTrail
    Dim _clsDB As New clsDatabase

    Public Sub New()
        initialize()
    End Sub

#Region "Properties"
    Public Property transId As String

    Public Property transModule As String

    Public Property linkID As String

    Public Property loggedChanges As String

    Public Property transType As String

    Public Property transBy As String

    Public Property logDate As String


#End Region

    Public Sub initialize()
        _transId = ""
        _transModule = ""
        _linkID = ""
        _loggedChanges = ""
        _transType = ""
        _transBy = ""
        _logDate = ""
    End Sub


    Public Function browseLogTransactions(thisTrans As String, thisTransType As String) As DataTable
        Dim sql As String = ""
        sql = "SELECT trans_module, logged_changes, CONCAT(trans_by,' - ', trans_datetime) AS trans_by FROM tbl_audit_trail " & _
              " WHERE  link_id='" & thisTrans & "' AND trans_module <> 'DELETE' ORDER BY trans_datetime DESC "
        Return _clsDB.Fill_DataTable(sql, "tbl_audit_trail")
    End Function


    Public Sub saveAuditTrail()
        With _clsDB.dbUtility
            .fieldItems = "trans_id,trans_module,link_id,logged_changes,trans_type,trans_by"
            .sqlString = .getSQLStatement("tbl_audit_trail", "INSERT")
            _transId = DateTime.Now.ToString("MMddyyyymmhhss") & Left(Guid.NewGuid().ToString.Replace("-", ""), 25).ToUpper
            .ADDPARAM_CMD_String("trans_id", _transId)
            .ADDPARAM_CMD_String("trans_module", _transModule)
            .ADDPARAM_CMD_String("link_id", _linkID)
            .ADDPARAM_CMD_String("logged_changes", _loggedChanges)
            .ADDPARAM_CMD_String("trans_type", _transType)
            .ADDPARAM_CMD_String("trans_by", _transBy)
            .executeUsingCommandFromSQL(True)
        End With
    End Sub


    Public Sub getAuditTrail(ByVal _id As String)
        Dim dt As New DataTable
        dt = _clsDB.Fill_DataTable("SELECT * FROM tbl_audit_trail WHERE trans_id='" & _id & "' LIMIT 1")
        If dt.Rows.Count > 0 Then
            _transId = dt.Rows(0)("trans_id").ToString
            _transModule = dt.Rows(0)("trans_module").ToString
            _linkID = dt.Rows(0)("link_id").ToString
            _loggedChanges = dt.Rows(0)("logged_changes").ToString
            _transType = dt.Rows(0)("trans_type").ToString
            _transBy = dt.Rows(0)("trans_by").ToString
            _logDate = dt.Rows(0)("trans_datetime").ToString
        Else
            initialize()
        End If
    End Sub

    Public Function getTransactionChanges(oldDT As DataTable, newDT As DataTable) As String


        Dim changes As New StringBuilder

        For i = 0 To oldDT.Columns.Count - 1

            If oldDT.Rows(0)(i).ToString <> newDT.Rows(0)(i).ToString Then

                If changes.ToString = "" Then
                    changes.Append(" (" & oldDT.Columns(i).ColumnName & ")[ " & oldDT.Rows(0)(i).ToString & "|" & newDT.Rows(0)(i).ToString & " ]")
                    changes.Append(" , ")
                Else

                    changes.Append(" (" & oldDT.Columns(i).ColumnName & ")[ " & oldDT.Rows(0)(i).ToString & "|" & newDT.Rows(0)(i).ToString & " ]")
                    changes.Append(" , ")
                End If
            End If
        Next

        Try
            changes.Remove(changes.Length - 2, 2)
        Catch ex As Exception

        End Try



        Return changes.ToString.Trim
    End Function
    Public Function getTransactionNewEntry(newDT As DataTable) As String


        Dim changes As New StringBuilder

        For i = 0 To newDT.Columns.Count - 1



            If changes.ToString = "" Then
                changes.Append(" (" & newDT.Columns(i).ColumnName & ")[ " & newDT.Rows(0)(i).ToString & " ]")
                changes.Append(" , ")
            Else

                changes.Append(" (" & newDT.Columns(i).ColumnName & ")[ " & newDT.Rows(0)(i).ToString & " ]")
                changes.Append(" , ")
            End If

        Next

        Try
            changes.Remove(changes.Length - 2, 2)
        Catch ex As Exception

        End Try



        Return changes.ToString.Trim
    End Function


End Class
