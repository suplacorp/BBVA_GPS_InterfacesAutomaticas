using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;

namespace Suplacorp.GPS.BL
{
    public interface IInterfazRegIni<RegIni,RegProc>
    {
        RegIni LeerFicheroInterfaz(string nombre_fichero, string ruta_fichero_lectura, List<ValidacionInterfazBE> lstValidacion);

        void LlenarEntidad_RegIni(ref RegIni interfaz_RegIniBE, ref String[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegIni);

        RegProc LlenarEntidad_RegProc(ref RegIni interfaz_RegIniBE, ref String[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegProc);

         void LlenarEntidad_RegFin(ref RegIni interfaz_RegIniBE, ref String[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegFin);
    }
}
