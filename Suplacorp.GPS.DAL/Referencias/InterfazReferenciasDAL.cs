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
    public class InterfazReferenciasDAL : BaseDAL, IInterfazRegIniDAL<InterfazReferencias_RegIniBE, InterfazSuministros_RegIniBE>
    {
        #region Constructor
        public InterfazReferenciasDAL()
        {

        }
        #endregion

        public string RegistrarRegIni(ref InterfazReferencias_RegIniBE interfazReferencias_RegIniBE) {
            string result = "";

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
                sqlDatabase.AddInParameter(cmd, "PROCESADO", DbType.Int32, interfazReferencias_RegIniBE.Procesado);
                sqlDatabase.AddInParameter(cmd, "NUMERO_TOTAL_REGISTROS_FIN", DbType.String, interfazReferencias_RegIniBE.Numero_total_registros_fin);
                sqlDatabase.AddInParameter(cmd, "TIPO_REGISTRO_FIN", DbType.String, interfazReferencias_RegIniBE.Tipo_registro_fin);
                sqlDatabase.AddInParameter(cmd, "NUMERO_REGISTROS_PROCESO_FIN", DbType.String, interfazReferencias_RegIniBE.Numero_registros_proceso_fin);
                sqlDatabase.AddInParameter(cmd, "NUMERO_REGISTROS_TIPO2_FIN", DbType.String, interfazReferencias_RegIniBE.Numero_registros_tipo2_fin);
                sqlDatabase.AddInParameter(cmd, "NUMERO_REGISTROS_TIPO3_FIN", DbType.String, interfazReferencias_RegIniBE.Numero_registros_tipo3_fin);
                
                result = sqlDatabase.ExecuteScalar(cmd).ToString();
                return result;

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return result;
            }
        }

        public string RegistrarProc(ref InterfazReferencias_RegIniBE interfazReferencias_RegIniBE) {
            string sql = "";
            string sql_aux = "";
            StringBuilder sql2 = new StringBuilder();
            string result = "";

            try{
                sql =
                    " " + "\r\n" +
                    "BEGIN TRANSACTION; " + "\r\n" +
                    "BEGIN TRY" + "\r\n" +
                    "DECLARE @ID_ERROR INT = 0 /*CAPTURANDO ERROR*/" + "\r\n";
                sql2.Append(sql);

                foreach (var proc in interfazReferencias_RegIniBE.LstInterfazReferencias_RegProcBE)
                {
                    sql_aux = " " + "\r\n" +
                            "INSERT INTO BBVA_GPS_INTERFAZ_REFERENCIAS_REG_PROC " + "\r\n" +
                            "(" + "\r\n" +
                            "    IDREGINI," + "\r\n" +
                            "    NUMERAL," + "\r\n" +
                            "    TIPO_REGISTRO," + "\r\n" +
                            "    SOCIEDAD," + "\r\n" +
                            "    CODIGO_MATERIAL," + "\r\n" +
                            "    TEXTO_BREVE_MATERIAL," + "\r\n" +
                            "    UNIDAD_MEDIDA_BASE," + "\r\n" +
                            "    UNIDAD_MEDIDA_PEDIDO," + "\r\n" +
                            "    CONTADOR_CONV_UND_MED_BASE," + "\r\n" +
                            "    DENOMINADOR_CONV_UND_MED_BASE," + "\r\n" +
                            "    STATUS_MAT_TODOS_CENTROS," + "\r\n" +
                            "    STATUS_MAT_ESPECIF_CENTRO," + "\r\n" +
                            "    TIPO_APROV," + "\r\n" +
                            "    INDICADOR_BREVE," + "\r\n" +
                            "    PESO_BRUTO," + "\r\n" +
                            "    UNIDAD_PESO," + "\r\n" +
                            "    VOLUMEN," + "\r\n" +
                            "    UNIDAD_VOLUMEN," + "\r\n" +
                            "    CODIGO_ANTIGUO_MATERIAL," + "\r\n" +
                            "    CENTRO," + "\r\n" +
                            "    PROCESADO" + "\r\n" +
                            ")" + "\r\n" +
                            "VALUES(" + "\r\n" +
                            "   " + interfazReferencias_RegIniBE.Idregini.ToString() + "," + "\r\n" +
                            "   '" + proc.Numeral + "'," + "\r\n" +
                            "   '" + proc.Tipo_registro + "'," + "\r\n" +
                            "   '" + proc.Sociedad + "'," + "\r\n" +
                            "   '" + proc.Codigo_material + "'," + "\r\n" +
                            "   '" + proc.Texto_breve_material + "'," + "\r\n" +
                            "   '" + proc.Unidad_medida_base + "'," + "\r\n" +
                            "   '" + proc.Unidad_medida_pedido + "'," + "\r\n" +
                            "   '" + proc.Contador_conv_und_med_base + "'," + "\r\n" +
                            "   '" + proc.Denominador_conv_und_med_base + "'," + "\r\n" +
                            "   '" + proc.Status_mat_todos_centros + "'," + "\r\n" +
                            "   '" + proc.Status_mat_especif_centro + "'," + "\r\n" +
                            "   '" + proc.Tipo_aprov + "'," + "\r\n" +
                            "   '" + proc.Indicador_breve + "'," + "\r\n" +
                            "   " + proc.Peso_bruto + "," + "\r\n" +
                            "   '" + proc.Unidad_peso + "'," + "\r\n" +
                            "   " + proc.Volumen + "," + "\r\n" +
                            "   '" + proc.Unidad_volumen + "'," + "\r\n" +
                            "   '" + proc.Codigo_antiguo_material + "'," + "\r\n" +
                            "   '" + proc.Centro + "'," + "\r\n" +
                            "   " + proc.Procesado + "" + "\r\n" +
                            ")" + "\r\n" +
                            "";
                    sql2.Append(sql_aux);
                }

                sql =
                    "END TRY" + "\r\n" +
                    "BEGIN CATCH" + "\r\n" +
                    "" + "\r\n" +
                    "IF @@TRANCOUNT > 0" + "\r\n" +
                    "BEGIN" + "\r\n" +
                    "   ROLLBACK TRANSACTION;" + "\r\n" +
                    "END" + "\r\n" +
                    "" + "\r\n" +
                    "/* REGISTRANDO ERROR EN LOG*/" + "\r\n" +
                    "DECLARE @ERROR_PROCEDURE VARCHAR(MAX),@ERROR_LINE VARCHAR(MAX), @ERROR_MESSAGE VARCHAR(MAX), @ERROR_STATE VARCHAR(MAX)" + "\r\n" +
                    "SELECT  @ERROR_PROCEDURE = ERROR_PROCEDURE(), @ERROR_LINE = ERROR_LINE(), @ERROR_MESSAGE = ERROR_MESSAGE(), @ERROR_STATE = ERROR_STATE()" + "\r\n" +
                    "EXEC BBVA_GPS_REGISTRARERROR_SQL @ERROR_PROCEDURE, @ERROR_LINE, @ERROR_MESSAGE, @ERROR_STATE, @ID_ERROR OUTPUT" + "\r\n" +
                    "/*FIN REGISTRO ERROR*/" + "\r\n" +
                    "END CATCH;" + "\r\n" +
                    "" + "\r\n" +
                    "/* RETORNANDO RESULTADO*/" + "\r\n" +
                    "DECLARE @ROWS_AFFECTED INT = 0 " + "\r\n" +
                    "SELECT @ROWS_AFFECTED=COUNT(IDPROC) FROM BBVA_GPS_INTERFAZ_REFERENCIAS_REG_PROC (NOLOCK) WHERE IDREGINI = "+ interfazReferencias_RegIniBE.Idregini.ToString() + " " + "\r\n" +
                    "SELECT CONVERT(VARCHAR(20), @ROWS_AFFECTED) + ';' + CONVERT(VARCHAR(20), @ID_ERROR)" + "\r\n" +
                    "" + "\r\n" +
                    "IF @@TRANCOUNT > 0" + "\r\n" +
                    "BEGIN" + "\r\n" +
                    "    COMMIT TRANSACTION;" + "\r\n" +
                    "" + "\r\n" +
                    "END";
                sql2.Append(sql);
                
                DbCommand cmd = sqlDatabase.GetSqlStringCommand(sql2.ToString());
                result = sqlDatabase.ExecuteScalar(cmd).ToString();
                return result;

            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
                return result;
            }
        }

        public string ActualizarClienteArticulo_IntRef(ref InterfazReferencias_RegIniBE interfaz_RegIniBE)
        {
            string result = "";

            try{
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_UPD_IREF_ACT_CLIENT_ART");
                sqlDatabase.AddInParameter(cmd, "IDREGINI", DbType.Int32, interfaz_RegIniBE.Idregini);
                sqlDatabase.AddInParameter(cmd, "IDCLIENTE", DbType.Int32, GlobalVariables.IdCliente);
                result = sqlDatabase.ExecuteScalar(cmd).ToString();
            }   
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public string RegistrarCab(ref InterfazReferencias_RegIniBE interfaz_RegIniBE,  InterfazSuministros_RegIniBE interfaz_RegCabBE)
        {
            //NO SE IMPLEMENTARÁ
            throw new NotImplementedException();
        }

        public string RegistrarPos(InterfazSuministros_RegIniBE interfaz_RegCabBE)
        {
            throw new NotImplementedException();
        }
    }
}
