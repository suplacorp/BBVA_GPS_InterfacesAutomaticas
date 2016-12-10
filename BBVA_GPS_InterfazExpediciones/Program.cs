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
            InterfazExpediciones_RegIniBE intExpediciones;
            
            /*1) GENERAR INTERFAZ DE EXPEDICIONES */
            if ((new InterfazExpedicionesBL()).GenerarInterfazExpediciones(ref idregini)) {

                intExpediciones = new InterfazExpediciones_RegIniBE();
                intExpediciones.Idregini = idregini;

                /*2) OBTENER LA LISTA TOTAL DE LA EXPEDICIÓN PREVIAMENTE GENERADA */
                if ((new InterfazExpedicionesBL()).ObtenerExpedicionesGeneradas(ref intExpediciones)) {
                    if (intExpediciones.LstInterfazExpediciones_RegCabBE.Count > 0) {

                        /*3) GENERAR "FICHERO DE EXPEDICIONES PARA EL BBVA " */


                        /*4) NOTIFICAR POR EMAIL EL "FICHERO" Y EL "REPORTE HTML" */

                        result = true;
                    }        
                }
                /*5) PROBAR TODO EL FLUJO COMPLETO!!!  */
            }


            /*PROVISIONAL - BORRAR LUEGO*/
            intExpediciones = new InterfazExpediciones_RegIniBE();
            intExpediciones.Idregini = 320;
            if ((new InterfazExpedicionesBL()).ObtenerExpedicionesGeneradas(ref intExpediciones))
            {
                if (intExpediciones.LstInterfazExpediciones_RegCabBE.Count > 0)
                {
                    /*3) GENERAR "FICHERO DE EXPEDICIONES PARA EL BBVA " */


                    /*4) NOTIFICAR POR EMAIL EL "FICHERO" Y EL "REPORTE HTML" */

                    result = true;
                }
            }
            /*FIN PROVISIONAL - BORRAR LUEGO*/


            return result;
        }
    }
}
