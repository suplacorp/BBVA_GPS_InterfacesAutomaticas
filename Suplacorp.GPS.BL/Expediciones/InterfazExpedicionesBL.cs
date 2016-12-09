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
    public class InterfazExpedicionesBL : BaseBL<InterfazExpediciones_ExpedicionBE>
    {

        public List<InterfazExpediciones_ExpedicionBE> ObtenerExpedicionesGeneradas_Log(int idregini)
        {
            try{
                return (new InterfazExpedicionesDAL()).ObtenerExpedicionesGeneradas_Log(idregini);
            }
            catch (Exception ex){
                throw;
            }
        }

        public bool GenerarInterfazExpediciones(ref int idregini) {
            string[] result_valores;
            bool result = false;
            
            try{
                //Registrando en BD la Interfaz de Expediciones (Generando la interfaz de expediciones)
                result_valores = (new InterfazExpedicionesDAL()).GenerarInterfazExpediciones("NOMBRE_FICHERO_DESTINO_KIKE").Split(';');
                idregini = int.Parse(result_valores[0]);

                if (idregini != 0){
                    result = true;
                }
                else if(int.Parse(result_valores[1]) != 0) {
                    /*Ocurrió un error, mostrar el mensaje con el id error, notificar ejecutivo, etc etc etc*/
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                /*Ocurrió un error, mostrar el mensaje con el id error, notificar ejecutivo, etc etc etc*/
            }
            return result;
        }

    }
}
