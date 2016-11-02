
Public Class GeneradorPasswordAleatorio
    Public Shared Function GenerarPassword_UW_Suplasoft(ByVal nombres As String, ByVal apellidopat As String, ByVal apellidomat As String)
        Dim aleatorio As Integer
        Dim aleatorio1 As Integer
        Dim primera As String = ""
        Dim segunda As String = ""
        Dim tercera As String = ""
        Dim cuarta As String = ""
        Dim password As String = ""
        Dim Random As New Random()


        Try
            nombres = IIf(nombres = "", " ", nombres)
            apellidopat = IIf(apellidopat = "", " ", apellidopat)
            apellidomat = IIf(apellidomat = "", " ", apellidomat)

            aleatorio = Asc(nombres) * 10 + Asc(apellidopat) * 11 + Asc(apellidomat) * 5 + Int(Rnd(100) * 1000) + Day(Date.Now) + Hour(Date.Now) + Second(Date.Now) * 12
            aleatorio1 = Int(Rnd(100) * 1000) + Day(Date.Now) + Hour(Date.Now) + Second(Date.Now) * 12

            Select Case Mid(aleatorio, Len(aleatorio) - 1, 1)
                Case "0" : primera = "A"
                    tercera = "j"
                Case "1" : primera = "F"
                    tercera = "d"
                Case "2" : primera = "K"
                    tercera = "M"
                Case "3" : primera = "O"
                    tercera = "w"
                Case "4" : primera = "U"
                    tercera = "i"
                Case "5" : primera = "P"
                    tercera = "z"
                Case "6" : primera = "T"
                    tercera = "r"
                Case "7" : primera = "S"
                    tercera = "x"
                Case "8" : primera = "Z"
                    tercera = "q"
                Case "9" : primera = "Q"
                    tercera = "i"
            End Select

            Select Case Mid(aleatorio, 1, 1)
                Case "0" : segunda = "A"
                    cuarta = "j"
                Case "1" : segunda = "F"
                    cuarta = "d"
                Case "2" : segunda = "K"
                    cuarta = "m"
                Case "3" : segunda = "O"
                    cuarta = "w"
                Case "4" : segunda = "U"
                    cuarta = "i"
                Case "5" : segunda = "P"
                    cuarta = "z"
                Case "6" : segunda = "T"
                    cuarta = "r"
                Case "7" : segunda = "S"
                    cuarta = "x"
                Case "8" : segunda = "Z"
                    cuarta = "q"
                Case "9" : segunda = "Q"
                    cuarta = "i"
            End Select

            If Len(Second(TimeOfDay)) > 1 Then
                Select Case (Second(TimeOfDay)).ToString().Substring(1, Second(TimeOfDay).ToString().Length - 1)
                    Case "0", "4", "9", "8" : password = primera & segunda & aleatorio & tercera & cuarta & Random.Next(1, 100)
                    Case "1", "6", "5" : password = primera & segunda & cuarta & tercera & aleatorio & Random.Next(1, 100)
                    Case "2", "3", "7" : password = aleatorio & segunda & primera & tercera & cuarta & Random.Next(1, 100)
                    Case Else : password = aleatorio & primera & segunda & aleatorio & Random.Next(1, 100)
                End Select
            Else
                password = primera & segunda & aleatorio & tercera & cuarta
            End If

            password = Replace(password, "0", "Z")
            password = Replace(password, "o", "p")
            password = Replace(password, "O", "P")
            password = Replace(password, "1", "U")
            password = Replace(password, "l", "e")
            password = Replace(password, "L", "E")
            password = Replace(password, "I", "J")
            password = Replace(password, "i", "j")
        Catch ex As Exception
            Throw
        End Try
        
        Return password
    End Function

End Class
