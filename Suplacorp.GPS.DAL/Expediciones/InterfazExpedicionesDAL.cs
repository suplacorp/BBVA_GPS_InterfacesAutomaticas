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
    public class InterfazExpedicionesDAL : BaseDAL
    {

        public List<InterfazExpediciones_ExpedicionBE> ObtenerExpedicionesGeneradas_Log(int idregini){

            List<InterfazExpediciones_ExpedicionBE> lstExpediciones = new List<InterfazExpediciones_ExpedicionBE>();
            InterfazExpediciones_ExpedicionBE expedicion;

            try{
                using (DbCommand _sqlCmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_SEL_CARGAR_INT_EXPEDICIONES"))
                {
                    sqlDatabase.AddInParameter(_sqlCmd, "IDREGINI", DbType.Int32, idregini);
                    sqlDatabase.AddInParameter(_sqlCmd, "IDCLIENTE", DbType.Int32, GlobalVariables.IdCliente);

                    using (IDataReader dataReader = sqlDatabase.ExecuteReader(_sqlCmd))
                    {
                        while (dataReader.Read())
                        {
                            expedicion = new InterfazExpediciones_ExpedicionBE();

                            expedicion.IDREGINI = (Convert.IsDBNull(dataReader["IDREGINI"]) ? 0 : Convert.ToInt32(dataReader["IDREGINI"]));
                            expedicion.NRO_PROCESO = (Convert.IsDBNull(dataReader["NRO_PROCESO"]) ? 0 : Convert.ToInt32(dataReader["NRO_PROCESO"]));
                            expedicion.NUMERAL_INI = (Convert.IsDBNull(dataReader["NUMERAL_INI"]) ? "" : Convert.ToString(dataReader["NUMERAL_INI"]));
                            expedicion.TIPO_REGISTRO_INI = (Convert.IsDBNull(dataReader["TIPO_REGISTRO_INI"]) ? 0 : Convert.ToInt32(dataReader["TIPO_REGISTRO_INI"]));
                            expedicion.TIPO_INTERFAZ = (Convert.IsDBNull(dataReader["TIPO_INTERFAZ"]) ? "" : Convert.ToString(dataReader["TIPO_INTERFAZ"]));
                            expedicion.PAIS = (Convert.IsDBNull(dataReader["PAIS"]) ? "" : Convert.ToString(dataReader["PAIS"]));
                            expedicion.IDENTIFICADOR_INTERFAZ = (Convert.IsDBNull(dataReader["IDENTIFICADOR_INTERFAZ"]) ? "" : Convert.ToString(dataReader["IDENTIFICADOR_INTERFAZ"]));
                            expedicion.NOMBRE_FICHERO = (Convert.IsDBNull(dataReader["NOMBRE_FICHERO"]) ? "" : Convert.ToString(dataReader["NOMBRE_FICHERO"]));
                            expedicion.NOMBRE_FICHERO_DESTINO = (Convert.IsDBNull(dataReader["NOMBRE_FICHERO_DESTINO"]) ? "" : Convert.ToString(dataReader["NOMBRE_FICHERO_DESTINO"]));
                            expedicion.FECHA_EJECUCION = (Convert.IsDBNull(dataReader["FECHA_EJECUCION"]) ? "" : Convert.ToString(dataReader["FECHA_EJECUCION"]));
                            expedicion.HORA_PROCESO = (Convert.IsDBNull(dataReader["HORA_PROCESO"]) ? "" : Convert.ToString(dataReader["HORA_PROCESO"]));
                            expedicion.RUTA_FICHERO = (Convert.IsDBNull(dataReader["RUTA_FICHERO"]) ? "" : Convert.ToString(dataReader["RUTA_FICHERO"]));
                            expedicion.IDINTERFAZ = (Convert.IsDBNull(dataReader["IDINTERFAZ"]) ? 0 : Convert.ToInt32(dataReader["IDINTERFAZ"]));
                            expedicion.IDTIPOREGISTRO = (Convert.IsDBNull(dataReader["IDTIPOREGISTRO"]) ? 0 : Convert.ToInt32(dataReader["IDTIPOREGISTRO"]));
                            expedicion.PROCESADO_INI = (Convert.IsDBNull(dataReader["PROCESADO_INI"]) ? 0 : Convert.ToInt32(dataReader["PROCESADO_INI"]));
                            expedicion.NUMERO_TOTAL_REGISTROS_FIN = (Convert.IsDBNull(dataReader["NUMERO_TOTAL_REGISTROS_FIN"]) ? 0 : Convert.ToInt32(dataReader["NUMERO_TOTAL_REGISTROS_FIN"]));
                            expedicion.TIPO_REGISTRO_FIN = (Convert.IsDBNull(dataReader["TIPO_REGISTRO_FIN"]) ? 0 : Convert.ToInt32(dataReader["TIPO_REGISTRO_FIN"]));
                            expedicion.NUMERO_REGISTROS_CAB_FIN = (Convert.IsDBNull(dataReader["NUMERO_REGISTROS_CAB_FIN"]) ? 0 : Convert.ToInt32(dataReader["NUMERO_REGISTROS_CAB_FIN"]));
                            expedicion.NUMERO_REGISTROS_POS_FIN = (Convert.IsDBNull(dataReader["NUMERO_REGISTROS_POS_FIN"]) ? 0 : Convert.ToInt32(dataReader["NUMERO_REGISTROS_POS_FIN"]));
                            expedicion.NUMERO_REGISTROS_TIPO3_FIN = (Convert.IsDBNull(dataReader["NUMERO_REGISTROS_TIPO3_FIN"]) ? "" : Convert.ToString(dataReader["NUMERO_REGISTROS_TIPO3_FIN"]));
                            expedicion.IDCAB = (Convert.IsDBNull(dataReader["IDCAB"]) ? 0 : Convert.ToInt32(dataReader["IDCAB"]));
                            expedicion.NUMERAL_CAB = (Convert.IsDBNull(dataReader["NUMERAL_CAB"]) ? 0 : Convert.ToInt32(dataReader["NUMERAL_CAB"]));
                            expedicion.TIPO_REGISTRO_CAB = (Convert.IsDBNull(dataReader["TIPO_REGISTRO_CAB"]) ? 0 : Convert.ToInt32(dataReader["TIPO_REGISTRO_CAB"]));
                            expedicion.TIPO_MOVIMIENTO = (Convert.IsDBNull(dataReader["TIPO_MOVIMIENTO"]) ? "" : Convert.ToString(dataReader["TIPO_MOVIMIENTO"]));
                            expedicion.TIPO_EXPEDICION = (Convert.IsDBNull(dataReader["TIPO_EXPEDICION"]) ? "" : Convert.ToString(dataReader["TIPO_EXPEDICION"]));
                            expedicion.NRO_DOC_COMPRAS_RESERVA = (Convert.IsDBNull(dataReader["NRO_DOC_COMPRAS_RESERVA"]) ? "" : Convert.ToString(dataReader["NRO_DOC_COMPRAS_RESERVA"]));
                            expedicion.FECHA_CONTABILIZACION = (Convert.IsDBNull(dataReader["FECHA_CONTABILIZACION"]) ? "" : Convert.ToString(dataReader["FECHA_CONTABILIZACION"]));
                            expedicion.NUMERO_CESTA = (Convert.IsDBNull(dataReader["NUMERO_CESTA"]) ? "" : Convert.ToString(dataReader["NUMERO_CESTA"]));
                            expedicion.TEXTO_CABECERA_DOCUMENTO = (Convert.IsDBNull(dataReader["TEXTO_CABECERA_DOCUMENTO"]) ? "" : Convert.ToString(dataReader["TEXTO_CABECERA_DOCUMENTO"]));
                            expedicion.PROCESADO_CAB = (Convert.IsDBNull(dataReader["PROCESADO_CAB"]) ? 0 : Convert.ToInt32(dataReader["PROCESADO_CAB"]));
                            expedicion.MENSAJE_ERROR_CAB = (Convert.IsDBNull(dataReader["MENSAJE_ERROR_CAB"]) ? "" : Convert.ToString(dataReader["MENSAJE_ERROR_CAB"]));
                            expedicion.IDPOS = (Convert.IsDBNull(dataReader["IDPOS"]) ? 0 : Convert.ToInt32(dataReader["IDPOS"]));
                            expedicion.NUMERAL_POS = (Convert.IsDBNull(dataReader["NUMERAL_POS"]) ? 0 : Convert.ToInt32(dataReader["NUMERAL_POS"]));
                            expedicion.TIPO_REGISTRO_POS = (Convert.IsDBNull(dataReader["TIPO_REGISTRO_POS"]) ? 0 : Convert.ToInt32(dataReader["TIPO_REGISTRO_POS"]));
                            expedicion.NRO_POSIC_PED_RESERVA = (Convert.IsDBNull(dataReader["NRO_POSIC_PED_RESERVA"]) ? "" : Convert.ToString(dataReader["NRO_POSIC_PED_RESERVA"]));
                            expedicion.NRO_MATERIAL = (Convert.IsDBNull(dataReader["NRO_MATERIAL"]) ? "" : Convert.ToString(dataReader["NRO_MATERIAL"]));
                            expedicion.IDARTICULO = (Convert.IsDBNull(dataReader["IDARTICULO"]) ? 0 : Convert.ToInt32(dataReader["IDARTICULO"]));
                            expedicion.DESCRIPCION_ART = (Convert.IsDBNull(dataReader["DESCRIPCION_ART"]) ? "" : Convert.ToString(dataReader["DESCRIPCION_ART"]));
                            expedicion.UNIDAD_MEDIDA_PEDIDO = (Convert.IsDBNull(dataReader["UNIDAD_MEDIDA_PEDIDO"]) ? "" : Convert.ToString(dataReader["UNIDAD_MEDIDA_PEDIDO"]));
                            expedicion.CANTIDAD = (Convert.IsDBNull(dataReader["CANTIDAD"]) ? 0 : Convert.ToDouble(dataReader["CANTIDAD"]));
                            expedicion.INDICADOR_ENTREGA_FINAL = (Convert.IsDBNull(dataReader["INDICADOR_ENTREGA_FINAL"]) ? "" : Convert.ToString(dataReader["INDICADOR_ENTREGA_FINAL"]));
                            expedicion.BULTO = (Convert.IsDBNull(dataReader["BULTO"]) ? "" : Convert.ToString(dataReader["BULTO"]));
                            expedicion.NUMERO_LOTE = (Convert.IsDBNull(dataReader["NUMERO_LOTE"]) ? "" : Convert.ToString(dataReader["NUMERO_LOTE"]));
                            expedicion.PROCESADO_POS = (Convert.IsDBNull(dataReader["PROCESADO_POS"]) ? 0 : Convert.ToInt32(dataReader["PROCESADO_POS"]));
                            expedicion.MENSAJE_ERROR_POS = (Convert.IsDBNull(dataReader["MENSAJE_ERROR_POS"]) ? "" : Convert.ToString(dataReader["MENSAJE_ERROR_POS"]));

                            lstExpediciones.Add(expedicion);
                        }
                    }
                }
                return lstExpediciones;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GenerarInterfazExpediciones(string nombre_fichero_destino)
        {
            string result = "";

            try
            {
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_INS_REGISTRAR_IEXP");

                sqlDatabase.AddInParameter(cmd, "NOMBRE_FICHERO_DESTINO", DbType.String, nombre_fichero_destino);

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
