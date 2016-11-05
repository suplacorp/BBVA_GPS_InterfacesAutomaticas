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

namespace Suplacorp.GPS.DAL
{
    public class InterfazReferenciasDAL : BaseDAL
    {
        #region Constructor
        public InterfazReferenciasDAL()
        {

        }
        #endregion

        public bool RegistrarInterfaz(ref InterfazReferencias_RegIniBE interfazReferencias_RegIniBE) {
            string result = "";
            string[] result_valores;

            try {

                DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_INS_IREF_AGREGAR_REGINI");
                sqlDatabase.AddInParameter(cmd, "NUMERAL", DbType.String, interfazReferencias_RegIniBE.Numeral);
                sqlDatabase.AddInParameter(cmd, "TIPO_REGISTRO", DbType.String, interfazReferencias_RegIniBE.Tipo_registro);
                sqlDatabase.AddInParameter(cmd, "TIPO_INTERFAZ", DbType.String, interfazReferencias_RegIniBE.Tipo_interfaz);
                sqlDatabase.AddInParameter(cmd, "PAIS", DbType.String, interfazReferencias_RegIniBE.Pais);
                sqlDatabase.AddInParameter(cmd, "IDENTIFICADOR_INTERFAZ", DbType.String, interfazReferencias_RegIniBE.Identificador_interfaz);
                sqlDatabase.AddInParameter(cmd, "NOMBRE_FICHERO", DbType.String, interfazReferencias_RegIniBE.Nombre_fichero);
                sqlDatabase.AddInParameter(cmd, "NOMBRE_FICHERO_DESTINO", DbType.String, interfazReferencias_RegIniBE.Nombre_fichero_detino);
                sqlDatabase.AddInParameter(cmd, "FECHA_EJECUCION", DbType.DateTime, interfazReferencias_RegIniBE.Fecha_ejecucion);
                sqlDatabase.AddInParameter(cmd, "HORA_PROCESO", DbType.String, interfazReferencias_RegIniBE.Hora_proceso);
                sqlDatabase.AddInParameter(cmd, "RUTA_FICHERO", DbType.String, interfazReferencias_RegIniBE.Ruta_fichero_detino);
                sqlDatabase.AddInParameter(cmd, "IDINTERFAZ", DbType.Int32, interfazReferencias_RegIniBE.Interfaz.Idinterface);
                sqlDatabase.AddInParameter(cmd, "IDTIPOREGISTRO", DbType.Int32, interfazReferencias_RegIniBE.Tiporegistro.Idtiporegistro);
                sqlDatabase.AddInParameter(cmd, "PROCESADO", DbType.Boolean, interfazReferencias_RegIniBE.Procesado);
                sqlDatabase.AddInParameter(cmd, "NUMERO_TOTAL_REGISTROS_FIN", DbType.String, interfazReferencias_RegIniBE.Numero_total_registros_fin);
                sqlDatabase.AddInParameter(cmd, "TIPO_REGISTRO_FIN", DbType.String, interfazReferencias_RegIniBE.Tipo_registro_fin);
                sqlDatabase.AddInParameter(cmd, "NUMERO_REGISTROS_PROCESO_FIN", DbType.String, interfazReferencias_RegIniBE.Numero_registros_proceso_fin);
                sqlDatabase.AddInParameter(cmd, "NUMERO_REGISTROS_TIPO2_FIN", DbType.String, interfazReferencias_RegIniBE.Numero_registros_tipo2_fin);
                sqlDatabase.AddInParameter(cmd, "NUMERO_REGISTROS_TIPO3_FIN", DbType.String, interfazReferencias_RegIniBE.Numero_registros_tipo3_fin);
                
                result = sqlDatabase.ExecuteScalar(cmd).ToString();
                result_valores = result.Split(';');
                
                /* Registró el registro inicial correctamente */
                if (int.Parse(result_valores[0]) != 0){
                    interfazReferencias_RegIniBE.Idregini = int.Parse(result_valores[0]);
                    return true;
                }
                else {
                    /* Ocurrió un error en el registro inicial */
                    return false;
                }
            }
            catch (Exception ex) {
                throw;
            }

        }
    }
}
