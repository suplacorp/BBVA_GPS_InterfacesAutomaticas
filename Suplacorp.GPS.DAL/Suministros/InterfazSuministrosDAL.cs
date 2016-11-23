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
    public class InterfazSuministrosDAL : BaseDAL, IInterfazRegIniDAL<InterfazSuministros_RegIniBE, InterfazSuministros_RegCabBE>
    {
        #region Constructor
        public InterfazSuministrosDAL()
        {

        }
        #endregion

        public string RegistrarRegIni(ref InterfazSuministros_RegIniBE interfaz_RegIniBE)
        {
            string result = "";

            try
            {
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_INS_ISUM_AGREGAR_REGINI");

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
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }

        public string RegistrarCab(ref InterfazSuministros_RegIniBE interfaz_RegIniBE,  InterfazSuministros_RegCabBE interfaz_RegCabBE)
        {
            string result = "";

            try
            {
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_INS_ISUM_AGREGAR_REGCAB");
                sqlDatabase.AddInParameter(cmd, "IDREGINI", DbType.Int32, interfaz_RegIniBE.Idregini);
                sqlDatabase.AddInParameter(cmd, "NUMERAL", DbType.String, interfaz_RegCabBE.Numeral);
                sqlDatabase.AddInParameter(cmd, "TIPO_REGISTRO", DbType.String, interfaz_RegCabBE.Tipo_registro);
                sqlDatabase.AddInParameter(cmd, "CODIGO_CESTA", DbType.String, interfaz_RegCabBE.Codigo_cesta);
                sqlDatabase.AddInParameter(cmd, "NOMBRE_DESTINATARIO", DbType.String, interfaz_RegCabBE.Nombre_destinatario);
                sqlDatabase.AddInParameter(cmd, "DESC_UNID_OFI_ESTAB", DbType.String, interfaz_RegCabBE.Desc_unid_ofi_estab);
                sqlDatabase.AddInParameter(cmd, "CALLE_DIRECCION", DbType.String, interfaz_RegCabBE.Calle_direccion);
                sqlDatabase.AddInParameter(cmd, "NRO_DIRECCION_ENTREGA", DbType.String, interfaz_RegCabBE.Nro_direccion_entrega);
                sqlDatabase.AddInParameter(cmd, "CODIGO_POSTAL", DbType.String, interfaz_RegCabBE.Codigo_postal);
                sqlDatabase.AddInParameter(cmd, "PROVINCIA", DbType.String, interfaz_RegCabBE.Provincia);
                sqlDatabase.AddInParameter(cmd, "APARTADO_ALTERNATIVO", DbType.String, interfaz_RegCabBE.Apartado_alternativo);
                sqlDatabase.AddInParameter(cmd, "REGION_DEPARTAMENTO", DbType.String, interfaz_RegCabBE.Region_departamento);
                sqlDatabase.AddInParameter(cmd, "PAIS", DbType.String, interfaz_RegCabBE.Pais);
                sqlDatabase.AddInParameter(cmd, "TELEFONO_CONTACTO", DbType.String, interfaz_RegCabBE.Telefono_contacto);
                sqlDatabase.AddInParameter(cmd, "USUARIO", DbType.String, interfaz_RegCabBE.Usuario);
                sqlDatabase.AddInParameter(cmd, "COLONIA_COMUNA_PLANTA_DISTRITO", DbType.String, interfaz_RegCabBE.Colonia_comuna_planta_distrito);
                sqlDatabase.AddInParameter(cmd, "ENTRE_CALLE1", DbType.String, interfaz_RegCabBE.Entre_calle1);
                sqlDatabase.AddInParameter(cmd, "ENTRE_CALLE2", DbType.String, interfaz_RegCabBE.Entre_calle2);
                sqlDatabase.AddInParameter(cmd, "CENTRO", DbType.String, interfaz_RegCabBE.Centro);
                sqlDatabase.AddInParameter(cmd, "URGENTE", DbType.String, interfaz_RegCabBE.Urgente);
                sqlDatabase.AddInParameter(cmd, "PROCESADO", DbType.Int32, interfaz_RegCabBE.Procesado);

                result = sqlDatabase.ExecuteScalar(cmd).ToString();
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }

        public string RegistrarProc(ref InterfazSuministros_RegIniBE interfaz_RegIniBE)
        {
            //NO SE IMPLEMENTARÁ
            throw new NotImplementedException();
        }

        public string RegistrarPos(InterfazSuministros_RegCabBE interfaz_RegCabBE)
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

                foreach (var pos in interfaz_RegCabBE.LstInterfazSuministros_RegPosBE)
                {
                    sql_aux = " " + "\r\n" +
                            "INSERT INTO BBVA_GPS_INTERFAZ_SUMINISTROS_REG_POS " + "\r\n" +
                            "(" + "\r\n" +
                            "    IDCAB," + "\r\n" +
                            "    NUMERAL," + "\r\n" +
                            "    TIPO_REGISTRO," + "\r\n" +
                            "    CODIGO_CESTA," + "\r\n" +
                            "    MOVIMIENTO_CLASE_DOC," + "\r\n" +
                            "    PEDIDO_REF_RESERVA," + "\r\n" +
                            "    POSICION_PED_RESERVA," + "\r\n" +
                            "    MATERIAL," + "\r\n" +
                            "    FECHA_ENTREGA," + "\r\n" +
                            "    CENTRO_COSTE," + "\r\n" +
                            "    CANTIDAD_PEDIDO_RESERVA," + "\r\n" +
                            "    UNIDAD_MEDIDA_PEDIDO," + "\r\n" +
                            "    PROCESADO" + "\r\n" +
                            ")" + "\r\n" +
                            "VALUES(" + "\r\n" +
                            "   " + interfaz_RegCabBE.Idcab.ToString() + "," + "\r\n" +
                            "   '" + pos.Numeral + "'," + "\r\n" +
                            "   '" + pos.Tipo_registro + "'," + "\r\n" +
                            "   '" + pos.Codigo_cesta + "'," + "\r\n" +
                            "   '" + pos.Movimiento_clase_doc + "'," + "\r\n" +
                            "   '" + pos.Pedido_ref_reserva + "'," + "\r\n" +
                            "   '" + pos.Posicion_ped_reserva + "'," + "\r\n" +
                            "   '" + pos.Material + "'," + "\r\n" +
                            "   '" + pos.Fecha_entrega.ToString("yyyMMdd") + "'," + "\r\n" +
                            "   '" + pos.Centro_coste + "'," + "\r\n" +
                            "   '" + pos.Cantidad_pedido_reserva + "'," + "\r\n" +
                            "   '" + pos.Unidad_medida_pedido + "'," + "\r\n" +
                            "   '" + pos.Procesado + "'" + "\r\n" +
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
                    "SELECT @ROWS_AFFECTED=COUNT(IDPOS) FROM BBVA_GPS_INTERFAZ_SUMINISTROS_REG_POS (NOLOCK) WHERE IDCAB = " + interfaz_RegCabBE.Idcab.ToString() + " " + "\r\n" +
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
        }

        //public string GenerarPedidosInterfazSum(int idregini) {
        //    string result = "";

        //    try{
        //        DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_INS_ISUM_GENERAR_PEDIDOS");
        //        sqlDatabase.AddInParameter(cmd, "IDREGINI", DbType.Int32, idregini);
        //        result = sqlDatabase.ExecuteScalar(cmd).ToString();
        //        return result;
        //    }
        //    catch (Exception ex){
        //        Console.WriteLine(ex.Message);
        //        return result;
        //    }
        //}

        public string GenerarPedidosInterfazSum(int idregini)
        {
            string result = "";
            DbTransaction trans;

            using (DbConnection conn = sqlDatabase.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                try{

                    DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_INS_ISUM_GENERAR_PEDIDOS");
                    sqlDatabase.AddInParameter(cmd, "IDREGINI", DbType.Int32, idregini);

                    result = sqlDatabase.ExecuteScalar(cmd, trans).ToString();
                    trans.Commit();
                }
                catch{
                    trans.Rollback();                }
            }

            return result;
        }

        public List<InterfazSuministros_PedidoBE> ObtenerPedidosGenerados_Log(int idregini) {

            List<InterfazSuministros_PedidoBE> lstPedidosGenerados = new List<InterfazSuministros_PedidoBE>();
            InterfazSuministros_PedidoBE pedido;

            try
            {
                using (DbCommand _sqlCmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_SEL_CARGAR_PEDIDOS_GENERADOS"))
                {
                    sqlDatabase.AddInParameter(_sqlCmd, "IDREGINI", DbType.Int32, idregini);
                    sqlDatabase.AddInParameter(_sqlCmd, "IDCLIENTE", DbType.Int32, GlobalVariables.IdCliente);

                    using (IDataReader dataReader = sqlDatabase.ExecuteReader(_sqlCmd))
                    {
                        while (dataReader.Read())
                        {
                            pedido = new InterfazSuministros_PedidoBE();

                            pedido.IDREGINI = (Convert.IsDBNull(dataReader["IDREGINI"]) ? 0 : Convert.ToInt32(dataReader["IDREGINI"]));
                            pedido.NRO_PROCESO = (Convert.IsDBNull(dataReader["NRO_PROCESO"]) ? 0 : Convert.ToInt32(dataReader["NRO_PROCESO"]));
                            pedido.NOMBRE_FICHERO_DESTINO = (Convert.IsDBNull(dataReader["NOMBRE_FICHERO_DESTINO"]) ? "" : Convert.ToString(dataReader["NOMBRE_FICHERO_DESTINO"]));
                            pedido.FECHA_EJECUCION = (Convert.IsDBNull(dataReader["FECHA_EJECUCION"]) ? "" : Convert.ToString(dataReader["FECHA_EJECUCION"]));
                            pedido.HORA_PROCESO = (Convert.IsDBNull(dataReader["HORA_PROCESO"]) ? "" : Convert.ToString(dataReader["HORA_PROCESO"]));
                            pedido.RUTA_FICHERO = (Convert.IsDBNull(dataReader["RUTA_FICHERO"]) ? "" : Convert.ToString(dataReader["RUTA_FICHERO"]));
                            pedido.FECHA_REGISTRO = (Convert.IsDBNull(dataReader["FECHA_REGISTRO"]) ? "" : Convert.ToString(dataReader["FECHA_REGISTRO"]));
                            pedido.IDCAB = (Convert.IsDBNull(dataReader["IDCAB"]) ? 0 : Convert.ToInt32(dataReader["IDCAB"]));
                            pedido.CODIGO_CESTA = (Convert.IsDBNull(dataReader["CODIGO_CESTA"]) ? "" : Convert.ToString(dataReader["CODIGO_CESTA"]));
                            pedido.NOMBRE_DESTINATARIO = (Convert.IsDBNull(dataReader["NOMBRE_DESTINATARIO"]) ? "" : Convert.ToString(dataReader["NOMBRE_DESTINATARIO"]));
                            pedido.DESC_UNID_OFI_ESTAB = (Convert.IsDBNull(dataReader["DESC_UNID_OFI_ESTAB"]) ? "" : Convert.ToString(dataReader["DESC_UNID_OFI_ESTAB"]));
                            pedido.CALLE_DIRECCION = (Convert.IsDBNull(dataReader["CALLE_DIRECCION"]) ? "" : Convert.ToString(dataReader["CALLE_DIRECCION"]));
                            pedido.PROVINCIA = (Convert.IsDBNull(dataReader["PROVINCIA"]) ? "" : Convert.ToString(dataReader["PROVINCIA"]));
                            pedido.REGION_DEPARTAMENTO = (Convert.IsDBNull(dataReader["REGION_DEPARTAMENTO"]) ? "" : Convert.ToString(dataReader["REGION_DEPARTAMENTO"]));
                            pedido.TELEFONO_CONTACTO = (Convert.IsDBNull(dataReader["TELEFONO_CONTACTO"]) ? "" : Convert.ToString(dataReader["TELEFONO_CONTACTO"]));
                            pedido.USUARIO = (Convert.IsDBNull(dataReader["USUARIO"]) ? "" : Convert.ToString(dataReader["USUARIO"]));
                            pedido.COLONIA_COMUNA_PLANTA_DISTRITO = (Convert.IsDBNull(dataReader["COLONIA_COMUNA_PLANTA_DISTRITO"]) ? "" : Convert.ToString(dataReader["COLONIA_COMUNA_PLANTA_DISTRITO"]));
                            pedido.URGENTE = (Convert.IsDBNull(dataReader["URGENTE"]) ? "" : Convert.ToString(dataReader["URGENTE"]));
                            pedido.PROCESADO_CAB = (Convert.IsDBNull(dataReader["PROCESADO_CAB"]) ? "" : Convert.ToString(dataReader["PROCESADO_CAB"]));
                            pedido.MENSAJE_ERROR_CAB = (Convert.IsDBNull(dataReader["MENSAJE_ERROR_CAB"]) ? "" : Convert.ToString(dataReader["MENSAJE_ERROR_CAB"]));
                            pedido.IDPOS = (Convert.IsDBNull(dataReader["IDPOS"]) ? 0 : Convert.ToInt32(dataReader["IDPOS"]));
                            pedido.PEDIDO_REF_RESERVA = (Convert.IsDBNull(dataReader["PEDIDO_REF_RESERVA"]) ? "" : Convert.ToString(dataReader["PEDIDO_REF_RESERVA"]));
                            pedido.IDARTICULO = (Convert.IsDBNull(dataReader["IDARTICULO"]) ? 0 : Convert.ToInt32(dataReader["IDARTICULO"]));
                            pedido.MATERIAL = (Convert.IsDBNull(dataReader["MATERIAL"]) ? "" : Convert.ToString(dataReader["MATERIAL"]));
                            pedido.CANTIDAD_PEDIDO_RESERVA = (Convert.IsDBNull(dataReader["CANTIDAD_PEDIDO_RESERVA"]) ? 0.000 : Convert.ToDouble(dataReader["CANTIDAD_PEDIDO_RESERVA"]));
                            pedido.QPEDIDO = (Convert.IsDBNull(dataReader["QPEDIDO"]) ? 0 : Convert.ToDouble(dataReader["QPEDIDO"]));
                            pedido.PRECIO = (Convert.IsDBNull(dataReader["PRECIO"]) ? 0 : Convert.ToDouble(dataReader["PRECIO"]));
                            pedido.PROCESADO_POS = (Convert.IsDBNull(dataReader["PROCESADO_POS"]) ? 0 : Convert.ToInt32(dataReader["PROCESADO_POS"]));
                            pedido.MENSAJE_ERROR_POS = (Convert.IsDBNull(dataReader["MENSAJE_ERROR_POS"]) ? "" : Convert.ToString(dataReader["MENSAJE_ERROR_POS"]));
                            pedido.IDMONEDA = (Convert.IsDBNull(dataReader["IDMONEDA"]) ? 0 : Convert.ToInt32(dataReader["IDMONEDA"]));

                            lstPedidosGenerados.Add(pedido);
                        }
                    }
                }
                return lstPedidosGenerados;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        

    }
}
