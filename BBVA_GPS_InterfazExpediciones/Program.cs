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

namespace BBVA_GPS_InterfazExpediciones
{
    class Program
    {
        static void Main(string[] args){

            try
            {
                DefinirVariablesGlobales();
                GenerarInterfazExpediciones();
            }
            catch(Exception ex) {
                InterfazReferenciasBL objBL = new InterfazReferenciasBL();
                /*NOTIFICACIÓN [ERROR] POR EMAIL*/
                objBL.EnviarCorreoElectronico(
                    objBL.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Referencias), "", "[ERROR GENERAL - Main]", "",
                    objBL.FormatearMensajeError_HTML(ex, 0, "ERROR GENERAL"), null);
                /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                Console.WriteLine(objBL.FormatearMensajeError_CONSOLA(ex, 0, "ERROR GENERAL"));
            }
        }

        public static bool GenerarInterfazExpediciones()
        {
            bool result = false;
            int idregini = 0;
            int errorID = 0;
            string drive = "";
            string fileName_Expediciones = "";
            string fileName_Expediciones_suplacorp = "";
            string fileName_Expediciones_fullpath = "";
            InterfazExpediciones_RegIniBE intExpediciones;
            InterfazExpedicionesBL objBL_ERROR = new InterfazExpedicionesBL();
            
            try
            {
                /*
                    1) GENERAR "FICHERO DE EXPEDICIONES PARA EL BBVA" 
                    - VALIDAR QUE SI YA DEJÓ UN FICHERO DE EXPEDICIÓN ANTERIOR Y NO HA SIDO DESCARGADO POR BBVA AUN, ENTONCES, VERSIONAR 1,2,3,4...
                    [OJO], AL REUNIRME CON EL BBVA EXPRESARON QUE NO SE DEBE VERSIONAR EN ABSOLUTO, PERO, DEJARÉ EL VERSIONAMIENTO DEL
                    FICHERO YA QUE DESEO MONITOREAR SU COMPORTAMIENTO Y NO DESEO PERDER NINGÚN FICHERO GENERADO AL SER "SOBREESCRITO",
                    LUEGO DECIDIRÉ SI ELIMINO ESTA FUNCIONALIDAD, POR AHORA NO.
                */
                fileName_Expediciones = GenerarNombreFicheroExpediciones(); /* --> VERSIONAMIENTO TEMPORALMENTE */
                fileName_Expediciones_fullpath = GlobalVariables.Ruta_sftp + fileName_Expediciones + ".txt";
                fileName_Expediciones_suplacorp = fileName_Expediciones + "_" + DateTime.Now.ToString("yyyyMMdd_hmmss").ToString() + ".txt";

                intExpediciones = new InterfazExpediciones_RegIniBE();

                /*2) GENERAR FICHERO DE INTERFAZ DE EXPEDICIONES */
                if ((new InterfazExpedicionesBL()).GenerarInterfazExpediciones(ref idregini, fileName_Expediciones_suplacorp, ref errorID))
                {
                    #region "Expedición generada correctamente"
                    {
                        //intExpediciones = new InterfazExpediciones_RegIniBE();
                        intExpediciones.Idregini = idregini;

                        /*3) OBTENER LA LISTA TOTAL DE LA EXPEDICIÓN PREVIAMENTE GENERADA */
                        drive = Path.GetPathRoot(GlobalVariables.Ruta_sftp);
                        if (Directory.Exists(drive))
                        {
                            if ((new InterfazExpedicionesBL()).ObtenerExpedicionesGeneradas(ref intExpediciones))
                            {
                                if (intExpediciones.LstInterfazExpediciones_RegCabBE.Count > 0)
                                {
                                    if (GenerarFicheroInterfazExpediciones(fileName_Expediciones, fileName_Expediciones_fullpath, intExpediciones))
                                    {
                                        /*4.1) COPIA LOG: D:\Suplacorp\InterfacesImportadas_BBVA_GPS\Expediciones\... */
                                        File.Copy(fileName_Expediciones_fullpath, Utilitarios.ObtenerRutaFicheroDestino(fileName_Expediciones) + fileName_Expediciones_suplacorp);
                                        //4.2) [NOTIFICAR POR CONSOLA]
                                        Console.WriteLine((new InterfazExpedicionesBL()).FormatearMensajeCulminacionCorrecta_CONSOLA(1,
                                            "Int. Expediciones", "Se completó correctamente el proceso de generación de la Int. de Expediciones."));
                                        /*4.3) NOTIFICAR POR EMAIL EL "FICHERO" Y EL "REPORTE HTML" */
                                        //ENVIAR CORREO BIEN DETALLADO AL EJECUTIVO E INTERESADOS SOBRE LA GENERACIÓN DE LA INT. DE EXPEDICIONES
                                        (new InterfazExpedicionesBL()).EnviarCorreoElectronico((new InterfazExpedicionesBL()).ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones),
                                            "", /* Emails con copia */
                                            "[Int. Expediciones] - Reporte de generación de Interfaz de Expediciones.",
                                            (Utilitarios.ObtenerRutaFicheroDestino(fileName_Expediciones) + fileName_Expediciones_suplacorp),
                                            (new InterfazExpedicionesBL()).GenerarReporte_GeneracionInterfazExpediciones(intExpediciones), null);

                                        result = true;
                                    }
                                    else
                                    {
                                        /* OCURRIÓ UN ERROR EN LA GENERACIÓN DEL FICHERO DE ITNERFAZ DE EXPEDICIONES*/
                                        objBL_ERROR.EnviarCorreoElectronico(
                                            objBL_ERROR.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - Int. Expediciones]", "",
                                            (objBL_ERROR.FormatearMensajeError_HTML(null, 0, "[ERROR CRÍTICO GENERACIÓN FICHERO DE EXPEDICIÓN]")), null);
                                        /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                                        Console.WriteLine(objBL_ERROR.FormatearMensajeError_CONSOLA(null, 0, "[ERROR CRÍTICO GENERACIÓN FICHERO DE EXPEDICIÓN]"));
                                        //DESHACER LOS REGISTOS ACTUALIZADOS DE LA INT. DE SUMINISTOS!(NOTIFICACIÓN_COMPLETA; IDGUIAS_NOTIFICADAS) EN PROCESO
                                        /* ELIMINACIÓN DE REGISTRO INICIAL, "RESET DE TODO" EL PROCESO*/
                                        (new InterfazExpedicionesBL()).Resetear_Proceso_Interfaz((int)GlobalVariables.Interfaz.Expediciones, intExpediciones.Idregini);
                                    }
                                }
                            }
                        }
                        else
                        {
                            /* OCURRIÓ UN ERROR EN EL REGISTRO INICIAL */
                            /* OCURRIÓ UN ERROR EN EL SFTP */
                            objBL_ERROR.EnviarCorreoElectronico(
                                objBL_ERROR.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - Int. Expediciones]", "",
                                (objBL_ERROR.FormatearMensajeError_HTML(null, 0, "[ERROR CRÍTICO SFTP] - NO SE TIENE ACCESO AL SFTP")), null);
                            /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                            Console.WriteLine(objBL_ERROR.FormatearMensajeError_CONSOLA(null, 0, "[ERROR CRÍTICO SFTP] - NO SE TIENE ACCESO AL SFTP"));
                            //DESHACER LOS REGISTOS ACTUALIZADOS DE LA INT. DE SUMINISTOS!(NOTIFICACIÓN_COMPLETA; IDGUIAS_NOTIFICADAS) EN PROCESO
                            /* ELIMINACIÓN DE REGISTRO INICIAL, "RESET DE TODO" EL PROCESO*/
                            objBL_ERROR.Resetear_Proceso_Interfaz((int)GlobalVariables.Interfaz.Expediciones, intExpediciones.Idregini);
                        }
                    }
                    #endregion
                }
                else if(errorID != 0)
                {
                    #region "Error"
                    objBL_ERROR.EnviarCorreoElectronico(
                            objBL_ERROR.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ALERTA - Int. Expediciones]", "",
                            (objBL_ERROR.FormatearMensajeError_HTML(null, 0, "[ALERTA] - NO GENERÓ LA INT. DE EXPEDICIONES")), null);
                    /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                    Console.WriteLine(objBL_ERROR.FormatearMensajeError_CONSOLA(null, 0, "[ALERTA] - NO GENERÓ LA INT. DE EXPEDICIONES"));
                    #endregion
                }else if(errorID == 0 && idregini == 0)
                {
                    #region "NUEVO REQUERIMIENTO BBVA --> REPORTAR EXPEDICIÓN VACÍA"
                    //NO HAY NADA QUE REPORTAR EN EL FICHERO DE EXPEDICIONES, PERO AÚN ASÍ, 
                    //DEBEMOS GENERAR LA "INT. DE EXPEDICIONES" [VACÍO], EL GPS ESPERA RECIBIR UN FICHERO DIARIAMENTE SINO GENERARÁ ERRORES PARA EL BABY-VA ;)

                    /*ESCRIBIENDO EL [REGISTRO INICIAL] VACÍO  DE LA INTERFAZ DE EXPEDICIONES */
                    intExpediciones.Numeral = "000000";
                    intExpediciones.Tipo_registro = "0";
                    intExpediciones.Tipo_interfaz = "E";
                    intExpediciones.Pais = "PE";
                    intExpediciones.Identificador_interfaz = "EX";
                    intExpediciones.Nombre_fichero = "PE_OL1_EXPED";
                    intExpediciones.Fecha_ejecucion = DateTime.Today;
                    intExpediciones.Hora_proceso = "00:00:00";

                    /*ESCRIBIENDO EL [REGISTRO CONTROL] VACÍO DE LA INTERFAZ DE EXPEDICIONES */
                    intExpediciones.Numero_total_registros_fin = "000000";
                    intExpediciones.Tipo_registro_fin = "9";
                    intExpediciones.Numero_registros_cab_fin = "00000";
                    intExpediciones.Numero_registros_pos_fin = "00000";
                    intExpediciones.Numero_registros_tipo3_fin = "00000";

                    if (GenerarFicheroInterfazExpediciones(fileName_Expediciones, fileName_Expediciones_fullpath, intExpediciones))
                    {
                        /*4.1) COPIA LOG: D:\Suplacorp\InterfacesImportadas_BBVA_GPS\Expediciones\... */
                        File.Copy(fileName_Expediciones_fullpath, Utilitarios.ObtenerRutaFicheroDestino(fileName_Expediciones) + fileName_Expediciones_suplacorp);
                        //4.2) [NOTIFICAR POR CONSOLA]
                        Console.WriteLine((new InterfazExpedicionesBL()).FormatearMensajeCulminacionCorrecta_CONSOLA(1,
                            "Int. Expediciones [VACÍA]", "Se completó correctamente el proceso de generación de la Int. de Expediciones."));
                        /*4.3) NOTIFICAR POR EMAIL EL "FICHERO" Y EL "REPORTE HTML" */
                        //ENVIAR CORREO BIEN DETALLADO AL EJECUTIVO E INTERESADOS SOBRE LA GENERACIÓN DE LA INT. DE EXPEDICIONES
                        (new InterfazExpedicionesBL()).EnviarCorreoElectronico((new InterfazExpedicionesBL()).ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones),
                            "", /* Emails con copia */
                            "[Int. Expediciones] [VACÍA]- Reporte de generación de Interfaz de Expediciones.",
                            (Utilitarios.ObtenerRutaFicheroDestino(fileName_Expediciones) + fileName_Expediciones_suplacorp),
                            (new InterfazExpedicionesBL()).GenerarReporte_GeneracionInterfazExpediciones(intExpediciones), null);
                    }
                    else
                    {
                        /* OCURRIÓ UN ERROR EN LA GENERACIÓN DEL FICHERO DE ITNERFAZ DE EXPEDICIONES*/
                        objBL_ERROR.EnviarCorreoElectronico(
                            objBL_ERROR.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - Int. Expediciones] [VACÍA]", "",
                            (objBL_ERROR.FormatearMensajeError_HTML(null, 0, "[ERROR CRÍTICO GENERACIÓN FICHERO DE EXPEDICIÓN - VACÍO]")), null);
                        /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                        Console.WriteLine(objBL_ERROR.FormatearMensajeError_CONSOLA(null, 0, "[ERROR CRÍTICO GENERACIÓN FICHERO DE EXPEDICIÓN - VACÍO]"));
                        //DESHACER LOS REGISTOS ACTUALIZADOS DE LA INT. DE SUMINISTOS!(NOTIFICACIÓN_COMPLETA; IDGUIAS_NOTIFICADAS) EN PROCESO
                        /* ELIMINACIÓN DE REGISTRO INICIAL, "RESET DE TODO" EL PROCESO*/
                        (new InterfazExpedicionesBL()).Resetear_Proceso_Interfaz((int)GlobalVariables.Interfaz.Expediciones, intExpediciones.Idregini);
                    }
                    #endregion
                }
            }
            catch {
                throw;
            }

            return result;
        }

        private static string GenerarNombreFicheroExpediciones() {

            string[] files = System.IO.Directory.GetFiles(GlobalVariables.Ruta_sftp, "*.txt");
            string currentFileName = "";
            string fileName = "PE_OL1_EXPED";
            int qFicherosExpedicionesPrevios = 0;
            ArrayList arrayList = new ArrayList();

            try
            {
                for (int i = 0; i < files.Length; i++)
                {
                    currentFileName = files[i].ToString().Split('\\')[1].Split('.')[0];
                    if (currentFileName.Contains("PE_OL1_EXPED"))
                    {
                        if (currentFileName.Length == 12){
                            arrayList.Add(0);
                        }
                        else{
                            arrayList.Add(currentFileName.Substring(12, 2));
                        }
                    }
                }

                arrayList.Reverse();
                if (arrayList.Count > 0){
                    qFicherosExpedicionesPrevios = arrayList.Count;
                    fileName = "PE_OL1_EXPED" + ((qFicherosExpedicionesPrevios + 1) > 9 ? (Convert.ToInt32(arrayList[0]) + 1).ToString() : "0" + (Convert.ToInt32(arrayList[0]) + 1).ToString());
                }
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }

            return fileName;
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
                GlobalVariables.IdCliente = Convert.ToInt32(lstVariables["idcliente"]);
            }
            catch
            {
                throw;
            }
        }

        private static bool GenerarFicheroInterfazExpediciones(string fileName_Expediciones, string fileName_Expediciones_fullpath, InterfazExpediciones_RegIniBE intExpediciones) {
            bool result = false;
            StringBuilder strBuilder = new StringBuilder();
            var delimiter = "\t";

            try
            {

                using (FileStream fs = new FileStream(fileName_Expediciones_fullpath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    /*ESCRIBIENDO EL [REGISTRO INICIAL] DE LA INTERFAZ DE EXPEDICIONES */
                    strBuilder.Append(Utilitarios.CompletarConCeros_Izquierda(intExpediciones.Numeral, 6) + delimiter);
                    strBuilder.Append(intExpediciones.Tipo_registro + delimiter);
                    strBuilder.Append(intExpediciones.Tipo_interfaz + delimiter);
                    strBuilder.Append(intExpediciones.Pais + delimiter);
                    strBuilder.Append(intExpediciones.Identificador_interfaz + delimiter);
                    strBuilder.Append(intExpediciones.Nombre_fichero + delimiter);
                    strBuilder.Append(intExpediciones.Fecha_ejecucion.ToString("ddMMyyyy") + delimiter);
                    strBuilder.Append(intExpediciones.Hora_proceso);
                    sw.WriteLine(strBuilder.ToString());
                    strBuilder.Clear();

                    /*ESCRIBIENDO CADA [REGISTRO CABECERA] DE LA INTERFAZ DE EXPEDICIONES */
                    foreach (var cab in intExpediciones.LstInterfazExpediciones_RegCabBE)
                    {
                        strBuilder.Append(Utilitarios.CompletarConCeros_Izquierda(cab.Numeral, 6) + delimiter);
                        strBuilder.Append(cab.Tipo_registro + delimiter);
                        strBuilder.Append(cab.Tipo_movimiento + delimiter);
                        strBuilder.Append(cab.Tipo_expedicion + delimiter);
                        strBuilder.Append(Utilitarios.CompletarConCeros_Izquierda(cab.Nro_doc_compras_reserva,10) + delimiter);
                        strBuilder.Append(cab.Fecha_contabilizacion.ToString("ddMMyyyy") + delimiter);
                        strBuilder.Append(cab.Numero_cesta.Trim() + delimiter);
                        strBuilder.Append(cab.Texto_cabecera_documento.Trim() + delimiter);
                        strBuilder.Append(delimiter);
                        sw.WriteLine(strBuilder.ToString());
                        strBuilder.Clear();

                        /*ESCRIBIENDO CADA [POSICIÓN DE LA CABECERA ACTUAL] DE LA INTERFAZ DE EXPEDICIONES */
                        foreach (var pos in cab.LstInterfazExpediciones_RegPosBE)
                        {
                            strBuilder.Append(Utilitarios.CompletarConCeros_Izquierda(pos.Numeral, 6) + delimiter);
                            strBuilder.Append(pos.Tipo_registro + delimiter);
                            strBuilder.Append(Utilitarios.CompletarConCeros_Izquierda(pos.Nro_posic_ped_reserva,5) + delimiter);
                            strBuilder.Append(Utilitarios.CompletarConCeros_Izquierda(pos.Nro_material,18) + delimiter);
                            strBuilder.Append(pos.Unidad_medida_pedido + delimiter);
                            strBuilder.Append(Utilitarios.CompletarConCeros_Izquierda(Utilitarios.DecimalFormato_SUPLA_BBVA(pos.Cantidad.ToString()),13) + delimiter);
                            strBuilder.Append(pos.Indicador_entrega_final + delimiter);
                            strBuilder.Append(pos.Bulto + delimiter);
                            strBuilder.Append(pos.Numero_lote + delimiter);
                            strBuilder.Append(delimiter);
                            sw.WriteLine(strBuilder.ToString());
                            strBuilder.Clear();
                        }
                    }

                    /*ESCRIBIENDO REGISTRO FINAL DE LA INTERFAZ DE EXPEDICIONES */
                    strBuilder.Append(Utilitarios.CompletarConCeros_Izquierda(intExpediciones.Numero_total_registros_fin, 6) + delimiter);
                    strBuilder.Append(intExpediciones.Tipo_registro_fin + delimiter);
                    strBuilder.Append(Utilitarios.CompletarConCeros_Izquierda(intExpediciones.Numero_registros_cab_fin,5) + delimiter);
                    strBuilder.Append(Utilitarios.CompletarConCeros_Izquierda(intExpediciones.Numero_registros_pos_fin,5) + delimiter);
                    strBuilder.Append(intExpediciones.Numero_registros_tipo3_fin);
                    sw.Write(strBuilder.ToString());
                    strBuilder.Clear();

                    result = true;

                    //MUY IMPORTANTE CONGELAR EL PROCESO, PARA DARLE TIEMPO A LIBERAR EL RECURSO (FICHERO) 
                    System.Threading.Thread.Sleep(2000);
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            
            return result;
        }

    }
}
