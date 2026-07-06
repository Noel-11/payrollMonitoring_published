Imports Microsoft.VisualBasic
Imports System.Data.Common
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports System.Web.UI.ClientScriptManager
Imports System.Web
Imports BasicFrame.WebControls.BasicDatePicker
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Public Class clsLibrary
    Dim _clsDB As New clsDatabase

    'Get Current Page
    Public Function fncrurrentpage(ByVal _strQUERY_STRING As String, ByVal _strSCRIPT_NAME As String) As String

        Dim _strPage As String() = _strSCRIPT_NAME.Split("/")
        _strSCRIPT_NAME = _strPage(_strPage.Length - 1)
        Return _strSCRIPT_NAME + "?" + _strQUERY_STRING

    End Function

    Public Function PopupWindow(ByVal _strURLPath As String, ByVal _intTypeid As Integer) As String
        If (_intTypeid = 1) Then
            Return "window.open('http://" + _strURLPath + "', 'CustomPopup' , 'height=550, width=600, status=yes, resizable=yes, scrollbars=yes, toolbar=yes, location=yes, menubar=yes')"
        ElseIf (_intTypeid = 2) Then
            Return "window.open('" + _strURLPath + "', 'CustomPopup' , 'height=550, width=600, status=no, resizable=yes, scrollbars=yes, toolbar=no, location=no, menubar=no')"
        Else
            Return "window.open('http://" + _strURLPath + "', 'CustomPopup' , 'height=550, width=600, status=no, resizable=yes, scrollbars=yes, toolbar=no, location=no, menubar=no')"
        End If
    End Function


    Public Function fnSetCurrentPage(ByVal _intPageIndex As Integer, ByVal _dt As DataTable, Optional _size As Integer = 10) As String
        Dim _strReturn As String

        Dim _intTotalRec As Integer, _intTotalPage As Integer

        _intTotalRec = 0
        If _dt.Rows.Count > 0 Then
            _intTotalRec = _dt.Rows.Count
        End If

        If _intTotalRec < _size + 1 Then
            _intTotalPage = 1
        Else
            _intTotalPage = Math.Truncate((_intTotalRec / _size))
            If (_intTotalRec Mod _size) <> 0 Then
                _intTotalPage += 1
            End If
        End If

        If _intPageIndex < 1 Then
            _intPageIndex = 1
        End If

        If _intTotalPage < 1 Then
            _intTotalPage = 1
        End If

        _strReturn = " Page " & _intPageIndex.ToString() & " of " & _intTotalPage.ToString() & ", Total Records: " & _intTotalRec.ToString

        Return _strReturn

    End Function

    Public Function fnShowMessage(ByVal sMessage As String) As String
        Dim strMsg As String = sMessage
        If sMessage = "" Then
            strMsg = "Transaction has been successfully saved."
        End If
        Return "<script>alert('" & strMsg & "');</script>"
    End Function

    Public Function fnRedirectAlert(ByVal url As String, ByVal msg As String) As String

        Dim strMsg As String = msg
        If msg = "" Then
            strMsg = "Transaction has been successfully saved."
        End If

        Dim strScript As String = "<script language=JavaScript>"
        strScript += "alert('" & strMsg & "');"
        strScript += "window.location='" & url & "'"
        strScript += "</script>"

        Return strScript

    End Function

    Public Function fnValidateEmail(ByVal _strEmail As String) As Boolean

        If _strEmail.Length > 0 Then
            _strEmail = _strEmail.ToLower()
        End If

        If System.Text.RegularExpressions.Regex.IsMatch(_strEmail, ConfigurationManager.AppSettings("EmailPattern").ToString()) Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function fnValidDate(ByVal _strDate As String) As Boolean

        Try
            If Not (Convert.IsDBNull(_strDate)) Then
                Dim _dtDate As Date = DateTime.Parse(_strDate)
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try

    End Function

    Public Function Boolean_To_Integer(ByVal this_bol As Boolean) As Integer
        If this_bol = True Then
            Return 1
        Else
            Return 0
        End If
    End Function

End Class
