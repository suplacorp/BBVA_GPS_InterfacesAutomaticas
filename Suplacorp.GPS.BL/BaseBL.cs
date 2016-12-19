using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;
using System.IO;
using Suplacorp.GPS.Utils;
using Suplacorp.GPS.DAL;

namespace Suplacorp.GPS.BL
{
    public abstract class BaseBL<T>
    {

        #region Constructor
        public BaseBL(){

        }
        #endregion
        
        public void EnviarCorreoElectronico(string emailPara, string emailConCopia, string asunto, string fileName, string cuerpoCorreo, string fileName2) {
            Email email = new Email();
            try
            {
                
                email.emailTo = emailPara;
                email.emailCC = emailConCopia;
                email.subject = asunto;
                email.isBodyHtml = true;
                email.fileName = fileName;
                email.fileName2 = fileName2;
                email.body = cuerpoCorreo;
                email.EnviaCorreo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {
                email = null;
            }
        }

        public string FormatearMensajeError_HTML(Exception ex, int idError, string nombreInterfaz) {

            StringBuilder strBuilding = new StringBuilder();
            string correoAux = "";
            
            try
            {
                correoAux = "<br /><font style='background-color:red;color:white;font-size:large'>Detale del error:</b></font>";
                strBuilding.Append(correoAux);

                if (ex != null)
                {
                    correoAux = "<hr /> " + "\r\n" +
                        "<table border='1' class='tamanoTabla' style='background-color:yellow;font-size:large'> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Nombre de Interfaz</td> " + "\r\n" +
                        "        <td>" + nombreInterfaz + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>ID Error (Tabla dbo.ERRORSISTEMAS)</td> " + "\r\n" +
                        "        <td>" + idError + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Exception.Message</td> " + "\r\n" +
                        "        <td>" + ex.Message + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Exception.InnerException</td> " + "\r\n" +
                        "        <td>" + ex.InnerException + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Exception.Source</td> " + "\r\n" +
                        "        <td>" + ex.Source + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Exception.StackTrace</td> " + "\r\n" +
                        "        <td>" + ex.StackTrace + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Exception.TargetSite</td> " + "\r\n" +
                        "        <td>" + ex.TargetSite + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Fecha Error</td> " + "\r\n" +
                        "        <td>" + DateTime.Today.ToString("dd/MM/yyyy") + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Hora Error</td> " + "\r\n" +
                        "        <td>" + DateTime.Now.ToString("h:mm:ss tt") + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "</table>";
                }
                else {
                    correoAux = "<hr /> " + "\r\n" +
                        "<table border='1' class='tamanoTabla' style='background-color:yellow;font-size:large'> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Nombre de Interfaz</td> " + "\r\n" +
                        "        <td>" + nombreInterfaz + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>ID Error (Tabla dbo.ERRORSISTEMAS)</td> " + "\r\n" +
                        "        <td>" + idError + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Fecha Error</td> " + "\r\n" +
                        "        <td>" + DateTime.Today.ToString("dd/MM/yyyy") + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>Hora Error</td> " + "\r\n" +
                        "        <td>" + DateTime.Now.ToString("h:mm:ss tt") + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "</table>";
                }
                strBuilding.Append(correoAux);
            }
            catch {
                throw;
            }
            return strBuilding.ToString();
        }

        public string FormatearMensajeError_CONSOLA(Exception ex, int idError, string nombreInterfaz)
        {

            StringBuilder strBuilding = new StringBuilder();
            string correoAux = "";

            try
            {
                correoAux = "############################################################" + "\r\n" +
                            "########################### ERROR ##########################" + "\r\n" +
                            "############################################################" + "\n\r";
                if (ex != null)
                {
                    correoAux = correoAux + "" + "\n\r" +
                        "Nombre de Interfaz: " + nombreInterfaz + "\n\r" +
                        "ID Error (Tabla dbo.ERRORSISTEMAS): " + idError + "\n\r" +
                        "Exception.Message: " + ex.Message + "\n\r" +
                        "Exception.InnerException: " + ex.InnerException + "\n\r" +
                        "Exception.Source: " + ex.Source + "\n\r" +
                        "Exception.StackTrace: " + ex.StackTrace + "\n\r" +
                        "Exception.TargetSite: " + ex.TargetSite + "\n\r" +
                        "Fecha Error: " + DateTime.Today.ToString("dd/MM/yyyy") + "\n\r" +
                        "Hora Error: " + DateTime.Now.ToString("h:mm:ss tt") + "\n\r";
                }
                else
                {
                    correoAux = correoAux + "" + "\n\r" +
                        "Nombre de Interfaz: " + nombreInterfaz + "\n\r" +
                        "ID Error (Tabla dbo.ERRORSISTEMAS): " + idError + "\n\r" +
                        "Fecha Error: " + DateTime.Today.ToString("dd/MM/yyyy") + "\n\r" +
                        "Hora Error: " + DateTime.Now.ToString("h:mm:ss tt") + "\n\r";
                }
                strBuilding.Append(correoAux);
            }
            catch{
                throw;
            }
            return strBuilding.ToString();
        }

        public string FormatearMensajeCulminacionCorrecta_CONSOLA(int secuencia, string nombreInterfaz, string mensaje)
        {

            StringBuilder strBuilding = new StringBuilder();
            string correoAux = "";

            try{
                correoAux = correoAux + "" + "\n\r" +
                        "Secuencia: " + secuencia + "\n\r" +
                        "Nombre Interfaz: [" + nombreInterfaz + "]\n\r" +
                        "Mensaje: " + mensaje + "\n\r" +
                        "Fecha: " + DateTime.Today.ToString("dd/MM/yyyy") + "\n\r" +
                        "Hora: " + DateTime.Now.ToString("h:mm:ss tt") + "\n\r";
                strBuilding.Append(correoAux);
            }
            catch{
                throw;
            }
            return strBuilding.ToString();
        }

        public string ObtenerDestinatariosReporteInterfaz(int idInterfaz)
        {
            try{
                return (new BaseDAL()).ObtenerDestinatariosReporteInterfaz(idInterfaz);
            }
            catch{
                throw;
            }
        }

        public string Resetear_Proceso_Interfaz(int idinterfaz, int idregini)
        {
            try{
                return (new BaseDAL()).Resetear_Proceso_Interfaz(idinterfaz, idregini);
            }
            catch{
                throw;
            }
        }
    }
}
