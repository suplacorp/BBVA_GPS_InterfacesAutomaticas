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

namespace BBVA_GPS_InterfacesAutomaticas
{
    class Program
    {
        //enum Interfaces { PE_OL1_REFER, PE_OL1_SUMIN, PE_OL1_EXPED, PE_OL1_PREFAC };


        static void Main(string[] args)
        {
            /*DEFINIENDO VARIABLES GLOBALES*/
            DefinirVariablesGlobales();

            /* Activando el FileWatcher para detectar actividad en el SFTP */
            //ActivarFileWatcher_SuplaSFTP();


            /* PRUEBAS BBVA PROVISIONALES - BORRAR LUEGO*/
            /*
            String[] valores_linea_actual;
            string cabecera_suministro = "000002	2	0100084029  	P	8500057740	00010	000000000210000239	25112015	MX11003313	2,000         	PAQ	";
            valores_linea_actual = cabecera_suministro.Split('\t');
            */

            List<InterfazSuministros_PedidoBE> _lstObjectos = new List<InterfazSuministros_PedidoBE>();
            _lstObjectos = (new InterfazSuministrosBL()).Kike();

            (new InterfazSuministrosBL()).GenerarReporte_GeneracionPedidos(_lstObjectos);

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
                    Console.WriteLine(ioException.Message);
                }
            }
            catch (NullReferenceException ex) {
                Console.WriteLine(ex.Message);
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
                        case "PE_OL1_REFER": /*Interfaz Referencias*/
                            InterfazReferencias_RegIniBE interfazReferencias_RegIniBE = new InterfazReferencias_RegIniBE();
                            InterfazReferenciasBL interfazRefBL = new InterfazReferenciasBL();

                            //Leer Fichero del BBVA
                            interfazReferencias_RegIniBE = interfazRefBL.LeerFicheroInterfaz(nombreFicheroBBVA, Ruta_fichero_detino_Ref, _lstValidacion);
                            interfazReferencias_RegIniBE.Nombre_fichero_detino = nombreFicheroSuplacorp;
                            if (interfazRefBL.RegistrarInterfaz_RegIni(ref interfazReferencias_RegIniBE)){
                                //Actualizar el maestro "Cliente_Articulo"
                                if (interfazRefBL.ActualizarClienteArticulo_IntRef(ref interfazReferencias_RegIniBE)){
                                    /*Notificar por EMAIL los artículos que se actualizaron, resaltando los que no pudieron ser actualizados (procesado = 0) */
                                }
                                else {  
                                    //Notificar por correo el problema
                                }
                            }
                            else {
                                //Notificar por correo el error con el código de error generado y más detalles sobre la interfaz
                            }
                            break;
                        case "PE_OL1_SUMIN": /*Interfaz Suministros*/

                            InterfazSuministros_RegIniBE interfazSum_RegIniBE = new InterfazSuministros_RegIniBE();
                            InterfazSuministrosBL interfazSumBL = new InterfazSuministrosBL();

                            //Leer Fichero del BBVA
                            interfazSum_RegIniBE = interfazSumBL.LeerFicheroInterfaz(nombreFicheroSuplacorp, Ruta_fichero_detino_Ref, _lstValidacion);
                            interfazSum_RegIniBE.Nombre_fichero_detino = nombreFicheroSuplacorp;
                            if (interfazSumBL.RegistrarInterfaz_RegIni(ref interfazSum_RegIniBE))
                            {

                            }
                            else {
                                //Notificar por correo el error con el código de error generado y más detalles sobre la interfaz
                            }

                            break;
                        case "PE_OL1_EXPED": /*Interfaz Expediciones*/
                            Console.WriteLine("Expediciones: " + _lstValidacion.Count.ToString());
                            break;
                        case "PE_OL1_PREFAC": /*Interfaz Prefacturas*/
                            Console.WriteLine("Prefacturas: " + _lstValidacion.Count.ToString());
                            break;
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                //throw ex;
                Console.WriteLine(ex.Message);
            }
         
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


    }



    



}
