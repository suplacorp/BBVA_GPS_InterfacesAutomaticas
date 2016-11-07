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


        static void Main(string[] args)
        {
            /*DEFINIENDO VARIABLES GLOBALES*/
            DefinirVariablesGlobales();

            /* Activando el FileWatcher para detectar actividad en el SFTP */
            ActivarFileWatcher_SuplaSFTP();
            //Console.WriteLine(DateTime.Now.ToString("yyyyMMdd_hmmss"));
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
            string filename = "";
            string fullpath = "";
            // Specify what is done when a file is changed, created, or deleted.
            try {
                //EventoDetectado_Crearon(e); //ORIGINAL TEMPORALMENTE COMENTADO

                Console.WriteLine("File " + e.FullPath + " started copying : " + DateTime.Now.ToString());
                try
                {
                    Console.WriteLine("File is copied : " + DateTime.Now.ToString());
                    filename = e.Name;
                    fullpath = e.FullPath;

                    string newFileName = filename + "_" + DateTime.Now.ToString("yyyyMMdd_hmmss").ToString();

                    //D:\Suplacorp\InterfacesImportadas_BBVA_GPS\Temporal
                    System.Threading.Thread.Sleep(3000);
                    File.Move(fullpath, @"D:\Suplacorp\InterfacesImportadas_BBVA_GPS\Temporal\"+ newFileName);

                    //there is the point when the file is completed copying .... now you should be able to access the file and process it.
                    //EventoDetectado_Crearon(filename, fullpath);
                    EventoDetectado_Crearon(newFileName, @"D:\Suplacorp\InterfacesImportadas_BBVA_GPS\Temporal\" + newFileName);

                }
                catch (IOException ioException)
                {
                    //Console.WriteLine(ioException.Message);
                    throw ioException;
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






        //private static void EventoDetectado_Crearon(FileSystemEventArgs e)
        private static void EventoDetectado_Crearon(string name, string fullpath)
        {
            List<ValidacionInterfazBE> _lstValidacion;
            string nombre_fichero = "";
            string ruta_fichero = "";
            try
            {
                if (name.Length > 0 & name.Contains(".")){
                    nombre_fichero = name.Split('.')[0];
                    ruta_fichero = fullpath.ToString();

                    _lstValidacion = new List<ValidacionInterfazBE>();
                    _lstValidacion = (new ValidacionInterfazBL()).ListarValidaciones_xInterfaz(nombre_fichero);

                    InterfazReferenciasBL _objBL;
                    switch (nombre_fichero){
                        case "PE_OL1_REFER": /*Interfaz Referencias*/

 
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
                throw ex;
                //Console.WriteLine(ex.Message);
            }
         
        }








        private static void DefinirVariablesGlobales()
        {
            GlobalVariables.Ruta_sftp = System.Configuration.ConfigurationSettings.AppSettings["ruta_sftp"].ToString();
            GlobalVariables.Ruta_fichero_detino_Ref = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Ref"].ToString();
            GlobalVariables.Ruta_fichero_detino_Exp = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Exp"].ToString();
            GlobalVariables.Ruta_fichero_detino_Pref = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Pref"].ToString();
            GlobalVariables.Ruta_fichero_detino_Sum = System.Configuration.ConfigurationSettings.AppSettings["ruta_fichero_detino_Sum"].ToString();
        }
        #endregion
    }



    



}
