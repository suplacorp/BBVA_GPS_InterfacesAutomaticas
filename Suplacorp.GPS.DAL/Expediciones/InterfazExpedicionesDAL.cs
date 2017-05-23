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
using System.Collections.Specialized;

namespace Suplacorp.GPS.DAL
{
    public class InterfazExpedicionesDAL : BaseDAL
    {
        
        public bool ObtenerExpedicionesGeneradas(ref InterfazExpediciones_RegIniBE intExpediciones){
            bool result = false;

            InterfazExpediciones_ExpedicionBE expedicion_Total;
            List<InterfazExpediciones_ExpedicionBE> lstExpediciones_Total;

            NameValueCollection lstCabeceras;
            InterfazExpediciones_RegCabBE expedicionCabeceraBE;
            InterfazExpediciones_RegPosBE expedicionPosicionBE;
            List<InterfazExpediciones_ExpedicionBE> lstExpediciones_Posiciones_xCabecera;

            try
            {
                using (DbCommand _sqlCmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_SEL_CARGAR_INT_EXPEDICIONES"))
                {
                    sqlDatabase.AddInParameter(_sqlCmd, "IDREGINI", DbType.Int32, intExpediciones.Idregini);
                    sqlDatabase.AddInParameter(_sqlCmd, "IDCLIENTE", DbType.Int32, GlobalVariables.IdCliente);

                    using (IDataReader dataReader = sqlDatabase.ExecuteReader(_sqlCmd))
                    {
                        lstExpediciones_Total = new List<InterfazExpediciones_ExpedicionBE>();
                        while (dataReader.Read())
                        {
                            expedicion_Total = new InterfazExpediciones_ExpedicionBE();

                            expedicion_Total.IDREGINI = (Convert.IsDBNull(dataReader["IDREGINI"]) ? 0 : Convert.ToInt32(dataReader["IDREGINI"]));
                            expedicion_Total.NRO_PROCESO = (Convert.IsDBNull(dataReader["NRO_PROCESO"]) ? 0 : Convert.ToInt32(dataReader["NRO_PROCESO"]));
                            expedicion_Total.NUMERAL_INI = (Convert.IsDBNull(dataReader["NUMERAL_INI"]) ? "" : Convert.ToString(dataReader["NUMERAL_INI"]));
                            expedicion_Total.TIPO_REGISTRO_INI = (Convert.IsDBNull(dataReader["TIPO_REGISTRO_INI"]) ? 0 : Convert.ToInt32(dataReader["TIPO_REGISTRO_INI"]));
                            expedicion_Total.TIPO_INTERFAZ = (Convert.IsDBNull(dataReader["TIPO_INTERFAZ"]) ? "" : Convert.ToString(dataReader["TIPO_INTERFAZ"]));
                            expedicion_Total.PAIS = (Convert.IsDBNull(dataReader["PAIS"]) ? "" : Convert.ToString(dataReader["PAIS"]));
                            expedicion_Total.IDENTIFICADOR_INTERFAZ = (Convert.IsDBNull(dataReader["IDENTIFICADOR_INTERFAZ"]) ? "" : Convert.ToString(dataReader["IDENTIFICADOR_INTERFAZ"]));
                            expedicion_Total.NOMBRE_FICHERO = (Convert.IsDBNull(dataReader["NOMBRE_FICHERO"]) ? "" : Convert.ToString(dataReader["NOMBRE_FICHERO"]));
                            expedicion_Total.NOMBRE_FICHERO_DESTINO = (Convert.IsDBNull(dataReader["NOMBRE_FICHERO_DESTINO"]) ? "" : Convert.ToString(dataReader["NOMBRE_FICHERO_DESTINO"]));
                            expedicion_Total.FECHA_EJECUCION = (Convert.IsDBNull(dataReader["FECHA_EJECUCION"]) ? DateTime.Today : Convert.ToDateTime(dataReader["FECHA_EJECUCION"]));
                            expedicion_Total.HORA_PROCESO = (Convert.IsDBNull(dataReader["HORA_PROCESO"]) ? "" : Convert.ToString(dataReader["HORA_PROCESO"]));
                            expedicion_Total.RUTA_FICHERO = (Convert.IsDBNull(dataReader["RUTA_FICHERO"]) ? "" : Convert.ToString(dataReader["RUTA_FICHERO"]));
                            expedicion_Total.IDINTERFAZ = (Convert.IsDBNull(dataReader["IDINTERFAZ"]) ? 0 : Convert.ToInt32(dataReader["IDINTERFAZ"]));
                            expedicion_Total.IDTIPOREGISTRO = (Convert.IsDBNull(dataReader["IDTIPOREGISTRO"]) ? 0 : Convert.ToInt32(dataReader["IDTIPOREGISTRO"]));
                            expedicion_Total.PROCESADO_INI = (Convert.IsDBNull(dataReader["PROCESADO_INI"]) ? 0 : Convert.ToInt32(dataReader["PROCESADO_INI"]));
                            expedicion_Total.NUMERO_TOTAL_REGISTROS_FIN = (Convert.IsDBNull(dataReader["NUMERO_TOTAL_REGISTROS_FIN"]) ? 0 : Convert.ToInt32(dataReader["NUMERO_TOTAL_REGISTROS_FIN"]));
                            expedicion_Total.TIPO_REGISTRO_FIN = (Convert.IsDBNull(dataReader["TIPO_REGISTRO_FIN"]) ? 0 : Convert.ToInt32(dataReader["TIPO_REGISTRO_FIN"]));
                            expedicion_Total.NUMERO_REGISTROS_CAB_FIN = (Convert.IsDBNull(dataReader["NUMERO_REGISTROS_CAB_FIN"]) ? 0 : Convert.ToInt32(dataReader["NUMERO_REGISTROS_CAB_FIN"]));
                            expedicion_Total.NUMERO_REGISTROS_POS_FIN = (Convert.IsDBNull(dataReader["NUMERO_REGISTROS_POS_FIN"]) ? 0 : Convert.ToInt32(dataReader["NUMERO_REGISTROS_POS_FIN"]));
                            expedicion_Total.NUMERO_REGISTROS_TIPO3_FIN = (Convert.IsDBNull(dataReader["NUMERO_REGISTROS_TIPO3_FIN"]) ? "" : Convert.ToString(dataReader["NUMERO_REGISTROS_TIPO3_FIN"]));
                            expedicion_Total.IDCAB = (Convert.IsDBNull(dataReader["IDCAB"]) ? 0 : Convert.ToInt32(dataReader["IDCAB"]));
                            expedicion_Total.NUMERAL_CAB = (Convert.IsDBNull(dataReader["NUMERAL_CAB"]) ? 0 : Convert.ToInt32(dataReader["NUMERAL_CAB"]));
                            expedicion_Total.TIPO_REGISTRO_CAB = (Convert.IsDBNull(dataReader["TIPO_REGISTRO_CAB"]) ? 0 : Convert.ToInt32(dataReader["TIPO_REGISTRO_CAB"]));
                            expedicion_Total.TIPO_MOVIMIENTO = (Convert.IsDBNull(dataReader["TIPO_MOVIMIENTO"]) ? "" : Convert.ToString(dataReader["TIPO_MOVIMIENTO"]));
                            expedicion_Total.TIPO_EXPEDICION = (Convert.IsDBNull(dataReader["TIPO_EXPEDICION"]) ? "" : Convert.ToString(dataReader["TIPO_EXPEDICION"]));
                            expedicion_Total.NRO_DOC_COMPRAS_RESERVA = (Convert.IsDBNull(dataReader["NRO_DOC_COMPRAS_RESERVA"]) ? "" : Convert.ToString(dataReader["NRO_DOC_COMPRAS_RESERVA"]));
                            expedicion_Total.FECHA_CONTABILIZACION = (Convert.IsDBNull(dataReader["FECHA_CONTABILIZACION"]) ? DateTime.Today : Convert.ToDateTime(dataReader["FECHA_CONTABILIZACION"]));
                            expedicion_Total.NUMERO_CESTA = (Convert.IsDBNull(dataReader["NUMERO_CESTA"]) ? "" : Convert.ToString(dataReader["NUMERO_CESTA"]));
                            expedicion_Total.TEXTO_CABECERA_DOCUMENTO = (Convert.IsDBNull(dataReader["TEXTO_CABECERA_DOCUMENTO"]) ? "" : Convert.ToString(dataReader["TEXTO_CABECERA_DOCUMENTO"]));
                            expedicion_Total.PROCESADO_CAB = (Convert.IsDBNull(dataReader["PROCESADO_CAB"]) ? 0 : Convert.ToInt32(dataReader["PROCESADO_CAB"]));
                            expedicion_Total.MENSAJE_ERROR_CAB = (Convert.IsDBNull(dataReader["MENSAJE_ERROR_CAB"]) ? "" : Convert.ToString(dataReader["MENSAJE_ERROR_CAB"]));
                            expedicion_Total.IDPOS = (Convert.IsDBNull(dataReader["IDPOS"]) ? 0 : Convert.ToInt32(dataReader["IDPOS"]));
                            expedicion_Total.NUMERAL_POS = (Convert.IsDBNull(dataReader["NUMERAL_POS"]) ? 0 : Convert.ToInt32(dataReader["NUMERAL_POS"]));
                            expedicion_Total.TIPO_REGISTRO_POS = (Convert.IsDBNull(dataReader["TIPO_REGISTRO_POS"]) ? 0 : Convert.ToInt32(dataReader["TIPO_REGISTRO_POS"]));
                            expedicion_Total.NRO_POSIC_PED_RESERVA = (Convert.IsDBNull(dataReader["NRO_POSIC_PED_RESERVA"]) ? "" : Convert.ToString(dataReader["NRO_POSIC_PED_RESERVA"]));
                            expedicion_Total.NRO_MATERIAL = (Convert.IsDBNull(dataReader["NRO_MATERIAL"]) ? "" : Convert.ToString(dataReader["NRO_MATERIAL"]));
                            expedicion_Total.IDARTICULO = (Convert.IsDBNull(dataReader["IDARTICULO"]) ? 0 : Convert.ToInt32(dataReader["IDARTICULO"]));
                            expedicion_Total.DESCRIPCION_ART = (Convert.IsDBNull(dataReader["DESCRIPCION_ART"]) ? "" : Convert.ToString(dataReader["DESCRIPCION_ART"]));
                            expedicion_Total.UNIDAD_MEDIDA_PEDIDO = (Convert.IsDBNull(dataReader["UNIDAD_MEDIDA_PEDIDO"]) ? "" : Convert.ToString(dataReader["UNIDAD_MEDIDA_PEDIDO"]));
                            expedicion_Total.CANTIDAD = (Convert.IsDBNull(dataReader["CANTIDAD"]) ? 0 : Convert.ToDecimal(dataReader["CANTIDAD"]));
                            expedicion_Total.INDICADOR_ENTREGA_FINAL = (Convert.IsDBNull(dataReader["INDICADOR_ENTREGA_FINAL"]) ? "" : Convert.ToString(dataReader["INDICADOR_ENTREGA_FINAL"]));
                            expedicion_Total.BULTO = (Convert.IsDBNull(dataReader["BULTO"]) ? "" : Convert.ToString(dataReader["BULTO"]));
                            expedicion_Total.NUMERO_LOTE = (Convert.IsDBNull(dataReader["NUMERO_LOTE"]) ? "" : Convert.ToString(dataReader["NUMERO_LOTE"]));
                            expedicion_Total.PROCESADO_POS = (Convert.IsDBNull(dataReader["PROCESADO_POS"]) ? 0 : Convert.ToInt32(dataReader["PROCESADO_POS"]));
                            expedicion_Total.MENSAJE_ERROR_POS = (Convert.IsDBNull(dataReader["MENSAJE_ERROR_POS"]) ? "" : Convert.ToString(dataReader["MENSAJE_ERROR_POS"]));

                            lstExpediciones_Total.Add(expedicion_Total);
                        }
                    }
                }

                /* LLENAR LAS ENTIDADES CORRECTAMENTE */
                if (lstExpediciones_Total.Count > 0)
                {
                    lstCabeceras = new NameValueCollection();

                    /*LLENANGO LA ENTIDAD DE LA INTERFAZ DE EXPEDICIONES - [REGISTRO INICIAL] */
                    intExpediciones.Nro_proceso = lstExpediciones_Total[0].NRO_PROCESO;
                    intExpediciones.Numeral = lstExpediciones_Total[0].NUMERAL_INI;
                    intExpediciones.Tipo_registro = lstExpediciones_Total[0].TIPO_REGISTRO_INI.ToString();
                    intExpediciones.Tipo_interfaz = lstExpediciones_Total[0].TIPO_INTERFAZ;
                    intExpediciones.Pais = lstExpediciones_Total[0].PAIS;
                    intExpediciones.Identificador_interfaz = lstExpediciones_Total[0].IDENTIFICADOR_INTERFAZ;
                    intExpediciones.Nombre_fichero = lstExpediciones_Total[0].NOMBRE_FICHERO;
                    intExpediciones.Nombre_fichero_detino = lstExpediciones_Total[0].NOMBRE_FICHERO_DESTINO;
                    intExpediciones.Fecha_ejecucion = lstExpediciones_Total[0].FECHA_EJECUCION;
                    intExpediciones.Hora_proceso = lstExpediciones_Total[0].HORA_PROCESO;
                    intExpediciones.Ruta_fichero_detino = lstExpediciones_Total[0].RUTA_FICHERO;
                    intExpediciones.Interfaz.Idinterface = lstExpediciones_Total[0].IDINTERFAZ;
                    intExpediciones.Tiporegistro.Idtiporegistro = lstExpediciones_Total[0].IDTIPOREGISTRO;
                    intExpediciones.Procesado = lstExpediciones_Total[0].PROCESADO_INI;
                    /*LLENANGO LA ENTIDAD DE LA INTERFAZ DE EXPEDICIONES - [REGISTRO FIN] */
                    intExpediciones.Numero_total_registros_fin = lstExpediciones_Total[0].NUMERO_TOTAL_REGISTROS_FIN.ToString();
                    intExpediciones.Tipo_registro_fin = lstExpediciones_Total[0].TIPO_REGISTRO_FIN.ToString();
                    intExpediciones.Numero_registros_cab_fin = lstExpediciones_Total[0].NUMERO_REGISTROS_CAB_FIN.ToString();
                    intExpediciones.Numero_registros_pos_fin = lstExpediciones_Total[0].NUMERO_REGISTROS_POS_FIN.ToString();
                    intExpediciones.Numero_registros_tipo3_fin = lstExpediciones_Total[0].NUMERO_REGISTROS_TIPO3_FIN;

                    /* OBTENIENDO TODAS LAS CABECERAS ENCONTRADAS */ /* NameValueCollection[KEY,"VACÍO"] */
                    foreach (InterfazExpediciones_ExpedicionBE cab in lstExpediciones_Total){
                        lstCabeceras.Add(cab.IDCAB.ToString(), "");
                    }

                    /* RECORRIENDO CADA CABECERA */
                    foreach (var idcab in lstCabeceras)
                    {
                        /* OBTENIENDO TODAS LAS "POSICIONES" DE LA CABECERA ACTUAL */
                        lstExpediciones_Posiciones_xCabecera = new List<InterfazExpediciones_ExpedicionBE>();
                        lstExpediciones_Posiciones_xCabecera = lstExpediciones_Total.FindAll(x => x.IDCAB.ToString() == idcab.ToString());
                        expedicionCabeceraBE = new InterfazExpediciones_RegCabBE();

                        expedicionCabeceraBE.Idregini = intExpediciones.Idregini;
                        expedicionCabeceraBE.Idcab = Convert.ToInt32(idcab);
                        expedicionCabeceraBE.Numeral = lstExpediciones_Posiciones_xCabecera[0].NUMERAL_CAB.ToString();
                        expedicionCabeceraBE.Tipo_registro = lstExpediciones_Posiciones_xCabecera[0].TIPO_REGISTRO_CAB.ToString();
                        expedicionCabeceraBE.Tipo_movimiento = lstExpediciones_Posiciones_xCabecera[0].TIPO_MOVIMIENTO;
                        expedicionCabeceraBE.Tipo_expedicion = lstExpediciones_Posiciones_xCabecera[0].TIPO_EXPEDICION;
                        expedicionCabeceraBE.Nro_doc_compras_reserva = lstExpediciones_Posiciones_xCabecera[0].NRO_DOC_COMPRAS_RESERVA;
                        expedicionCabeceraBE.Fecha_contabilizacion = lstExpediciones_Posiciones_xCabecera[0].FECHA_CONTABILIZACION;
                        expedicionCabeceraBE.Numero_cesta = lstExpediciones_Posiciones_xCabecera[0].NUMERO_CESTA;
                        expedicionCabeceraBE.Texto_cabecera_documento = lstExpediciones_Posiciones_xCabecera[0].TEXTO_CABECERA_DOCUMENTO;
                        expedicionCabeceraBE.Procesado = Convert.ToBoolean(lstExpediciones_Posiciones_xCabecera[0].PROCESADO_CAB);
                        intExpediciones.LstInterfazExpediciones_RegCabBE.Add(expedicionCabeceraBE);

                        /* RECORRIENDO [TODAS LAS POSICIONES DE] LA CABECERA ACTUAL */
                        foreach (var pos in lstExpediciones_Posiciones_xCabecera)
                        {
                            expedicionPosicionBE = new InterfazExpediciones_RegPosBE();

                            expedicionPosicionBE.Idcab = Convert.ToInt32(idcab);
                            expedicionPosicionBE.Idpos = pos.IDPOS;
                            expedicionPosicionBE.Numeral = pos.NUMERAL_POS.ToString();
                            expedicionPosicionBE.Tipo_registro = pos.TIPO_REGISTRO_POS.ToString();
                            expedicionPosicionBE.Nro_posic_ped_reserva = pos.NRO_POSIC_PED_RESERVA;
                            expedicionPosicionBE.Nro_material = pos.NRO_MATERIAL;
                            expedicionPosicionBE.Idarticulo = pos.IDARTICULO;
                            expedicionPosicionBE.Descripcion_art = pos.DESCRIPCION_ART;
                            expedicionPosicionBE.Unidad_medida_pedido = pos.UNIDAD_MEDIDA_PEDIDO;
                            expedicionPosicionBE.Cantidad = pos.CANTIDAD;
                            expedicionPosicionBE.Indicador_entrega_final = pos.INDICADOR_ENTREGA_FINAL;
                            expedicionPosicionBE.Bulto = pos.BULTO;
                            expedicionPosicionBE.Numero_lote = pos.NUMERO_LOTE;
                            expedicionPosicionBE.Procesado = pos.PROCESADO_POS;
                            intExpediciones.LstInterfazExpediciones_RegCabBE.Find(x => x.Idcab == Convert.ToInt32(idcab)).LstInterfazExpediciones_RegPosBE.Add(expedicionPosicionBE);
                        }
                    }

                    result = true;
                }
            }
            catch{
                throw;
            }
            return result;
        }

        public string GenerarInterfazExpediciones(string nombre_fichero_destino)
        {
            string result = "";

            try
            {
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_INS_REGISTRAR_IEXP");
                cmd.CommandTimeout = 1200; /* 20 minutos */
                sqlDatabase.AddInParameter(cmd, "NOMBRE_FICHERO_DESTINO", DbType.String, nombre_fichero_destino);
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
