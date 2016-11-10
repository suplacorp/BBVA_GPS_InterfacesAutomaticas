using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;

namespace Suplacorp.GPS.BL
{
    public interface IInterfazRegPosBL<RegIni, RegCab, RegPos>
    {
        #region LlenarEntidades

        RegPos LlenarEntidad_RegPos(ref RegIni interfaz_RegIniBE, ref String[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegPos);

        #endregion
    }
}
