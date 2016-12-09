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
            List<InterfazExpediciones_ExpedicionBE> lstExpediciones;
            /*1) GENERAR INTERFAZ DE EXPEDICIONES */
            if ((new InterfazExpedicionesBL()).GenerarInterfazExpediciones(ref idregini)) {
  
                /*2) OBTENER LA LISTA TOTAL DE LA EXPEDICIÓN PREVIAMENTE GENERADA */
                lstExpediciones = new List<InterfazExpediciones_ExpedicionBE>();
                lstExpediciones = (new InterfazExpedicionesBL()).ObtenerExpedicionesGeneradas_Log(idregini);

                if (lstExpediciones.Count > 0)
                {
                    /*3) ARMAR FICHERO DE EXPEDICIONES */

                    /*4) NOTIFICAR POR EMAIL EL FICHERO Y EL REPORTE  */

                    result = true;
                }

                /*5) PROBAR TODO EL FLUJO COMPLETO!!!  */
            }


            /*PROVISIONAL - BORRAR LUEGO*/
            idregini = 320;
            lstExpediciones = new List<InterfazExpediciones_ExpedicionBE>();
            lstExpediciones = (new InterfazExpedicionesBL()).ObtenerExpedicionesGeneradas_Log(idregini);
            /*FIN PROVISIONAL - BORRAR LUEGO*/

            return result;


        }
    }
}
