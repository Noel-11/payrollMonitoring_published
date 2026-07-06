Imports Microsoft.VisualBasic
Imports System.Data
Public Class clsUtilSecurity

    Dim _clsDB As New clsDatabase

    Public Function encryptString(ByVal _thisStr As String) As String

        Dim dt As New DataTable

        dt = _clsDB.Fill_DataTable("SELECT MD5('" & _thisStr & "')")

        Dim _encrypted As String = ""

        For Each dr As DataRow In dt.Rows
            _encrypted = dr(0)
        Next

        Return _encrypted

    End Function

End Class
