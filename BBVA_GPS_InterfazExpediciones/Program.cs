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

namespace BBVA_GPS_InterfazExpediciones
{
    class Program
    {
        static void Main(string[] args){

            DefinirVariablesGlobales();

            if (GenerarInterfazExpediciones()){
                /*Notificar por email que la generación de la interfaz de expediciones fue correcta */
            }
        }

        public static bool GenerarInterfazExpediciones()
        {
            bool result = false;
            int idregini = 0;
            string drive = "";
            string fileName_Expediciones = "";
            string fileName_Expediciones_suplacorp = "";
            string fileName_Expediciones_fullpath = "";
            InterfazExpediciones_RegIniBE intExpediciones;

            /*1) GENERAR INTERFAZ DE EXPEDICIONES */
            //if (1 == 1) /*BORRAR, SOLO PARA PRUEBAS*/
            if ((new InterfazExpedicionesBL()).GenerarInterfazExpediciones(ref idregini))
            { /*DESCOMENTAR!*/
                {
                    intExpediciones = new InterfazExpediciones_RegIniBE();
                    intExpediciones.Idregini = idregini; /*DESCOMENTAR!*/
                                                         //intExpediciones.Idregini = 322; /*BORRAR, SOLO PARA PRUEBAS*/

                    /*2) OBTENER LA LISTA TOTAL DE LA EXPEDICIÓN PREVIAMENTE GENERADA */
                    drive = Path.GetPathRoot(GlobalVariables.Ruta_sftp);
                    if (Directory.Exists(drive))
                    {
                        if ((new InterfazExpedicionesBL()).ObtenerExpedicionesGeneradas(ref intExpediciones))
                        {
                            if (intExpediciones.LstInterfazExpediciones_RegCabBE.Count > 0)
                            {
                                /*
                                 3) GENERAR "FICHERO DE EXPEDICIONES PARA EL BBVA" 
                                 - VALIDAR QUE SI YA DEJÓ UN FICHERO DE EXPEDICIÓN ANTERIOR Y NO HA SIDO DESCARGADO POR BBVA, ENTONCES, VERSIONAR 1,2,3,4...
                                 - DETECTAR LOS LOGS (EN EL OTRO APLICATIVO) Y TOMAR ACCIÓN (EVIAR POR CORREO).
                                */
                                fileName_Expediciones = GenerarNombreFicheroExpediciones();
                                fileName_Expediciones_fullpath = GlobalVariables.Ruta_sftp + fileName_Expediciones + ".txt";
                                fileName_Expediciones_suplacorp = fileName_Expediciones + "_" + DateTime.Now.ToString("yyyyMMdd_hmmss").ToString() + ".txt";

                                if (GenerarFicheroInterfazExpediciones(fileName_Expediciones, fileName_Expediciones_fullpath, intExpediciones))
                                {
                                    File.Copy(fileName_Expediciones_fullpath, Utilitarios.ObtenerRutaFicheroDestino(fileName_Expediciones) + fileName_Expediciones_suplacorp);

                                    /*4) NOTIFICAR POR EMAIL EL "FICHERO" Y EL "REPORTE HTML" */
                                    //ENVIAR CORREO BIEN DETALLADO AL EJECUTIVO E INTERESADOS SOBRE LA GENERACIÓN DE LA INT. DE EXPEDICIONES
                                    (new InterfazExpedicionesBL()).EnviarCorreoElectronico((new InterfazExpedicionesBL()).ObtenerDestinatariosReporteInterfaz(3),
                                        "", /* Emails con copia */
                                        "Reporte de generación de Interfaz de Expediciones",
                                        (Utilitarios.ObtenerRutaFicheroDestino(fileName_Expediciones) + fileName_Expediciones_suplacorp),
                                        (new InterfazExpedicionesBL()).GenerarReporte_GeneracionInterfazExpediciones(intExpediciones));

                                    result = true;
                                }
                                else
                                {
                                    /*
                                     - Notificar por email que hubo un error en la generación del fichero de expediciones
                                     - Deshacer toda la generación de la interfaz de expediciones
                                     */
                                }
                            }
                        }
                    }
                    else
                    {
                        /*
                         - Notificar por email que hay un error en acceso a la ruta SFTP
                         - Deshacer toda la generación de la interfaz de expediciones     
                        */
                    }

                    /* 5) PROBAR TODO EL FLUJO COMPLETO!  */
                }
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
            GlobalVariables.Ruta_sftp = System.Configuration.ConfigurationSettings.AppSettings["ruta_sftp"].ToString();
            GlobalVariables.Ruta_fichero_detino_Ref = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Ref"].ToString();
            GlobalVariables.Ruta_fichero_detino_Exp = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Exp"].ToString();
            GlobalVariables.Ruta_fichero_detino_Pref = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Pref"].ToString();
            GlobalVariables.Ruta_fichero_detino_Sum = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Sum"].ToString();

            //CARGANDO VARIABLES DE BD
            Dictionary<string, object> lstVariables = new Dictionary<string, object>();
            lstVariables = (new ValidacionInterfazBL()).CargarVariablesIniciales();

            GlobalVariables.IdCliente = Convert.ToInt32(lstVariables["IDCLIENTE"]);
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
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            
            return result;
        }
    }
}
