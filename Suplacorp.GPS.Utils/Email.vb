Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime

Public Class Email

    Public Property emailTo As String
    Public Property emailCC As String = String.Empty
    Public Property emailBC As String = String.Empty
    Public Property isBodyHtml As Boolean
    Public Property body As String = String.Empty
    Public Property subject As String = String.Empty
    Public Property fileName As String = String.Empty
    Public Property fileName2 As String = String.Empty

    Public attachment As System.Net.Mail.Attachment

#Region "constantes"
    Private emailFrom As String = "suplacorp@suplacorp.com.pe"
    Private pwdFrom As String = "iso90013"
#End Region

    Public Function EnviaCorreo() As Boolean
        Dim message As New MailMessage()
        Dim destinatarios As String()
        Dim CC As String()
        Dim CCO As String()

        Try
            destinatarios = emailTo.Split(",")
            CC = emailCC.Split(",")
            CCO = emailBC.Split(",")

            message.From = New MailAddress(emailFrom)

            For Each destinatario As String In destinatarios
                If destinatario <> "" Then message.To.Add(New MailAddress(destinatario))
            Next

            For Each destinatarioCC As String In CC
                If destinatarioCC <> "" Then message.CC.Add(New MailAddress(destinatarioCC))
            Next

            For Each destinatarioCCO As String In CCO
                If destinatarioCCO <> "" Then message.Bcc.Add(New MailAddress(destinatarioCCO))
            Next

            'Attach file
            If (Me.fileName.Length > 0 And System.IO.File.Exists(fileName)) Then
                attachment = New System.Net.Mail.Attachment(Me.fileName)
                message.Attachments.Add(attachment)
            End If

            'Attach file2
            If (Me.fileName2.Length > 0 And System.IO.File.Exists(fileName2)) Then
                attachment = New System.Net.Mail.Attachment(Me.fileName2)
                message.Attachments.Add(attachment)
            End If

            message.Subject = subject
            message.IsBodyHtml = isBodyHtml
            message.Body = body
            Dim clienteSmtp As New SmtpClient()
            clienteSmtp.UseDefaultCredentials = True
            clienteSmtp.Host = "smtp.gmail.com"
            clienteSmtp.Port = 587 '465
            clienteSmtp.EnableSsl = True
            clienteSmtp.Timeout = 20000
            clienteSmtp.Credentials = New NetworkCredential(emailFrom, pwdFrom)
            clienteSmtp.Send(message)

            Return True
        Catch ex As Exception
            Throw
        Finally

            message.Dispose()
            message = Nothing
        End Try

    End Function
End Class
