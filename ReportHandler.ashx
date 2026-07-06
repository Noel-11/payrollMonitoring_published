<%@ WebHandler Language="VB" Class="ReportHandler" %>

Imports System
Imports System.Web
Imports System.Drawing.Imaging
Imports System.Web.SessionState


Public Class ReportHandler : Implements IHttpHandler, IRequiresSessionState

    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
       
     
  

        ' Stop buffering the response
        context.Response.Buffer = False
        ' Clear the response content and headers
        context.Response.ClearContent()
        context.Response.ClearHeaders()


        context.Response.Buffer = True
        context.Response.Charset = ""

        context.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        context.Response.ContentType = "application/pdf"
        
        context.Response.BinaryWrite(context.Session("pdfBytes"))
         
        context.Response.Flush()
        context.Response.[End]()
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class