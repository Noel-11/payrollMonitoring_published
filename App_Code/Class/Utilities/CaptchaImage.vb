Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms
Imports System.Reflection

'*****************************************************************************************
'                           LICENSE INFORMATION
'*****************************************************************************************
'   PC_CaptchaImage Version 1.0.0.4
'   Creates a CAPTCHA (security) image to help prevent "bot spamming" and other data
'   input into online web systems via a software "bot"
'
'   Copyright (C) 2007  
'   Richard L. McCutchen 
'   Email: richard@psychocoder.net
'   Created: 27JUL07
'
'   This program is free software: you can redistribute it and/or modify
'   it under the terms of the GNU General Public License as published by
'   the Free Software Foundation, either version 3 of the License, or
'   (at your option) any later version.
'
'   This program is distributed in the hope that it will be useful,
'   but WITHOUT ANY WARRANTY; without even the implied warranty of
'   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'   GNU General Public License for more details.
'
'   You should have received a copy of the GNU General Public License
'   along with this program.  If not, see <http://www.gnu.org/licenses/>.
'*****************************************************************************************

Namespace PC
    Public Class CaptchaImage
#Region " Class Variables "
#Region " Property Variables "
        Private _text As String
        Private _width As Integer
        Private _height As Integer
        Private _failure As String
        Private _hatch As HatchStyles
        Private _mainColor As Color
        Private _secondaryColor As Color
#End Region

#Region " Global Variables "
        Private _image As Bitmap
        Private ran As Random = New Random
        Private fontFamily As String
#End Region
#End Region

#Region " Class Properties "
#Region " Read/Write "
        ''' <summary>
        ''' Property to hold the failure message to be displayed to the user
        ''' </summary>
        ''' <value>A string the developer choses to use</value>
        ''' <returns>The string message (or a NullReferenceException if it isnt provided)</returns>
        ''' <remarks></remarks>
        Public Property FailureMessage() As Object
            Get
                Return _failure
            End Get
            Set(ByVal value As Object)
                If String.IsNullOrEmpty(value) Then
                    Throw New NullReferenceException("A return message wasn't specified and is required.")
                    _failure = String.Empty
                Else
                    _failure = value
                End If
            End Set
        End Property
#End Region

#Region " Readonly "
        ''' <summary>
        ''' Readonly property to hold the text of the CAPTCHA image
        ''' </summary>
        ''' <value></value>
        ''' <returns>String value</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Text() As String
            Get
                Return _text
            End Get
        End Property

        ''' <summary>
        ''' Readonly property to hold the image
        ''' </summary>
        ''' <value></value>
        ''' <returns>A bitmap image</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Image() As Bitmap
            Get
                Return _image
            End Get
        End Property

        ''' <summary>
        ''' Readonly property to hold the width of the image
        ''' </summary>
        ''' <value></value>
        ''' <returns>Int value</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Width() As Integer
            Get
                Return _width
            End Get
        End Property

        ''' <summary>
        ''' Readonly property to hold the height of the image
        ''' </summary>
        ''' <value></value>
        ''' <returns>Int value</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Height() As Integer
            Get
                Return _height
            End Get
        End Property

        ''' <summary>
        ''' Readonly property to hold the main color of the image
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property MainColor() As Color
            'return the color name list
            Get
                Return _mainColor
            End Get
        End Property

        ''' <summary>
        ''' Readonly property to hold the secondaty color of the image
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SecondaryColor() As Color
            'return the color name list
            Get
                Return _secondaryColor
            End Get
        End Property

        ''' <summary>
        ''' Readonly property to hold the style of HatchBrush used
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HatchType() As HatchStyles
            Get
                Return _hatch
            End Get
        End Property
#End Region
#End Region

#Region " Enumerations "
        'Enumeration to hold the default dimensions of the CAPTCHA
        Enum DefaultPicSize
            Width = 150
            Height = 60
        End Enum

        'Enumerationh to hold HatchStyle values of the HatchBrush
        Enum HatchStyles
            LargeConfetti = HatchStyle.LargeConfetti
            Grid = HatchStyle.LargeGrid
            Cross = HatchStyle.DiagonalCross
            LargeCheckerBoard = HatchStyle.LargeCheckerBoard
            ZigZag = HatchStyle.ZigZag
            Diamond = HatchStyle.SolidDiamond
            SmallCheckerBoard = HatchStyle.SmallCheckerBoard
            SmallConfetti = HatchStyle.SmallConfetti
        End Enum
#End Region

#Region " Instantiation & Disposal "
        ''' <summary>
        ''' First New Constructor
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            _text = generateRandomString()
            _hatch = HatchStyles.ZigZag
            _mainColor = Color.Gray
            _secondaryColor = Color.White
            SetPicSize(DefaultPicSize.Height, DefaultPicSize.Height)
            SetFont()
            BuildCAPTCHA()
        End Sub

        ''' <summary>
        ''' Second New Constructor
        ''' </summary>
        ''' <param name="width">Width of the image</param>
        ''' <param name="height">Height of the image</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal width As Integer, ByVal height As Integer)
            _text = generateRandomString()
            _hatch = HatchStyles.ZigZag
            _mainColor = Color.Gray
            _secondaryColor = Color.White
            SetPicSize(width, height)
            SetFont()
            BuildCAPTCHA()
        End Sub

        ''' <summary>
        ''' Third New Constructor
        ''' </summary>
        ''' <param name="width">Width of the image</param>
        ''' <param name="height">Height of the image</param>
        ''' <param name="fontName">Name of the font to be used</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal width As Integer, ByVal height As Integer, ByVal fontName As String)
            _text = generateRandomString()
            _hatch = HatchStyles.ZigZag
            _mainColor = Color.Gray
            _secondaryColor = Color.White
            SetPicSize(width, height)
            SetFont(fontName)
            BuildCAPTCHA()
        End Sub
        ''' <summary>
        ''' Fourth New Constructor
        ''' </summary>
        ''' <param name="width">Width of the image</param>
        ''' <param name="height">Height of the image</param>
        ''' <param name="fontName">Name of the font to be used</param>
        ''' <param name="hatch">HatchBrush to use</param>
        ''' <param name="maincolor">Main color of the image</param>
        ''' <param name="secondarycolor">Secondary color of the image</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal width As Integer, ByVal height As Integer, ByVal fontName As String, _
        ByVal hatch As HatchStyles, ByVal maincolor As Color, ByVal secondarycolor As Color)
            _text = generateRandomString()
            _hatch = hatch
            _mainColor = maincolor
            _secondaryColor = secondarycolor
            SetPicSize(width, height)
            SetFont(fontName)
            BuildCAPTCHA()
        End Sub

        ''' <summary>
        ''' Finalize method
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overrides Sub Finalize()
            Dispose(False)
        End Sub

        ''' <summary>
        ''' Dispose method
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Dispose()
            GC.SuppressFinalize(Me)
            Dispose(True)
        End Sub

        ''' <summary>
        ''' Overridable Dispose method (uses the boolean disposing variable)
        ''' </summary>
        ''' <param name="disposing"></param>
        ''' <remarks></remarks>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                Image.Dispose()
            End If
        End Sub
#End Region

#Region " Set Image Properties Methods "
        ''' <summary>
        ''' Method for setting the size of the image
        ''' </summary>
        ''' <param name="width">Desired width of the image</param>
        ''' <param name="height">Desired height of the image</param>
        ''' <remarks></remarks>
        Private Sub SetPicSize(ByVal width As Integer, ByVal height As Integer)
            'Check to make sure they didnt supply a width of zero, if they did then throw an exception
            If width <= 0 Then Throw New ArgumentOutOfRangeException("width", width, "Argument out of range, must be greater than zero.")
            'Check to make sure they didnt supply a height of zero, if they did then throw an exception
            If height <= 0 Then Throw New ArgumentOutOfRangeException("height", height, "Argument out of range, must be greater than zero.")

            'Set the width & height of the image
            _width = width
            _height = height
        End Sub

        ''' <summary>
        ''' Procedure to set the font to be used on the CAPTCHA image
        ''' </summary>
        ''' <param name="fontName">Name of the font (Verdana, Arial, Tahoma, etc)</param>
        ''' <remarks></remarks>
        Private Sub SetFont(Optional ByVal fontName As String = "")
            Dim font As Font
            Try
                'If the user specified a font then use it
                If Not String.IsNullOrEmpty(fontName) Then
                    font = New Font(fontName, 8.0F)
                    fontName = fontName
                Else
                    'Otherwise default to Verdana
                    font = New Font("Verdana", 8.0F)
                    fontName = fontName
                End If
                font.Dispose()
            Catch ex As Exception
                'This ensures that if a font is specified that isnt on the end users
                'computer than a system font will be used
                fontName = System.Drawing.FontFamily.GenericSerif.Name
            End Try
        End Sub
#End Region

#Region " Generate Image "
        ''' <summary>
        ''' Method for generating the CAPTCHA image
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub BuildCAPTCHA()
            'Create a new 32-bit bitmap image.
            Dim captchaImage As Bitmap = New Bitmap(Width, Height, PixelFormat.Format32bppArgb)

            'Create a graphics object for drawing.
            Dim oGraphic As Graphics = Graphics.FromImage(captchaImage)
            oGraphic.SmoothingMode = SmoothingMode.AntiAlias
            Dim rect As Rectangle = New Rectangle(0, 0, Width, Height)

            'Fill in the background.
            Dim hbBrush As HatchBrush = New HatchBrush(_hatch, MainColor, SecondaryColor)
            oGraphic.FillRectangle(hbBrush, rect)

            'Set up the text font.
            Dim size As SizeF
            Dim captchaFontSize As Integer = rect.Height
            Dim captchaFont As Font

            'Adjust the font size until the text fits within the image.
            'Do
            '    System.Math.Max(Threading.Interlocked.Decrement(captchaFontSize), captchaFontSize + 1)
            captchaFont = New Font(fontFamily, captchaFontSize, FontStyle.Bold)
            size = oGraphic.MeasureString(Text, captchaFont)
            'Loop While size.Width > rect.Width

            'Set up the text format.
            Dim format As New StringFormat
            format.Alignment = StringAlignment.Center
            format.LineAlignment = StringAlignment.Center

            'Create a path using the text and warp it randomly.
            Dim gpCaptchaPath As GraphicsPath = New GraphicsPath
            gpCaptchaPath.AddString(Text, captchaFont.FontFamily, CType(captchaFont.Style, Integer), captchaFont.Size, rect, format)
            Dim captchaSingle As Single = 4.0F

            Dim points As PointF() = {New PointF(ran.Next(rect.Width) / captchaSingle, ran.Next(rect.Height) / captchaSingle), _
            New PointF(rect.Width - ran.Next(rect.Width) / captchaSingle, ran.Next(rect.Height) / captchaSingle), _
            New PointF(ran.Next(rect.Width) / captchaSingle, rect.Height - ran.Next(rect.Height) / captchaSingle), _
            New PointF(rect.Width - ran.Next(rect.Width) / captchaSingle, rect.Height - ran.Next(rect.Height) / captchaSingle)}

            Dim captchaMatrix As Matrix = New Matrix
            captchaMatrix.Translate(0.0F, 0.0F)
            gpCaptchaPath.Warp(points, rect, captchaMatrix, WarpMode.Perspective, 0.0F)

            'Draw the text.
            hbBrush = New HatchBrush(HatchStyle.ZigZag, Color.Black, Color.Black)
            oGraphic.FillPath(hbBrush, gpCaptchaPath)

            'Add some random noise.
            Dim m As Integer = Math.Max(rect.Width, rect.Height)
            Dim iCount As Integer = 0
            While iCount < CType((rect.Width * rect.Height / 30.0F), Integer)
                Dim xAxis As Integer = ran.Next(rect.Width)
                Dim yAxis As Integer = ran.Next(rect.Height)
                Dim width As Integer = ran.Next(m / 50)
                Dim height As Integer = ran.Next(m / 50)
                oGraphic.FillEllipse(hbBrush, xAxis, yAxis, width, height)
                System.Math.Min(System.Threading.Interlocked.Increment(iCount), iCount - 1)
            End While

            'Clean up after ourselves
            captchaFont.Dispose()
            hbBrush.Dispose()
            oGraphic.Dispose()

            'Set the final image and return it
            _image = captchaImage
        End Sub

        Private Function generateRandomString() As String

            Return Left(Guid.NewGuid.ToString.Replace("-", ""), 8)
        End Function
#End Region

#Region " Helpers "
        ''' <summary>
        ''' Function to generate the random string to be displayed on the image
        ''' </summary>
        ''' <param name="size">Length of the random string</param>
        ''' <returns>A 5 character random string</returns>
        ''' <remarks>
        ''' To keep track of do this:
        ''' Session("CaptchaText") = GenerateRandomString
        '''</remarks>
        Public Function GenerateRandomString(ByVal size As Integer) As String
            Dim captchaString As String = ""
            Dim iCount As Integer = 0
            Try
                While iCount < size
                    captchaString = String.Concat(captchaString, ran.Next(10).ToString)
                    System.Math.Min(System.Threading.Interlocked.Increment(iCount), iCount - 1)
                End While
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error Generating Code", MessageBoxButtons.OK, MessageBoxIcon.Error)
                captchaString = String.Empty
            End Try
            Return captchaString
        End Function
#End Region
    End Class
End Namespace

