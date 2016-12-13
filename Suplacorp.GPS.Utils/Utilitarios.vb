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


    'input: 01102015
    Public Shared Function FechaFormatoAAMMDD_BBVA(ByVal fecha As String) As String
        Dim dd As String = fecha.ToString.Substring(0, 2)
        Dim mm As String = fecha.ToString.Substring(2, 2)
        Dim aa As String = fecha.ToString.Substring(4, 4)
        Return String.Format("{0}-{1}-{2}", aa, mm, dd)
    End Function

    Public Shared Function HoraFormatoHHMMSS_BBVA(ByVal hora As String) As String
        Dim hh As String = hora.ToString.Substring(0, 2)
        Dim mm As String = hora.ToString.Substring(2, 2)
        Dim ss As String = hora.ToString.Substring(4, 2)
        Return String.Format("{0}:{1}:{2}", hh, mm, ss)
    End Function

    Public Shared Function DecimalFormato_BBVA(ByVal cantidad As String) As String
        'CAMBIANDO LA COMA (",") SEPARADORA DE DECIMALES EUROPEA POR EL PUNTO (".") SEPARADOR DE DECIMALES PERUANO
        If cantidad <> "" Then
            cantidad = cantidad.Replace(",", ".")
            Return cantidad
        Else
            Return 0
        End If
    End Function

    Public Shared Function DecimalFormato_SUPLA_BBVA(ByVal cantidad As String) As String
        'CAMBIANDO EL PUNTO (".") SEPARADOR DE DECIMALES PERUANO POR LA COMA (",") SEPARADOR DE DECIMALES EUROPEA
        If cantidad <> "" Then
            cantidad = cantidad.Replace(".", ",")
            Return cantidad
        Else
            Return 0
        End If
    End Function

    Public Shared Function QuitarExtensionNombreFichero_BBVA(ByVal nombreFichero As String) As String

        Try
            nombreFichero = nombreFichero.Remove(nombreFichero.Length - 4)
        Catch ex As Exception
            Throw
        End Try

        Return nombreFichero
    End Function

    Public Shared Function ObtenerNombreFicheroNeto(ByVal nombreFichero As String) As String
        If nombreFichero.Contains("PE_OL1_REFER") Then
            Return "PE_OL1_REFER"
        ElseIf nombreFichero.Contains("PE_OL1_SUMIN") Then
            Return "PE_OL1_SUMIN"
        ElseIf nombreFichero.Contains("PE_OL1_PREFAC") Then
            Return "PE_OL1_PREFAC"
        ElseIf nombreFichero.Contains("PE_OL1_EXPED_LOG_EXTERNO") Then
            Return "PE_OL1_EXPED_LOG_EXTERNO"
        ElseIf nombreFichero.Contains("PE_OL1_EXPED") Then
            Return "PE_OL1_EXPED"
        Else
            Return ""
        End If
    End Function


    Public Shared Function ObtenerRutaFicheroDestino(ByVal nombreFichero As String) As String
        ''Ruta_fichero_detino_Ref
        If nombreFichero.Contains("PE_OL1_REFER") Then
            Return GlobalVariables.Ruta_fichero_detino_Ref
        ElseIf nombreFichero.Contains("PE_OL1_SUMIN") Then
            Return GlobalVariables.Ruta_fichero_detino_Sum
        ElseIf nombreFichero.Contains("PE_OL1_PREFAC") Then
            Return GlobalVariables.Ruta_fichero_detino_Pref
        ElseIf nombreFichero.Contains("PE_OL1_EXPED_LOG_EXTERNO") Then
            Return GlobalVariables.Ruta_fichero_detino_Log_Exp
        ElseIf nombreFichero.Contains("PE_OL1_EXPED") Then
            Return GlobalVariables.Ruta_fichero_detino_Exp
        Else
            Return ""
        End If
    End Function


    Public Shared Function CompletarConCeros_Izquierda(ByVal valor As String, ByVal longitudMax As Integer) As String
        If (valor <> "") Then
            While (valor.Length < longitudMax)
                valor = "0" + valor
            End While
        End If
        Return valor
    End Function


End Class
