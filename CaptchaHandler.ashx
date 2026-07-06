<%@ WebHandler Language="VB" Class="CaptchaHandler" %>

Imports System
Imports System.Web
Imports System.Drawing.Imaging
Imports System.Web.SessionState

Public Class CaptchaHandler : Implements IHttpHandler, IRequiresSessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        'Declare The CaptchaObject
        Dim xcaptcha As PC.CaptchaImage
        'Create The Captcha Object		
        xcaptcha = New PC.CaptchaImage(300, 50)
        'Set the Captcha Text to a session variable
        context.Session("CaptchaText") = xcaptcha.Text
        'Set the response of this page to an image
        context.Response.ContentType = "image/jpeg"
        'output the image to the response
        xcaptcha.Image.Save(context.Response.OutputStream, ImageFormat.Jpeg)
        'Close and Dispose the Captcha Object
        xcaptcha.Dispose()
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class