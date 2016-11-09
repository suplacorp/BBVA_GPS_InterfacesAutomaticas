using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;

namespace Suplacorp.GPS.BL
{
    public interface IInterfazRegCabBL<RegIni, RegCab>
    {
        #region LlenarEntidades

        RegCab LlenarEntidad_RegCab(ref RegIni interfaz_RegIniBE, ref String[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegCab);

        #endregion

        bool RegistrarInterfaz_RegCab(ref RegIni interfaz_RegIniBE);
    }
}
