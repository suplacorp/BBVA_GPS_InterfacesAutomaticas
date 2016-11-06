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
            ActivarFileWatcher_SuplaSFTP();

         
        }

      

        

        private static  void DefinirVariablesGlobales() {
            GlobalVariables.Ruta_sftp = System.Configuration.ConfigurationSettings.AppSettings["ruta_sftp"].ToString();
            GlobalVariables.Ruta_fichero_detino_Ref = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Ref"].ToString();
            GlobalVariables.Ruta_fichero_detino_Exp = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Exp"].ToString();
            GlobalVariables.Ruta_fichero_detino_Pref = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Pref"].ToString();
            GlobalVariables.Ruta_fichero_detino_Sum = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Sum"].ToString();
        }

        #region FileWatcher Listener
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void ActivarFileWatcher_SuplaSFTP()
        {
            //string[] args = System.Environment.GetCommandLineArgs();
            string[] args = new string[10];
            args[1] = GlobalVariables.Ruta_sftp;

            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = args[1];

            /*  Watch for changes in LastAccess and LastWrite times, and
                the renaming of files or directories. 
           */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
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
        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            // Console.WriteLine("File: {0} created" + e.FullPath + " " + e.ChangeType);
            //EventoDetectado(e.Name, e.FullPath);
            try {
                EventoDetectado_Crearon(e);
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

        private static void EventoDetectado_Crearon(FileSystemEventArgs e)
        {
            List<ValidacionInterfazBE> _lstValidacion;
            string nombre_fichero = "";
            string ruta_fichero = "";

            try{
                if (e.Name.Length > 0 & e.Name.Contains(".") && e.ChangeType.ToString() == "Created"){
                    nombre_fichero = e.Name.Split('.')[0];
                    ruta_fichero = e.FullPath.ToString();

                    _lstValidacion = new List<ValidacionInterfazBE>();
                    _lstValidacion = (new ValidacionInterfazBL()).ListarValidaciones_xInterfaz(nombre_fichero);

                    InterfazReferenciasBL _objBL;
                    Thread t;
                    switch (nombre_fichero){
                        case "PE_OL1_REFER": /*Interfaz Referencias*/

                            /*
                            Console.WriteLine("#######################################################");
                            Console.WriteLine("Referencia: " + nombre_fichero);
                            Console.WriteLine("Referencia: " + ruta_fichero);
                            Console.WriteLine("Referencia: " + _lstValidacion.Count);
                            Console.WriteLine("#######################################################");
                            */

                            _objBL = new InterfazReferenciasBL();
                            _objBL.LeerFicheroInterfaz(nombre_fichero, ruta_fichero, _lstValidacion);

                            //(new InterfazReferenciasBL()).LeerFicheroInterfaz(nombre_fichero, ruta_fichero, _lstValidacion);

                            break;

                        case "PE_OL1_SUMIN": /*Interfaz Suministros*/
                            Console.WriteLine("Suministros: " + _lstValidacion.Count.ToString());
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
                Console.WriteLine(ex.Message);
            }
         
        }

        #endregion
    }



    



}
