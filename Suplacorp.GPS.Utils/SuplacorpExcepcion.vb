Imports System.Net.Mail

<Serializable()> _
Public Class SuplacorpExcepcion
    Inherits ApplicationException

    Public Enum CodigoError
        ExcepcionAplicacion = 0
        AccesoSinUsuario = 1
    End Enum

    Private iCodError As CodigoError
    Public ReadOnly Property CodError() As CodigoError
        Get
            Return iCodError
        End Get

    End Property

    Private sMensaje As String
    Public ReadOnly Property Mensaje() As String
        Get
            Return sMensaje
        End Get

    End Property

    Private msgCorreo As MailMessage
    Private smtpServidor As SmtpClient

    Public Sub New(ByVal e As Exception)
        sMensaje = e.Message & e.Source & e.StackTrace '& e.TargetSite.ToString &  e.InnerException.ToString

        smtpServidor = New SmtpClient
        msgCorreo = New MailMessage
        smtpServidor = New SmtpClient

        msgCorreo.From = New MailAddress("sistemas@suplacorp.com.pe", "Sistemas")
        msgCorreo.To.Add("lcanales@suplacorp.com.pe")
        msgCorreo.Subject = String.Format("Sistema Comercial [{0}]", Now)
        msgCorreo.Body = sMensaje
        msgCorreo.IsBodyHtml = True
        msgCorreo.Priority = System.Net.Mail.MailPriority.High
        smtpServidor.Host = "10.1.1.251"

        Try
            smtpServidor.Send(msgCorreo)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)

        Finally
            msgCorreo.Dispose()
            smtpServidor = Nothing

        End Try

    End Sub



End Class
