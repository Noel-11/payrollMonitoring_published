Imports System.Data
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports Microsoft.Reporting.WebForms
Partial Class _PayrollStatus

    Inherits System.Web.UI.Page

    Dim _dt As New DataTable

    Dim clsLibrary As New clsLibrary
    Dim _clsUser As New clsUser
    Dim _clsDB As New clsDatabase
    Dim _dtCV As New DataTable

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

End Class


