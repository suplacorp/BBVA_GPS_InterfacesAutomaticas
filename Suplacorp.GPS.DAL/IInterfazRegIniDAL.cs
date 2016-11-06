using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;

namespace Suplacorp.GPS.DAL
{
    public interface IInterfazRegIniDAL<RegIni, RegProc>
    {
        string RegistrarRegIni(ref RegIni interfaz_RegIniBE);
        string RegistrarProc(ref RegIni interfaz_RegIniBE);

    }       
}
