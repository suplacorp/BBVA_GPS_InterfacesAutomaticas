using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;

namespace Suplacorp.GPS.BL
{
    public interface IInterfaz<T>
    {
        List<T> LeerFicheroInterfaz(string nombre_fichero, string ruta_fichero, List<ValidacionInterfazBE> lstValidacion);
        T LlenarEntidad(string linea_actual, params List<ValidacionInterfazBE>[] listas_validacion_x_tiporegistro);
    }
}
