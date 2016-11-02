Imports System.Text.RegularExpressions
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

Public Class Utilitarios

    Public Shared Function Moneda_SegunIDMoneda(ByVal idMoneda As Integer) As String
        If idMoneda = 1 Then
            Return "US$"
        Else
            Return "S/."
        End If
    End Function

    Public Shared Function Moneda_SegunMoneda(ByVal idMoneda As Integer) As String
        If idMoneda = 1 Then
            Return "US$"
        Else
            Return "S/."
        End If
    End Function

    Public Shared Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)

        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

    Public Shared Function ObtenerDescripcionMes(ByVal mes As Integer)
        Select Case mes
            Case 0
                Return ""
            Case 1
                Return "ENERO "
            Case 2
                Return "FEBRERO "
            Case 3
                Return "MARZO "
            Case 4
                Return "ABRIL "
            Case 5
                Return "MAYO "
            Case 6
                Return "JUNIO "
            Case 7
                Return "JULIO "
            Case 8
                Return "AGOSTO "
            Case 9
                Return "SEPTIEMBRE "
            Case 10
                Return "OCTUBRE "
            Case 11
                Return "NOVIEMBRE "
            Case 12
                Return "DICIEMBRE "
        End Select

    End Function

    'ENCRIPTA SHA1
    Public Shared Function EncriptaSHA(ByVal strcadena As String) As String
        Dim Codificacion As New UTF8Encoding
        Dim data() As Byte = Codificacion.GetBytes(strcadena)
        Dim resultado() As Byte
        Dim sha As New SHA1CryptoServiceProvider()
        resultado = sha.ComputeHash(data)
        Dim sb As New StringBuilder
        For i As Integer = 0 To resultado.Length - 1
            If resultado(i) < 16 Then
                sb.Append("0")
            End If
            sb.Append(resultado(i).ToString("x"))
        Next
        Return sb.ToString().ToUpper

    End Function

    Public Shared Function DesencriptaSHA(ByVal strcadena As String) As String
        Dim Codificacion As New UTF8Encoding
        Dim data() As Byte = Codificacion.GetBytes(strcadena)
        Dim resultado() As Byte
        Dim sha As New SHA1CryptoServiceProvider()
        resultado = sha.ComputeHash(data)
        Dim sb As New StringBuilder
        For i As Integer = 0 To resultado.Length - 1
            If resultado(i) < 16 Then
                sb.Append("0")
            End If
            sb.Append(resultado(i).ToString("x"))
        Next
        Return sb.ToString().ToUpper


    End Function

    Public Shared Function FechaFormatoAAMMDD(ByVal fecha As String) As String
        Dim dd As String = fecha.ToString.Substring(0, 2)
        Dim mm As String = fecha.ToString.Substring(3, 2)
        Dim aa As String = fecha.ToString.Substring(6, 4)
        Return String.Format("{0}{1}{2}", aa, mm, dd)
    End Function

End Class
