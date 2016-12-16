Public Module GlobalVariables

    Public Global_ConnectionString_Suplaweb As String = ""
    Public Global_Nombre_BD_Suplaweb As String = ""

    Public Ruta_sftp As String = ""
    Public Ruta_fichero_detino_Ref As String = ""
    Public Ruta_fichero_detino_Exp As String = ""
    Public Ruta_fichero_detino_Log_Exp As String = ""
    Public Ruta_fichero_detino_Pref As String = ""
    Public Ruta_fichero_detino_Sum As String = ""


    Public IdCliente As Integer = 0

    Public ListaDepartamentosBBVA As New Dictionary(Of Integer, String)

    Public Enum Interfaz
        Referencias = 1
        Suministros = 2
        Expediciones = 3
        Prefacturas = 4
    End Enum

End Module
