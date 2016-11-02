Imports System.Text
Imports System.Data
Imports System.Xml
Imports System.IO


Public Class XML
    Public Const inicial As String = "<?xml version=""1.0"" encoding=""iso-8859-1"" ?><ROOT></ROOT>"

    Public Shared Function CargarXML(ByVal p_cArchivo As String) As DataTable

        Dim dstArchivo As New DataSet
        Dim dtbResultado As New DataTable

        Dim FS As FileStream
        Dim Reader As StreamReader

        Try

            FS = New FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location) & "\" & p_cArchivo, FileMode.Open, FileAccess.Read)
            Reader = New StreamReader(FS, Encoding.GetEncoding(1252))
            dstArchivo.ReadXml(Reader)

            dtbResultado = dstArchivo.Tables(0)
            Return dtbResultado

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)

        Finally
            'FS.Close()

        End Try

    End Function
    Public Shared Function CargarConexionXML(ByVal connection As String) As String

        Try
            Dim m_xmld As XmlDocument
            Dim m_nodelist As XmlNodeList
            Dim m_node As XmlNode
            Dim connectionvalue As String = String.Empty

            m_xmld = New XmlDocument()
            m_xmld.Load("C:\support\websystem.xml")
            m_nodelist = m_xmld.SelectNodes("/suplacorp/connection")

            For Each m_node In m_nodelist

                If connection = m_node.Attributes.GetNamedItem("name").Value Then
                    connectionvalue = m_node.ChildNodes.Item(0).InnerText
                    Exit For

                End If

            Next

            Return connectionvalue

        Catch ex As Exception
            Throw ex

        End Try

    End Function
End Class
