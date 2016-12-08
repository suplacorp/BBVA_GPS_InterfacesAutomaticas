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

namespace BBVA_GPS_InterfazExpediciones
{
    class Program
    {
        static void Main(string[] args){

            GenerarInterfazExpediciones();
        }

        public static bool GenerarInterfazExpediciones() {

            bool result = false;

            int idregini = 0;
            /*1) Primero GENERAR el @IDREGINI */
            if ((new InterfazExpedicionesBL()).GenerarInterfazExpediciones(ref idregini)) {
  
                /*2) Obtener la lista total de la expedición */
                //Obtener lista de los pedidos que se acaban de generar
                List<InterfazExpediciones_ExpedicionBE> lstExpediciones = new List<InterfazExpediciones_ExpedicionBE>();
                lstExpediciones = (new InterfazExpedicionesBL()).ObtenerExpedicionesGeneradas_Log(idregini);

                if (lstExpediciones.Count > 0)
                {
                    /*3) armar fichero de expediciones */

                    /*4) notificar por email el fichero y el reporte  */

                    result = true;
                }

                /*5) probar todo el flujo completo!!!  */
                
            }

            return result;


        }
    }
}
