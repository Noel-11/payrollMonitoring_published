Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.SmtpClient

Imports iText
Imports iText.Kernel.Pdf
Imports iText.Layout
Imports iText.Layout.Element
Imports iText.Layout.Properties
Imports iText.Kernel.Geom
Imports iText.Kernel.Pdf.Xobject
Imports iText.Kernel.Pdf.Canvas
Imports System.IO

Public Class clsUtility
    Public Function stringToBoolean(_yn As String) As Boolean
        If _yn = "Y" Then Return True

        Return False
    End Function

    Public Function stringToDecimalPositive(thisString As String) As Decimal
        Dim thisVal As Decimal = 0.0

        thisString = thisString.Trim.Replace(",", "").Replace(" ", "")

        If IsNumeric(thisString) Then
            thisVal = CDec(thisString)
        End If

        If thisVal < 0 Then thisVal = 0.0

        Return thisVal
    End Function

    Public Function stringToDecimal(thisString As String) As Decimal
        Dim thisVal As Decimal = 0.0

        thisString = thisString.Trim.Replace(",", "").Replace(" ", "")

        If IsNumeric(thisString) Then
            thisVal = CDec(thisString)
        End If


        Return thisVal
    End Function

    Public Function stringToInteger(thisString As String) As Integer
        Dim thisVal As Integer = 0

        thisString = thisString.Trim.Replace(",", "").Replace(" ", "")

        If IsNumeric(thisString) Then
            thisVal = CInt(thisString)
        End If

        Return thisVal
    End Function

    Public Function decimalToString(thisString As String, Optional zeroCount As String = "00") As String
        Dim thisVal As String

        thisString = thisString.Trim.Replace(",", "").Replace(" ", "")

        If IsNumeric(thisString) Then
            thisVal = CDec(thisString).ToString("#,##0." & zeroCount)
        Else
            thisVal = "0." & zeroCount
        End If


        Return thisVal
    End Function
    Public Function integerToString(thisString As String) As String
        Dim thisVal As String

        thisString = thisString.Trim.Replace(",", "").Replace(" ", "")

        If IsNumeric(thisString) Then
            thisVal = CDec(thisString).ToString("#,###")
        Else
            thisVal = "0"
        End If


        Return thisVal
    End Function


    Public Function dateToString(thisDate As String, Optional dateFormat As String = "yyyy-MM-dd") As String

        If IsDate(thisDate) Then
            Return CDate(thisDate).ToString(dateFormat)
        End If


        Return "0000-00-00"
    End Function

    Public Function stringToDate(thisDate As String) As Date

        If IsDate(thisDate) Then
            Return CDate(thisDate)
        End If


        Return CDate("01/01/0001")
    End Function
    Public Sub sendGmail(thisRecipient As String, thisSubject As String, thisMessage As String)
        Dim client As New SmtpClient("smtp.gmail.com", 587)
        client.EnableSsl = True
        client.UseDefaultCredentials = False
        Dim from As New MailAddress("lgucdoonlineservices@gmail.com", "[City Finance Office]")
        Dim [to] As New MailAddress(thisRecipient, thisRecipient)
        Dim message As New MailMessage(from, [to])
        message.Body = thisMessage & "<br/><br/>" & " This is a system generated email, please do not reply."
        message.IsBodyHtml = True
        message.Subject = thisSubject
        Dim myCreds As New NetworkCredential("lgucdoonlineservices@gmail.com", "derflez79", "")
        client.Credentials = myCreds
        Try
            client.Send(message)
        Catch ex As Exception

        End Try

    End Sub

    Public Function adjustPDFMargin(bytes() As Byte, leftMargin As Integer, topMargin As Integer) As Byte()
        Dim ms As New MemoryStream(bytes)
        Dim msW As New MemoryStream()
        Dim reader As New PdfReader(ms)

        Dim writer As New PdfWriter(msW)

        Dim srcDoc As PdfDocument = New PdfDocument(reader)
        Dim pageSize As Rectangle = srcDoc.GetFirstPage().GetPageSize()
        Dim resultPdfDoc As PdfDocument = New PdfDocument(writer)
        resultPdfDoc.SetDefaultPageSize(New PageSize(pageSize))
        resultPdfDoc.AddNewPage()



        Dim pageXObject As PdfFormXObject = srcDoc.GetFirstPage().CopyAsFormXObject(resultPdfDoc)
        Dim toMove As Rectangle = New Rectangle(0, 0, 800, 1000)

        ' Create a formXObject of a page content, in which the area to move is cut.
        Dim formXObject1 As PdfFormXObject = New PdfFormXObject(pageSize)
        Dim canvas1 As PdfCanvas = New PdfCanvas(formXObject1, resultPdfDoc)
        canvas1.Rectangle(0, 0, pageSize.GetWidth(), pageSize.GetHeight())
        canvas1.Rectangle(toMove)

        ' This method uses the even-odd rule to determine which regions lie inside the clipping path.
        canvas1.EoClip()
        canvas1.EndPath()
        canvas1.AddXObjectAt(pageXObject, 0, 0)

        ' Create a formXObject of the area to move.
        Dim formXObject2 As PdfFormXObject = New PdfFormXObject(pageSize)
        Dim canvas2 As PdfCanvas = New PdfCanvas(formXObject2, resultPdfDoc)
        canvas2.Rectangle(toMove)

        ' This method uses the nonzero winding rule to determine which regions lie inside the clipping path.
        canvas2.Clip()
        canvas2.EndPath()
        canvas2.AddXObjectAt(pageXObject, 0, 0)
        Dim canvas As PdfCanvas = New PdfCanvas(resultPdfDoc.GetFirstPage())
        canvas.AddXObjectAt(formXObject1, 0, 0)

        ' Add the area to move content, shifted 10 points to the left and 2 points to the bottom.
        canvas.AddXObjectAt(formXObject2, leftMargin, TopMargin)
        srcDoc.Close()
        resultPdfDoc.Close()

        Dim bytez() As Byte = msW.ToArray

        Return bytez

    End Function

    'Public Sub manipultaePDF(bytes() As Byte)
    '    Dim ms As New MemoryStream(bytes)
    '    Dim msW As New MemoryStream(bytes)
    '    Dim reader As New PdfReader(ms)

    '    Dim writer As New PdfWriter(Response.OutputStream)

    '    Dim srcDoc As PdfDocument = New PdfDocument(reader)
    '    Dim pageSize As Rectangle = srcDoc.GetFirstPage().GetPageSize()
    '    Dim resultPdfDoc As PdfDocument = New PdfDocument(writer)
    '    resultPdfDoc.SetDefaultPageSize(New PageSize(pageSize))
    '    resultPdfDoc.AddNewPage()



    '    Dim pageXObject As PdfFormXObject = srcDoc.GetFirstPage().CopyAsFormXObject(resultPdfDoc)
    '    Dim toMove As Rectangle = New Rectangle(0, 0, 800, 1000)

    '    ' Create a formXObject of a page content, in which the area to move is cut.
    '    Dim formXObject1 As PdfFormXObject = New PdfFormXObject(pageSize)
    '    Dim canvas1 As PdfCanvas = New PdfCanvas(formXObject1, resultPdfDoc)
    '    canvas1.Rectangle(0, 0, pageSize.GetWidth(), pageSize.GetHeight())
    '    canvas1.Rectangle(toMove)

    '    ' This method uses the even-odd rule to determine which regions lie inside the clipping path.
    '    canvas1.EoClip()
    '    canvas1.EndPath()
    '    canvas1.AddXObjectAt(pageXObject, 0, 0)

    '    ' Create a formXObject of the area to move.
    '    Dim formXObject2 As PdfFormXObject = New PdfFormXObject(pageSize)
    '    Dim canvas2 As PdfCanvas = New PdfCanvas(formXObject2, resultPdfDoc)
    '    canvas2.Rectangle(toMove)

    '    ' This method uses the nonzero winding rule to determine which regions lie inside the clipping path.
    '    canvas2.Clip()
    '    canvas2.EndPath()
    '    canvas2.AddXObjectAt(pageXObject, 0, 0)
    '    Dim canvas As PdfCanvas = New PdfCanvas(resultPdfDoc.GetFirstPage())
    '    canvas.AddXObjectAt(formXObject1, 0, 0)

    '    ' Add the area to move content, shifted 10 points to the left and 2 points to the bottom.
    '    canvas.AddXObjectAt(formXObject2, 50, -50)
    '    srcDoc.Close()
    '    resultPdfDoc.Close()

    '    Response.ContentType = "application/pdf"
    '    Response.AddHeader("content-disposition", "attachment;" + "filename=z.pdf")
    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    Response.Write(resultPdfDoc)
    '    Response.End()
    'End Sub
End Class
