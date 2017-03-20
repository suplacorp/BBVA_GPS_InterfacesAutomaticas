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
            string result = "";
            try
            {
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_INS_IPREFACT_AGREGAR_REGCAB");
                sqlDatabase.AddInParameter(cmd, "IDREGINI", DbType.Int32, interfaz_RegIniBE.Idregini);
                sqlDatabase.AddInParameter(cmd, "NUMERAL", DbType.String, interfaz_RegCabBE.Numeral);
                sqlDatabase.AddInParameter(cmd, "TIPO_REGISTRO", DbType.String, interfaz_RegCabBE.Tipo_registro);
                sqlDatabase.AddInParameter(cmd, "NUMERO_DOCUMENTO_PRELIMINAR", DbType.String, interfaz_RegCabBE.Numero_documento_preliminar);
                sqlDatabase.AddInParameter(cmd, "SOCIEDAD", DbType.String, interfaz_RegCabBE.Sociedad);
                sqlDatabase.AddInParameter(cmd, "CLAVE_MONEDA", DbType.String, interfaz_RegCabBE.Clave_moneda);
                sqlDatabase.AddInParameter(cmd, "FACT_IMPORTE_POSIT_NEGAT", DbType.String, interfaz_RegCabBE.Fact_importe_posit_negat);
                sqlDatabase.AddInParameter(cmd, "IMPORTE_TOTAL_FACTURA_SIN_IMPUESTOS", DbType.Decimal, interfaz_RegCabBE.Importe_total_factura_sin_impuestos);
                sqlDatabase.AddInParameter(cmd, "IMPORTE_TOTAL_IMPUESTOS", DbType.Decimal, interfaz_RegCabBE.Importe_total_impuestos);
                sqlDatabase.AddInParameter(cmd, "FECHA_FACTURA", DbType.DateTime, interfaz_RegCabBE.Fecha_factura);
                sqlDatabase.AddInParameter(cmd, "EJERCICIO", DbType.String, interfaz_RegCabBE.Ejercicio);
                sqlDatabase.AddInParameter(cmd, "PROCESADO", DbType.Int32, interfaz_RegCabBE.Procesado);

                result = sqlDatabase.ExecuteScalar(cmd).ToString();
            }
            catch{
                throw;
            }
            return result;
        }

        public string RegistrarPos(InterfazPrefacturas_RegCabBE interfaz_RegCabBE)
        {
            string sql = "";
            string sql_aux = "";
            StringBuilder sql2 = new StringBuilder();
            string result = "";

            try
            {
                sql =
                    " " + "\r\n" +
                    "BEGIN TRANSACTION; " + "\r\n" +
                    "BEGIN TRY" + "\r\n" +
                    "DECLARE @ID_ERROR INT = 0 /*CAPTURANDO ERROR*/" + "\r\n";
                sql2.Append(sql);

                foreach (var pos in interfaz_RegCabBE.LstInterfazPrefacturas_RegPosBE)
                {
                    sql_aux = " " + "\r\n" +
                            "INSERT INTO BBVA_GPS_INTERFAZ_PREFACTURAS_REG_POS " + "\r\n" +
                            "(" + "\r\n" +
                            "    IDCAB," + "\r\n" +
                            "    NUMERAL," + "\r\n" +
                            "    TIPO_REGISTRO," + "\r\n" +
                            "    NUMERO_DOCUMENTO_COMPRAS," + "\r\n" +
                            "    POSICION_DOCUMENTO_COMPRAS," + "\r\n" +
                            "    NUMERO_DOC_REF_ALBARAN," + "\r\n" +
                            "    POSICION_FACT_IMPORT_POST_NEG," + "\r\n" +
                            "    IMPORTE_TOTAL_POSICION_SIN_IMPUESTOS," + "\r\n" +
                            "    PORCENTAJE_IMPUESTO," + "\r\n" +
                            "    NUMERO_MATERIAL_SERVICIO," + "\r\n" +
                            "    CANTIDAD," + "\r\n" +
                            "    UNIDAD_MEDIDA_BASE," + "\r\n" +
                            "    PROCESADO" + "\r\n" +
                            ")" + "\r\n" +
                            "VALUES(" + "\r\n" +
                            "   " + interfaz_RegCabBE.Idcab.ToString() + "," + "\r\n" +
                            "   '" + pos.Numeral + "'," + "\r\n" +
                            "   '" + pos.Tipo_registro + "'," + "\r\n" +
                            "   '" + pos.Numero_documento_compras + "'," + "\r\n" +
                            "   '" + pos.Posicion_documento_compras + "'," + "\r\n" +
                            "   '" + pos.Numero_doc_ref_albaran + "'," + "\r\n" +
                            "   '" + pos.Posicion_fact_import_post_neg + "'," + "\r\n" +
                            "   " + pos.Importe_total_posicion_sin_impuestos + "," + "\r\n" +
                            "   " + pos.Porcentaje_impuesto + "," + "\r\n" +
                            "   '" + pos.Numero_material_servicio + "'," + "\r\n" +
                            "   " + pos.Cantidad + "," + "\r\n" +
                            "   '" + pos.Unidad_medida_base + "'," + "\r\n" +
                            "   " + pos.Procesado + "" + "\r\n" +
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
                    "SELECT @ROWS_AFFECTED=COUNT(IDPOS) FROM BBVA_GPS_INTERFAZ_PREFACTURAS_REG_POS (NOLOCK) WHERE IDCAB = " + interfaz_RegCabBE.Idcab.ToString() + " " + "\r\n" +
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
            }
            catch{
                throw;
            }
            return result;
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
                
                result = sqlDatabase.ExecuteScalar(cmd).ToString();
            }
            catch{
                throw;
            }
            return result;
        }

        public string MarcarGuiasAsociadas_Prefactura(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE)
        {
            string result = "";

            try
            {
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("bbva_gps_MarcarGuiasAsociadas_Prefactura");
                cmd.CommandTimeout = 600; /*10 minutos*/
                sqlDatabase.AddInParameter(cmd, "idregini_prefactura", DbType.Int32, interfaz_RegIniBE.Idregini);
                
                result = sqlDatabase.ExecuteScalar(cmd).ToString();
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
