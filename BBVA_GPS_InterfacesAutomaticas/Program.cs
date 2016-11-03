using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using Suplacorp.GPS.BE;
using Suplacorp.GPS.BL;
using System.Configuration;

namespace BBVA_GPS_InterfacesAutomaticas
{
    class Program
    {
        //enum Interfaces { PE_OL1_REFER, PE_OL1_SUMIN, PE_OL1_EXPED, PE_OL1_PREFAC };
        

        static void Main(string[] args)
        {
            //TESTING1 KIKE
            /*
            string registro_inicial = "000000	0	S	MX	MM	MX_OL1_REFER	01102015	172629													";
            string registro_abajo = "000001	1	MX11	000000000210000222	BLOCK DE 100 HOJAS	UN	PAQ	1	100			Z002		0,000		0,000		10100023	MX11		";
            string registro_ultimo = "000205	9	00205	00000	00000																";
            String[] names = registro_inicial.Split('\t');
            String[] names = registro_ultimo.Split('\t');
            */

            //TESTING2
            //List<ValidacionInterfazBE> _lstValidacion = new List<ValidacionInterfazBE>();
            //_lstValidacion = (new ValidacionInterfazBL()).ListarValidaciones_xInterfaz("PE_OL1_REFER");


            /* Activando el FileWatcher para detectar actividad en el SFTP */
            ActivarFileWatcher_SuplaSFTP();


        }








        #region FileWatcher Listener

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void ActivarFileWatcher_SuplaSFTP()
        {
            //string[] args = System.Environment.GetCommandLineArgs();
            string[] args = new string[10];
            string ruta_sftp = System.Configuration.ConfigurationSettings.AppSettings["ruta_sftp"];
            args[1] = ruta_sftp;

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
            EventoDetectado_Crearon(e);
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

                    switch (nombre_fichero){
                        case "PE_OL1_REFER": /*Interfaz Referencias*/
                            Console.WriteLine("Referencias: " + _lstValidacion.Count.ToString());
                            (new InterfazReferenciasBL()).LeerFicheroInterfaz(nombre_fichero, ruta_fichero, _lstValidacion);

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
            catch (Exception ex)
            {
                throw;
            }
            finally {
                _lstValidacion = null;
            }
        }

        #endregion


    }
}
