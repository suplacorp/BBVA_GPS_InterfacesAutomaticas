using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace Suplacorp.GPS.DAL
{
    public class ValidacionInterfazDAL : BaseDAL
    {
        public ValidacionInterfazDAL() {
            
        }

        public void kike() {
            
            //TEST
            SqlDatabase sqlDatabase = new SqlDatabase(base.strConnectionString);
            using (IDataReader reader = sqlDatabase.ExecuteReader(CommandType.Text, "SELECT TOP 1 * FROM usuariosweb")){
                DisplayRowValues(reader);
            }
        }
        static void DisplayRowValues(IDataReader reader)
        {
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine("{0} = {1}", reader.GetName(i), reader[i].ToString());
                }
                Console.WriteLine();
            }
        }

    }
}
