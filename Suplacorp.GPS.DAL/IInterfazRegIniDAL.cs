using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;

namespace Suplacorp.GPS.DAL
{
    public interface IInterfazRegIniDAL<RegIni, RegCab>
    {
        string RegistrarRegIni(ref RegIni interfaz_RegIniBE);

        string RegistrarProc(ref RegIni interfaz_RegIniBE);

        string RegistrarCab(ref RegIni interfaz_RegIniBE, RegCab interfaz_RegCabBE);

        string RegistrarPos(RegCab interfaz_RegCabBE);
        

    }       
}
