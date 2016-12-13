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
    public class InterfazPrefacturaDAL : BaseDAL, IInterfazRegIniDAL<InterfazPrefacturas_RegIniBE, InterfazPrefacturas_RegCabBE>
    {
        public string RegistrarCab(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE, InterfazPrefacturas_RegCabBE interfaz_RegCabBE)
        {
            throw new NotImplementedException();
        }

        public string RegistrarPos(InterfazPrefacturas_RegCabBE interfaz_RegCabBE)
        {
            throw new NotImplementedException();
        }

        public string RegistrarProc(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE)
        {
            //NO SE IMPLEMENTARÁ
            throw new NotImplementedException();
        }

        public string RegistrarRegIni(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE)
        {
            string result = "";

            try
            {
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_INS_IPREFACT_AGREGAR_REGINI");

                /*
                sqlDatabase.AddInParameter(cmd, "NUMERAL", DbType.String, interfaz_RegIniBE.Numeral);
                sqlDatabase.AddInParameter(cmd, "TIPO_REGISTRO", DbType.String, interfaz_RegIniBE.Tipo_registro);
                sqlDatabase.AddInParameter(cmd, "TIPO_INTERFAZ", DbType.String, interfaz_RegIniBE.Tipo_interfaz);
                sqlDatabase.AddInParameter(cmd, "PAIS", DbType.String, interfaz_RegIniBE.Pais);
                sqlDatabase.AddInParameter(cmd, "IDENTIFICADOR_INTERFAZ", DbType.String, interfaz_RegIniBE.Identificador_interfaz);
                sqlDatabase.AddInParameter(cmd, "NOMBRE_FICHERO", DbType.String, interfaz_RegIniBE.Nombre_fichero);
                sqlDatabase.AddInParameter(cmd, "NOMBRE_FICHERO_DESTINO", DbType.String, interfaz_RegIniBE.Nombre_fichero_detino);
                sqlDatabase.AddInParameter(cmd, "FECHA_EJECUCION", DbType.DateTime, interfaz_RegIniBE.Fecha_ejecucion);
                sqlDatabase.AddInParameter(cmd, "HORA_PROCESO", DbType.String, interfaz_RegIniBE.Hora_proceso);
                sqlDatabase.AddInParameter(cmd, "RUTA_FICHERO", DbType.String, interfaz_RegIniBE.Ruta_fichero_detino);
                sqlDatabase.AddInParameter(cmd, "IDINTERFAZ", DbType.Int32, interfaz_RegIniBE.Interfaz.Idinterface);
                sqlDatabase.AddInParameter(cmd, "IDTIPOREGISTRO", DbType.Int32, interfaz_RegIniBE.Tiporegistro.Idtiporegistro);
                sqlDatabase.AddInParameter(cmd, "PROCESADO", DbType.Int32, interfaz_RegIniBE.Procesado);
                sqlDatabase.AddInParameter(cmd, "NUMERO_TOTAL_REGISTROS_FIN", DbType.String, interfaz_RegIniBE.Numero_total_registros_fin);
                sqlDatabase.AddInParameter(cmd, "TIPO_REGISTRO_FIN", DbType.String, interfaz_RegIniBE.Tipo_registro_fin);
                sqlDatabase.AddInParameter(cmd, "NUMERO_REGISTROS_CAB_FIN", DbType.String, interfaz_RegIniBE.Numero_registros_cab_fin);
                sqlDatabase.AddInParameter(cmd, "NUMERO_REGISTROS_POS_FIN", DbType.String, interfaz_RegIniBE.Numero_registros_pos_fin);
                sqlDatabase.AddInParameter(cmd, "NUMERO_REGISTROS_TIPO3_FIN", DbType.String, interfaz_RegIniBE.Numero_registros_tipo3_fin);
                */
                result = sqlDatabase.ExecuteScalar(cmd).ToString();
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }
    }
}
