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
using System.Timers;

namespace BBVA_GPS_MonitorInterfacesAutomaticas
{
    class Program
    {
        static int cantProcesos = 0;

        #region VARIABLES PARA DESHABILITAR EL BOTÓN DE CERRAR "X" DE LA CONSOLA
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        #endregion

        static void Main(string[] args)
        {
            
            try
            {
                DeshabilitarBotonCerrarConsola();

                DefinirVariablesGlobales();
                
                /* TIMER DE VALIDACIÓN */
                System.Timers.Timer aTimer = new System.Timers.Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Interval = 60000; /*VALIDACIÓN SE LANZA CADA 60 SEGUNDOS (1 MINUTO)*/
                aTimer.Enabled = true;

                Console.WriteLine("Presionar \'q\' para terminar el MONITOREO del aplicativo BBVA_GPS_InterfacesAutomaticas.");
                while (Console.Read() != 'q') ;
            }
            catch (Exception) {
                throw;
            }
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            try{
                Validaciones();
            }
            catch{
                throw;
            }
        }

        private static void Validaciones()
        {
            try
            {
                string drive = "";
                InterfazExpedicionesBL objBL_ERROR = new InterfazExpedicionesBL();
                drive = Path.GetPathRoot(GlobalVariables.Ruta_sftp);

                Process[] lst_Processes_BBVA_GPS_InterfacesAutomaticas = Process.GetProcessesByName("BBVA_GPS_InterfacesAutomaticas");
                cantProcesos = lst_Processes_BBVA_GPS_InterfacesAutomaticas.Count();

                /* [VALIDACIÓN 1] EL APLICATIVO NO SE ESTÁ EJECUTÁNDO */
                if (cantProcesos == 0)
                {
                    /* ERROR */
                    objBL_ERROR.EnviarCorreoElectronico(
                        objBL_ERROR.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - GENERAL]", "",
                        (objBL_ERROR.FormatearMensajeError_HTML(null, 0, "[ERROR CRÍTICO BBVA GPS] - El aplicativo: BBVA_GPS_InterfacesAutomaticas.exe no se está ejecutando.")), null);
                    /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                    Console.WriteLine(objBL_ERROR.FormatearMensajeError_CONSOLA(null, 0, "[ERROR CRÍTICO BBVA GPS] - El aplicativo: BBVA_GPS_InterfacesAutomaticas.exe no se está ejecutando."));
                    return;
                }
                /* [VALIDACIÓN 2] NO DEBE HABER MÁS DE UNA INSTANCIA DEL APLICATIVO "BBVA_GPS_InterfacesAutomaticas" EJECURÁNDOSE */
                else if (cantProcesos > 1)
                {
                    /* ERROR */
                    objBL_ERROR.EnviarCorreoElectronico(
                        objBL_ERROR.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - GENERAL]", "",
                        (objBL_ERROR.FormatearMensajeError_HTML(null, 0, "[ERROR CRÍTICO BBVA GPS] - Hay más de 1 aplicativo: BBVA_GPS_InterfacesAutomaticas.exe ejecutándose.")), null);
                    /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                    Console.WriteLine(objBL_ERROR.FormatearMensajeError_CONSOLA(null, 0, "[ERROR CRÍTICO BBVA GPS] - Hay más de 1 aplicativo: BBVA_GPS_InterfacesAutomaticas.exe ejecutándose."));
                    return;
                }
                /* [VALIDACIÓN 3] VERIFICANDO "SALUD" DEL SFTP, QUE ESTÉ LEVANTADO, CONECTADO, FUNCIONANDO */
                else if (!Directory.Exists(drive))
                {
                    objBL_ERROR.EnviarCorreoElectronico(
                        objBL_ERROR.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - GENERAL]", "",
                        (objBL_ERROR.FormatearMensajeError_HTML(null, 0, "[ERROR CRÍTICO BBVA GPS] - No se tiene acceso al SFTP desde SUPLACORPDB.")), null);
                    /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                    Console.WriteLine(objBL_ERROR.FormatearMensajeError_CONSOLA(null, 0, "[ERROR CRÍTICO BBVA GPS] - No se tiene acceso al SFTP desde SUPLACORPDB."));
                    return;
                }
                /* EJECUCCIÓN DEL MONITEOREO CORRECTA */
                else
                {
                    Console.WriteLine("[EJECUCCIÓN DEL MONITEOREO CORRECTA] - Monitoreo del aplicativo BBVA_GPS_InterfacesAutomaticas " + DateTime.Today.ToString());
                }
            }
            catch
            {
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
                /*
                Dictionary<string, object> lstVariables = new Dictionary<string, object>();
                lstVariables = (new ValidacionInterfazBL()).CargarVariablesIniciales();
                GlobalVariables.IdCliente = Convert.ToInt32(lstVariables["IDCLIENTE"]);
                */

                /*LISTA DE CÓDIGO DE DEPARTAMENTOS SEGÚN BBVA (ELLOS MANEJAN SUS PROPIOS CÓDIGOS EN LA INTERFAZ)*/
                //GlobalVariables.ListaDepartamentosBBVA = (new UtilBL()).ObtenerListaDepartamentosBBVA();
            }
            catch
            {
                throw;
            }
        }

        private static void DeshabilitarBotonCerrarConsola()
        {
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);
            //Console.Read();
        }

    }
}
