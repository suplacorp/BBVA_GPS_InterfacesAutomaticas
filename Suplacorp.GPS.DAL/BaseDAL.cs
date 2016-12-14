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

        public string ObtenerDestinatariosReporteInterfaz(int idInterfaz){
            string result = "";

            try{
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_SEL_OBTENER_DESTINATARIOS_NOTIFICACION");
                sqlDatabase.AddInParameter(cmd, "@IDINTERFAZ", DbType.Int32, idInterfaz);
                result = sqlDatabase.ExecuteScalar(cmd).ToString();
            }
            catch{
                throw;
            }
            return result;
        }


    }
}
