using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazExpediciones_ExpedicionBE
    {
        #region Variables

        private int _IDREGINI;
        private int _NRO_PROCESO;
        private string _NUMERAL_INI;
        private int _TIPO_REGISTRO_INI;
        private string _TIPO_INTERFAZ;
        private string _PAIS;
        private string _IDENTIFICADOR_INTERFAZ;
        private string _NOMBRE_FICHERO;
        private string _NOMBRE_FICHERO_DESTINO;
        private string _FECHA_EJECUCION;
        private string _HORA_PROCESO;
        private string _RUTA_FICHERO;
        private int _IDINTERFAZ;
        private int _IDTIPOREGISTRO;
        private int _PROCESADO_INI;
        private int _NUMERO_TOTAL_REGISTROS_FIN;
        private int _TIPO_REGISTRO_FIN;
        private int _NUMERO_REGISTROS_CAB_FIN;
        private int _NUMERO_REGISTROS_POS_FIN;
        private string _NUMERO_REGISTROS_TIPO3_FIN;
        private int _IDCAB;
        private int _NUMERAL_CAB;
        private int _TIPO_REGISTRO_CAB;
        private string _TIPO_MOVIMIENTO;
        private string _TIPO_EXPEDICION;
        private string _NRO_DOC_COMPRAS_RESERVA;
        private string _FECHA_CONTABILIZACION;
        private string _NUMERO_CESTA;
        private string _TEXTO_CABECERA_DOCUMENTO;
        private int _PROCESADO_CAB;
        private string _MENSAJE_ERROR_CAB;
        private int _IDPOS;
        private int _NUMERAL_POS;
        private int _TIPO_REGISTRO_POS;
        private string _NRO_POSIC_PED_RESERVA;
        private string _NRO_MATERIAL;
        private int _IDARTICULO;
        private string _DESCRIPCION_ART;
        private string _UNIDAD_MEDIDA_PEDIDO;
        private double _CANTIDAD;
        private string _INDICADOR_ENTREGA_FINAL;
        private string _BULTO;
        private string _NUMERO_LOTE;
        private int _PROCESADO_POS;
        private string _MENSAJE_ERROR_POS;

        #endregion

        #region Propiedades
        public int IDREGINI
        {
            get
            {
                return _IDREGINI;
            }

            set
            {
                _IDREGINI = value;
            }
        }

        public int NRO_PROCESO
        {
            get
            {
                return _NRO_PROCESO;
            }

            set
            {
                _NRO_PROCESO = value;
            }
        }

        public string NUMERAL_INI
        {
            get
            {
                return _NUMERAL_INI;
            }

            set
            {
                _NUMERAL_INI = value;
            }
        }

        public int TIPO_REGISTRO_INI
        {
            get
            {
                return _TIPO_REGISTRO_INI;
            }

            set
            {
                _TIPO_REGISTRO_INI = value;
            }
        }

        public string TIPO_INTERFAZ
        {
            get
            {
                return _TIPO_INTERFAZ;
            }

            set
            {
                _TIPO_INTERFAZ = value;
            }
        }

        public string PAIS
        {
            get
            {
                return _PAIS;
            }

            set
            {
                _PAIS = value;
            }
        }

        public string IDENTIFICADOR_INTERFAZ
        {
            get
            {
                return _IDENTIFICADOR_INTERFAZ;
            }

            set
            {
                _IDENTIFICADOR_INTERFAZ = value;
            }
        }

        public string NOMBRE_FICHERO
        {
            get
            {
                return _NOMBRE_FICHERO;
            }

            set
            {
                _NOMBRE_FICHERO = value;
            }
        }

        public string NOMBRE_FICHERO_DESTINO
        {
            get
            {
                return _NOMBRE_FICHERO_DESTINO;
            }

            set
            {
                _NOMBRE_FICHERO_DESTINO = value;
            }
        }

        public string FECHA_EJECUCION
        {
            get
            {
                return _FECHA_EJECUCION;
            }

            set
            {
                _FECHA_EJECUCION = value;
            }
        }

        public string HORA_PROCESO
        {
            get
            {
                return _HORA_PROCESO;
            }

            set
            {
                _HORA_PROCESO = value;
            }
        }

        public string RUTA_FICHERO
        {
            get
            {
                return _RUTA_FICHERO;
            }

            set
            {
                _RUTA_FICHERO = value;
            }
        }

        public int IDINTERFAZ
        {
            get
            {
                return _IDINTERFAZ;
            }

            set
            {
                _IDINTERFAZ = value;
            }
        }

        public int IDTIPOREGISTRO
        {
            get
            {
                return _IDTIPOREGISTRO;
            }

            set
            {
                _IDTIPOREGISTRO = value;
            }
        }

        public int PROCESADO_INI
        {
            get
            {
                return _PROCESADO_INI;
            }

            set
            {
                _PROCESADO_INI = value;
            }
        }

        public int NUMERO_TOTAL_REGISTROS_FIN
        {
            get
            {
                return _NUMERO_TOTAL_REGISTROS_FIN;
            }

            set
            {
                _NUMERO_TOTAL_REGISTROS_FIN = value;
            }
        }

        public int TIPO_REGISTRO_FIN
        {
            get
            {
                return _TIPO_REGISTRO_FIN;
            }

            set
            {
                _TIPO_REGISTRO_FIN = value;
            }
        }

        public int NUMERO_REGISTROS_CAB_FIN
        {
            get
            {
                return _NUMERO_REGISTROS_CAB_FIN;
            }

            set
            {
                _NUMERO_REGISTROS_CAB_FIN = value;
            }
        }

        public int NUMERO_REGISTROS_POS_FIN
        {
            get
            {
                return _NUMERO_REGISTROS_POS_FIN;
            }

            set
            {
                _NUMERO_REGISTROS_POS_FIN = value;
            }
        }

        public string NUMERO_REGISTROS_TIPO3_FIN
        {
            get
            {
                return _NUMERO_REGISTROS_TIPO3_FIN;
            }

            set
            {
                _NUMERO_REGISTROS_TIPO3_FIN = value;
            }
        }

        public int IDCAB
        {
            get
            {
                return _IDCAB;
            }

            set
            {
                _IDCAB = value;
            }
        }

        public int NUMERAL_CAB
        {
            get
            {
                return _NUMERAL_CAB;
            }

            set
            {
                _NUMERAL_CAB = value;
            }
        }

        public int TIPO_REGISTRO_CAB
        {
            get
            {
                return _TIPO_REGISTRO_CAB;
            }

            set
            {
                _TIPO_REGISTRO_CAB = value;
            }
        }

        public string TIPO_MOVIMIENTO
        {
            get
            {
                return _TIPO_MOVIMIENTO;
            }

            set
            {
                _TIPO_MOVIMIENTO = value;
            }
        }

        public string TIPO_EXPEDICION
        {
            get
            {
                return _TIPO_EXPEDICION;
            }

            set
            {
                _TIPO_EXPEDICION = value;
            }
        }

        public string NRO_DOC_COMPRAS_RESERVA
        {
            get
            {
                return _NRO_DOC_COMPRAS_RESERVA;
            }

            set
            {
                _NRO_DOC_COMPRAS_RESERVA = value;
            }
        }

        public string FECHA_CONTABILIZACION
        {
            get
            {
                return _FECHA_CONTABILIZACION;
            }

            set
            {
                _FECHA_CONTABILIZACION = value;
            }
        }

        public string NUMERO_CESTA
        {
            get
            {
                return _NUMERO_CESTA;
            }

            set
            {
                _NUMERO_CESTA = value;
            }
        }

        public string TEXTO_CABECERA_DOCUMENTO
        {
            get
            {
                return _TEXTO_CABECERA_DOCUMENTO;
            }

            set
            {
                _TEXTO_CABECERA_DOCUMENTO = value;
            }
        }

        public int PROCESADO_CAB
        {
            get
            {
                return _PROCESADO_CAB;
            }

            set
            {
                _PROCESADO_CAB = value;
            }
        }

        public string MENSAJE_ERROR_CAB
        {
            get
            {
                return _MENSAJE_ERROR_CAB;
            }

            set
            {
                _MENSAJE_ERROR_CAB = value;
            }
        }

        public int IDPOS
        {
            get
            {
                return _IDPOS;
            }

            set
            {
                _IDPOS = value;
            }
        }

        public int NUMERAL_POS
        {
            get
            {
                return _NUMERAL_POS;
            }

            set
            {
                _NUMERAL_POS = value;
            }
        }

        public int TIPO_REGISTRO_POS
        {
            get
            {
                return _TIPO_REGISTRO_POS;
            }

            set
            {
                _TIPO_REGISTRO_POS = value;
            }
        }

        public string NRO_POSIC_PED_RESERVA
        {
            get
            {
                return _NRO_POSIC_PED_RESERVA;
            }

            set
            {
                _NRO_POSIC_PED_RESERVA = value;
            }
        }

        public string NRO_MATERIAL
        {
            get
            {
                return _NRO_MATERIAL;
            }

            set
            {
                _NRO_MATERIAL = value;
            }
        }

        public int IDARTICULO
        {
            get
            {
                return _IDARTICULO;
            }

            set
            {
                _IDARTICULO = value;
            }
        }

        public string DESCRIPCION_ART
        {
            get
            {
                return _DESCRIPCION_ART;
            }

            set
            {
                _DESCRIPCION_ART = value;
            }
        }

        public string UNIDAD_MEDIDA_PEDIDO
        {
            get
            {
                return _UNIDAD_MEDIDA_PEDIDO;
            }

            set
            {
                _UNIDAD_MEDIDA_PEDIDO = value;
            }
        }

        public double CANTIDAD
        {
            get
            {
                return _CANTIDAD;
            }

            set
            {
                _CANTIDAD = value;
            }
        }

        public string INDICADOR_ENTREGA_FINAL
        {
            get
            {
                return _INDICADOR_ENTREGA_FINAL;
            }

            set
            {
                _INDICADOR_ENTREGA_FINAL = value;
            }
        }

        public string BULTO
        {
            get
            {
                return _BULTO;
            }

            set
            {
                _BULTO = value;
            }
        }

        public string NUMERO_LOTE
        {
            get
            {
                return _NUMERO_LOTE;
            }

            set
            {
                _NUMERO_LOTE = value;
            }
        }

        public int PROCESADO_POS
        {
            get
            {
                return _PROCESADO_POS;
            }

            set
            {
                _PROCESADO_POS = value;
            }
        }

        public string MENSAJE_ERROR_POS
        {
            get
            {
                return _MENSAJE_ERROR_POS;
            }

            set
            {
                _MENSAJE_ERROR_POS = value;
            }
        }

        #endregion
    }
}
