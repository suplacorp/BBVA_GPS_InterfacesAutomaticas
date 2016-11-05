using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data.Sql;



namespace Suplacorp.GPS.DAL
{
    public class BaseDAL
    {
        protected SqlDatabase sqlDatabase;
        protected string strConnectionString;
        
        public BaseDAL() {
            strConnectionString = DAL.CreateConnection("ConnectionString");
            sqlDatabase = new SqlDatabase(this.strConnectionString);
        }

    }
}
