
Partial Class Include_wucReport
    Inherits System.Web.UI.UserControl

    Public Sub show(Optional ByVal _strHeader As String = "Report")
        ltEmbed.Text = String.Format(formatDoc, ResolveUrl("~/ReportHandler.ashx"), "")
        lblMsgBoxHeader.Text = _strHeader
        mpeMessageBox.Show()
    End Sub


    Public Function formatDoc() As String

        Dim embed As String = ""

        embed = "<object data=""{0}{1}"" type=""application/pdf"" width=""1000px"" height=""550px"">"
        embed += "</object>"

        Return embed

    End Function
End Class
