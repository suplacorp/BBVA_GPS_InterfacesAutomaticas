using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;
using System.Collections;
using System.Reflection;
using Suplacorp.GPS.Utils;
using System.IO;
using Suplacorp.GPS.DAL;
using System.Collections.Specialized;

namespace Suplacorp.GPS.BL
{
    public class LogExternoExpedicionesBL : BaseBL<string>
    {

        public string LeerFicheroInterfazLogExterno(string nombre_fichero, string ruta_fichero_lectura)
        {
            StringBuilder strBuilder = new StringBuilder();
            string linea_actual;
            
            try
            {

                using (StreamReader file = new StreamReader(ruta_fichero_lectura))
                {
                    while ((linea_actual = file.ReadLine()) != null)
                    {
                        if (linea_actual.Length > 0 && linea_actual != "")
                        {
                            strBuilder.Append(linea_actual + "\n");
                        }
                    }
                }

            }
            catch (Exception ex){
                Console.WriteLine(ex.Message + ";" + ex.Source.ToString() + ";" + ex.StackTrace.ToString());
            }
            return strBuilder.ToString();
        }
    }
}
