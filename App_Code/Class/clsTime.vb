Imports Microsoft.VisualBasic

Public Class clsTime
    Public Function isAMPM(ByVal _hour As Integer) As String
        If _hour < 12 Then
            Return "AM"
        Else
            Return "PM"
        End If
    End Function

    Public Function hour_12HourFormat(ByVal _hour As Integer) As Integer
        If _hour = 0 Then
            Return 12
        End If
        If _hour > 12 Then
            Return _hour - 12
        End If
        Return _hour
    End Function
    Public Function hour_24HourFormat(ByVal _hour As Integer, ByVal _ampm As String) As Integer
        If _ampm = "PM" Then
            If _hour < 12 Then
                _hour += 12
            End If
        End If

        If _ampm = "AM" And _hour = 12 Then
            _hour = 0
        End If

        Return _hour
    End Function
    Public Function isBetweenTime(ByVal this_hr As Integer, ByVal this_min As Integer, ByVal from_hr As Integer, ByVal from_min As Integer, ByVal to_hr As Integer, ByVal to_min As Integer) As Boolean

        If from_hr = to_hr Then
            If this_hr = from_hr And this_min >= from_min And this_min <= to_min Then
                Return True
            End If
        End If

        If from_hr <> to_hr Then
            If this_hr >= from_hr And this_hr <= to_hr Then
                If this_hr = from_hr And this_min >= from_min Then
                    Return True
                ElseIf this_hr = to_hr And this_min <= to_min Then
                    Return True
                ElseIf this_hr > from_hr And this_hr < to_hr Then
                    Return True
                End If
            End If
        End If

        If from_hr > to_hr Then
            If this_hr >= from_hr Then
                If this_hr = from_hr And this_min >= from_min Then
                    Return True
                ElseIf this_hr = to_hr And this_min <= to_min Then
                    Return True
                ElseIf this_hr > from_hr Or this_hr < to_hr Then
                    Return True
                End If
            End If

            If this_hr <= to_hr Then
                If this_hr < to_hr Then
                    Return True
                ElseIf this_hr = to_hr And this_min <= to_min Then
                    Return True
                End If
            End If
        End If

     
        
        Return False
    End Function

    Public Sub populateTimeDDL(ByRef ddl As DropDownList, ByVal timetype As String)
        Select Case timetype
            Case "HOUR"
                For ctr As Integer = 1 To 12
                    With ddl
                        If ctr < 10 Then
                            .Items.Insert(ctr - 1, New ListItem("0" & ctr.ToString, "0" & ctr))
                        Else
                            .Items.Insert(ctr - 1, New ListItem(ctr.ToString, ctr))
                        End If
                    End With
                Next
            Case "MINUTE"
                For ctr As Integer = 1 To 60
                    With ddl
                        If ctr < 11 Then
                            .Items.Insert(ctr - 1, New ListItem("0" & (ctr - 1).ToString, "0" & ctr - 1))
                        Else
                            .Items.Insert(ctr - 1, New ListItem((ctr - 1).ToString, ctr - 1))
                        End If
                    End With
                Next

        End Select

    End Sub
End Class
