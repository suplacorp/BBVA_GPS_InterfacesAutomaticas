﻿using System;
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

namespace BBVA_GPS_InterfacesAutomaticas
{
    class Program
    {
        //enum Interfaces { PE_OL1_REFER, PE_OL1_SUMIN, PE_OL1_EXPED, PE_OL1_PREFAC };
        //public static Dictionary<int, string> ListaDepartamentosBBVA = new Dictionary<int, string>();

        static void Main(string[] args)
        {

            try
            {
                /*DEFINIENDO VARIABLES GLOBALES*/
                DefinirVariablesGlobales();

                /* Activando el FileWatcher para detectar actividad en el SFTP */
                ActivarFileWatcher_SuplaSFTP();
            }
            catch(Exception ex) {
                InterfazReferenciasBL objBL = new InterfazReferenciasBL();
                /*NOTIFICACIÓN [ERROR] POR EMAIL*/
                objBL.EnviarCorreoElectronico(
                    objBL.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Referencias), "", "[ERROR GENERAL - Main]", "",
                    objBL.FormatearMensajeError_HTML(ex, 0, "ERROR GENERAL"));
                /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                Console.WriteLine(objBL.FormatearMensajeError_CONSOLA(ex, 0, "ERROR GENERAL"));
            }    
        }

        #region FileWatcher Listener

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void ActivarFileWatcher_SuplaSFTP()
        {
            string[] args = new string[10];
            args[1] = GlobalVariables.Ruta_sftp;

            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = args[1];

            /*  Watch for changes in LastAccess and LastWrite times, and
                the renaming of files or directories. 
           */
           // watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "*.txt";

            // Add event handlers.
            watcher.Created += new FileSystemEventHandler(OnCreated);
            //watcher.Changed += new FileSystemEventHandler(OnChanged);  //No lo tomo en consideración ya que se dispara TAMBIÉN después de CREAR
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);  //Auditoría para saber en qué momento eliminaron un fichero
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);     //No nos interesa hacer nada si renombran el fichero

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            Console.WriteLine("Presione la laetra \'q\' para terminar el programa \n[MUCHO CUIDADO! ESTE PROGRAMA DEBE PERMANECER ACTIVO SIEMPRE]");
            while (Console.Read() != 'q') ;
        }
        // Define the event handlers.
        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string nombreFicheroBBVA = "";
            string rutaFicheroBBVA = "";
            string nombreFicheroSuplacorp = "";
            
            // Especificar que se hará cuando el fichero es cambiado, creado, eliminado
            try {
                try
                {
                    Console.WriteLine("==========================================================");
                    Console.WriteLine("Fichero " + e.FullPath + " detectado, comienza la copia: " + DateTime.Now.ToString());
                    nombreFicheroBBVA = e.Name;
                    rutaFicheroBBVA = e.FullPath;
                    nombreFicheroSuplacorp = Utilitarios.QuitarExtensionNombreFichero_BBVA(nombreFicheroBBVA) + "_" + DateTime.Now.ToString("yyyyMMdd_hmmss").ToString() + ".txt";
                    
                    //MUY IMPORTANTE CONGELAR EL PROCESO, PARA DARLE TIEMPO A LIBERAR EL RECURSO (FICHERO) Y QUE NO HAYA UNA EXCEPCIÓN POR RECURSO AÚN EN USO
                    System.Threading.Thread.Sleep(1000);
                    
                    File.Move(rutaFicheroBBVA, Utilitarios.ObtenerRutaFicheroDestino(nombreFicheroSuplacorp) + nombreFicheroSuplacorp);
                    Console.WriteLine("==========================================================");
                    Console.WriteLine("");

                    //En este punto el fichero terminó de ser copiado (uploaded) por el BBVA, y ya puede ser procesado por Suplacorp
                    EventoDetectado_Crearon(nombreFicheroSuplacorp, (Utilitarios.ObtenerRutaFicheroDestino(nombreFicheroSuplacorp) + nombreFicheroSuplacorp));
                }
                catch (IOException ioException)
                {
                    InterfazReferenciasBL objBL = new InterfazReferenciasBL();
                    /*NOTIFICACIÓN [ERROR] POR EMAIL*/
                    objBL.EnviarCorreoElectronico(
                        objBL.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Referencias), "", "[ERROR GENERAL - Main]", "",
                        objBL.FormatearMensajeError_HTML(ioException, 0, "ERROR GENERAL"));
                    /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                    Console.WriteLine(objBL.FormatearMensajeError_CONSOLA(ioException, 0, "ERROR GENERAL"));
                }
            }
            catch (NullReferenceException ex) {
                InterfazReferenciasBL objBL = new InterfazReferenciasBL();
                /*NOTIFICACIÓN [ERROR] POR EMAIL*/
                objBL.EnviarCorreoElectronico(
                    objBL.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Referencias), "", "[ERROR GENERAL - Main]", "",
                    objBL.FormatearMensajeError_HTML(ex, 0, "ERROR GENERAL"));
                /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                Console.WriteLine(objBL.FormatearMensajeError_CONSOLA(ex, 0, "ERROR GENERAL"));
            }
        }
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            // Console.WriteLine("File: {0} changed" + e.FullPath + " " + e.ChangeType);
            //EventoDetectado(e.Name, e.FullPath);
            //EventoDetectado(e);
        }
        private static void OnDeleted(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            // Console.WriteLine("File: {0} deteded to {1}", e.OldFullPath, e.FullPath);
            //EventoDetectado(e.Name, e.FullPath);
            //EventoDetectado(e);
            //Llamar a otro Evento, no usar el mismo que uso para cuando suben (crean) un archivo o lo reemplazan
            
        }
        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            //Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
            //EventoDetectado(e.Name, e.FullPath);
            //EventoDetectado(e);
        }

        #endregion
 
        private static void EventoDetectado_Crearon(string nombreFicheroSuplacorp, string Ruta_fichero_detino_Ref)
        {
            List<ValidacionInterfazBE> _lstValidacion;
            string nombreFicheroBBVA;
            try
            {
                if (nombreFicheroSuplacorp.Length > 0 & nombreFicheroSuplacorp.Contains(".")){

                    nombreFicheroBBVA = Utilitarios.ObtenerNombreFicheroNeto(nombreFicheroSuplacorp);
                    _lstValidacion = new List<ValidacionInterfazBE>();
                    _lstValidacion = (new ValidacionInterfazBL()).ListarValidaciones_xInterfaz(nombreFicheroBBVA);

                    switch (nombreFicheroBBVA)
                    {
                        /* #################################################################################### */
                        case "PE_OL1_REFER": /*INTERFAZ REFERENCIAS*/
                            #region INTERFAZ DE REFERENCIA
                            InterfazReferencias_RegIniBE interfazReferencias_RegIniBE = new InterfazReferencias_RegIniBE();
                            InterfazReferenciasBL interfazRefBL = new InterfazReferenciasBL();

                            //LEER FICHERO DEL BBVA
                            interfazReferencias_RegIniBE = interfazRefBL.LeerFicheroInterfaz(nombreFicheroBBVA, Ruta_fichero_detino_Ref, _lstValidacion);
                            interfazReferencias_RegIniBE.Nombre_fichero_detino = nombreFicheroSuplacorp;
                            if (interfazRefBL.RegistrarInterfaz_RegIni(ref interfazReferencias_RegIniBE))
                            {
                                //ACTUALIZAR EL MAESTRO "CLIENTE_ARTICULO"
                                if (interfazRefBL.ActualizarClienteArticulo_IntRef(ref interfazReferencias_RegIniBE))
                                {
                                    //[NOTIFICAR POR CONSOLA]
                                    Console.WriteLine(interfazRefBL.FormatearMensajeCulminacionCorrecta_CONSOLA(1,
                                        "Int. Referencias", "Se completó correctamente el proceso de importación y actualización."));
                                }
                            }
                            #endregion
                            break;
                        /* #################################################################################### */
                        case "PE_OL1_SUMIN": /*INTERFAZ SUMINISTROS*/
                            #region INTERFAZ DE SUMINISTROS
                            InterfazSuministros_RegIniBE interfazSum_RegIniBE = new InterfazSuministros_RegIniBE();
                            InterfazSuministrosBL interfazSumBL = new InterfazSuministrosBL();

                            //LEER FICHERO DEL BBVA
                            interfazSum_RegIniBE = interfazSumBL.LeerFicheroInterfaz(nombreFicheroSuplacorp, Ruta_fichero_detino_Ref, _lstValidacion);
                            interfazSum_RegIniBE.Nombre_fichero_detino = nombreFicheroSuplacorp;

                            if (interfazSumBL.RegistrarInterfaz_RegIni(ref interfazSum_RegIniBE))
                            {
                                //[NOTIFICAR POR CONSOLA]
                                Console.WriteLine(interfazSumBL.FormatearMensajeCulminacionCorrecta_CONSOLA(1, 
                                    "Int. Prefactura", "Se completó correctamente el proceso de importación de pedidos y fueron generados correctamente."));
                            }
                            #endregion
                            break;
                        /* #################################################################################### */
                        case "PE_OL1_EXPED": /*INTERFAZ EXPEDICIONES*/
                            #region INTERFAZ DE EXPEDICIONES [NO SE PROCESA AQUÍ]
                            /*ESTA INTERFAZ NO SE TRABAJARÁ AQUÍ, SINO EN EL OTRO APLICATIVO DENTRO DE ESTA SOLUCIÓN: "BBVA_GPS_INTERFAZEXPEDICIONES" */
                            #endregion
                            break;
                        /* #################################################################################### */
                        case "PE_OL1_PREFAC": /*INTERFAZ PREFACTURAS */
                            #region INTERFAZ DE PREFACTURA
                            InterfazPrefacturas_RegIniBE interfazPreFact_RegIniBE = new InterfazPrefacturas_RegIniBE();
                            InterfazPrefacturaBL interfazPreFactBL = new InterfazPrefacturaBL();

                            //LEER FICHERO DEL BBVA
                            interfazPreFact_RegIniBE = interfazPreFactBL.LeerFicheroInterfaz(nombreFicheroSuplacorp, Ruta_fichero_detino_Ref, _lstValidacion);
                            interfazPreFact_RegIniBE.Nombre_fichero_detino = nombreFicheroSuplacorp;

                            //REGISTRAR EN BD LA ENTIDAD
                            if (interfazPreFactBL.RegistrarInterfaz_RegIni(ref interfazPreFact_RegIniBE))
                            {
                                //[NOTIFICAR POR CONSOLA]
                                Console.WriteLine((new InterfazPrefacturaBL()).FormatearMensajeCulminacionCorrecta_CONSOLA(1, 
                                    "Int. Prefactura", "Se completó correctamente el proceso de importación."));

                                //[NOTIFICAR] EJECUTIVO E INVOLUCRADOS (ENVIAR CORREO HTML Y ARCHIVO ADJUNTO)
                                if (interfazPreFact_RegIniBE.LstInterfazPrefacturas_RegCabBE.Count > 0)
                                {
                                    if ((new InterfazPrefacturaBL()).NotificarInterfazPreFactura(interfazPreFact_RegIniBE))
                                    {
                                        Console.WriteLine((new InterfazPrefacturaBL()).FormatearMensajeCulminacionCorrecta_CONSOLA(2, 
                                            "Int. Prefactura", "Se envió correctamente el e-amil de notificación del proceso de importación."));
                                    }
                                }
                            }
                            #endregion
                            break;
                        /* #################################################################################### */
                        case "PE_OL1_EXPED_LOG_EXTERNO": /*Interfaz Prefacturas*/
                            #region INTERFAZ DE LOG EXTERNO
                            LogExternoExpedicionesBL logExtExpBL = new LogExternoExpedicionesBL();
                            string lectura = logExtExpBL.LeerFicheroInterfazLogExterno(nombreFicheroSuplacorp, Ruta_fichero_detino_Ref);

                            if (!(lectura.Trim().Length == 44 && lectura.Trim().Contains("Fichero procesado correctamenteXX")))
                            {
                                logExtExpBL.EnviarCorreoElectronico((new InterfazExpedicionesBL()).ObtenerDestinatariosReporteInterfaz(3),
                                        "", /* Emails con copia */
                                        "Reporte log externo [HAY ERRORES] - Int. Expediciones",
                                        (Utilitarios.ObtenerRutaFicheroDestino(nombreFicheroBBVA) + nombreFicheroSuplacorp),
                                        lectura);
                            }
                            #endregion
                            break;
                            /* #################################################################################### */
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                InterfazReferenciasBL objBL = new InterfazReferenciasBL();
                /*NOTIFICACIÓN [ERROR] POR EMAIL*/
                objBL.EnviarCorreoElectronico(
                    objBL.ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Referencias), "", "[ERROR GENERAL - EventoDetectado_Crearon]", "",
                    objBL.FormatearMensajeError_HTML(ex, 0, "ERROR GENERAL"));
                /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                Console.WriteLine(objBL.FormatearMensajeError_CONSOLA(ex, 0, "ERROR GENERAL"));
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
            catch {
                throw;
            }
        }
    }



    



}
