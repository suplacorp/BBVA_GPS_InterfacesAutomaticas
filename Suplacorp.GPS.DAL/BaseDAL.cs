using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;


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

        public string Resetear_Proceso_Interfaz(int idinterfaz, int idregini)
        {
            string result = "";

            try
            {
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_INS_RESET_PROCESO_INTERFACES");
                sqlDatabase.AddInParameter(cmd, "@IDINTERFAZ", DbType.Int32, idinterfaz);
                sqlDatabase.AddInParameter(cmd, "@IDREGINI", DbType.Int32, idregini);
                result = sqlDatabase.ExecuteNonQuery(cmd).ToString();
            }
            catch
            {
                throw;
            }
            return result;
        }



    }
}
