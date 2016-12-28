using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using Suplacorp.GPS.BE;
using Suplacorp.GPS.BL;
using System.Configuration;
using Suplacorp.GPS.Utils;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.Collections;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace BBVA_GPS_MonitorInterfacesAutomaticas
{
    class Program
    {
        static int cantProcesos = 0;

        static void Main(string[] args)
        {
            string drive = "";
            try
            {
                DefinirVariablesGlobales();

                InterfazExpedicionesBL objBL_ERROR = new InterfazExpedicionesBL();
                drive = Path.GetPathRoot(GlobalVariables.Ruta_sftp);

                Process[] lst_Processes_BBVA_GPS_InterfacesAutomaticas = Process.GetProcessesByName("BBVA_GPS_InterfacesAutomaticas");
                cantProcesos = lst_Processes_BBVA_GPS_InterfacesAutomaticas.Count();

                /* EL APLICATIVO NO SE ESTÁ EJECUTÁNDO */
                if (cantProcesos == 0)
                {
                    /* ERROR */
                    objBL_ERROR.EnviarCorreoElectronico(
                        objBL_ERROR.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - GENERAL]", "",
                        (objBL_ERROR.FormatearMensajeError_HTML(null, 0, "[ERROR CRÍTICO BBVA GPS] - El aplicativo: BBVA_GPS_InterfacesAutomaticas.exe no se está ejecutando.")), null);
                    return;
                }
                /* HAY MÁS DE 01 APLICATIVO EJECURÁNDOSE */
                else if (cantProcesos > 1)
                {
                    /* ERROR */
                    objBL_ERROR.EnviarCorreoElectronico(
                        objBL_ERROR.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - GENERAL]", "",
                        (objBL_ERROR.FormatearMensajeError_HTML(null, 0, "[ERROR CRÍTICO BBVA GPS] - Hay más de 1 aplicativo: BBVA_GPS_InterfacesAutomaticas.exe ejecutándose.")), null);
                    return;
                }
                /* VERIFICANDO SALUD DEL SFTP, QUE ESTÉ LEVANTADO, CONECTADO, FUNCIONANDO */
                else if (!Directory.Exists(drive)) {
                    objBL_ERROR.EnviarCorreoElectronico(
                        objBL_ERROR.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - GENERAL]", "",
                        (objBL_ERROR.FormatearMensajeError_HTML(null, 0, "[ERROR CRÍTICO BBVA GPS] - No se tiene acceso al SFTP desde SUPLACORPDB.")), null);
                    return;
                }

            }
            catch (Exception ex) {
                throw;
            }
        }

        private static void DefinirVariablesGlobales()
        {
            try
            {
                GlobalVariables.Ruta_sftp = System.Configuration.ConfigurationSettings.AppSettings["ruta_sftp"].ToString();
                GlobalVariables.Ruta_fichero_detino_Ref = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Ref"].ToString();
                GlobalVariables.Ruta_fichero_detino_Exp = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Exp"].ToString();
                GlobalVariables.Ruta_fichero_detino_Pref = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Pref"].ToString();
                GlobalVariables.Ruta_fichero_detino_Sum = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Sum"].ToString();
                GlobalVariables.Ruta_fichero_detino_Log_Exp = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_LogExp"].ToString();

                //CARGANDO VARIABLES DE BD
                Dictionary<string, object> lstVariables = new Dictionary<string, object>();
                lstVariables = (new ValidacionInterfazBL()).CargarVariablesIniciales();
                GlobalVariables.IdCliente = Convert.ToInt32(lstVariables["IDCLIENTE"]);

                /*LISTA DE CÓDIGO DE DEPARTAMENTOS SEGÚN BBVA (ELLOS MANEJAN SUS PROPIOS CÓDIGOS EN LA INTERFAZ)*/
                GlobalVariables.ListaDepartamentosBBVA = (new UtilBL()).ObtenerListaDepartamentosBBVA();
            }
            catch
            {
                throw;
            }
        }

    }
}
