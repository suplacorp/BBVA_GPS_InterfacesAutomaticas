using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using Suplacorp.GPS.BE;
using Suplacorp.GPS.Utils;

namespace Suplacorp.GPS.DAL.Suministros
{
    public class InterfazSuministrosDAL : BaseDAL, IInterfazRegIniDAL<InterfazSuministros_RegIniBE>
    {
        #region Constructor
        public InterfazSuministrosDAL()
        {

        }
        public string RegistrarRegIni(ref InterfazSuministros_RegIniBE interfaz_RegIniBE)
        {
            throw new NotImplementedException();
        }

        public string RegistrarProc(ref InterfazSuministros_RegIniBE interfaz_RegIniBE)
        {
            throw new NotImplementedException();   
        }

        public string RegistrarCab(ref InterfazSuministros_RegIniBE interfaz_RegIniBE)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
