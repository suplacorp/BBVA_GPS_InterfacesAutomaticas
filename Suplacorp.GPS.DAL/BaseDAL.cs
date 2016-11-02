using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Suplacorp.GPS.DAL
{
    public class BaseDAL
    {
        //Protected dtrResultado As SqlClient.SqlDataReader
        protected string strConnectionString;
        //protected SqlDataReader dtrResultado;

        public BaseDAL() {
            strConnectionString = DAL.CreateConnection("ConnectionString");
            
        }

    }
}
