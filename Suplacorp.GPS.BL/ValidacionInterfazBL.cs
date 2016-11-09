using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.DAL;
using Suplacorp.GPS.BE;

namespace Suplacorp.GPS.BL
{
    public class ValidacionInterfazBL : BaseBL<ValidacionInterfazBE>
    {
        public ValidacionInterfazBL() {

        }

        public List<ValidacionInterfazBE> ListarValidaciones_xInterfaz(string nombre_fichero) {
            try {
                return (new ValidacionInterfazDAL()).ListarValidaciones_xInterfaz(nombre_fichero);
            }
            catch (Exception ex) {
                throw;
            }
        }

        public Dictionary<string, object> CargarVariablesIniciales()
        {
            try{
                return (new ValidacionInterfazDAL()).CargarVariablesIniciales();
            }
            catch (Exception ex){
                throw;
            }
        }

    }
}
