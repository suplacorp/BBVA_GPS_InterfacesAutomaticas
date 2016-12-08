using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazSuministros_PedidoBE 
    {
         
        #region Variables
        private int _IDREGINI;
        private int _NRO_PROCESO;
        private string _NOMBRE_FICHERO_DESTINO;
        private string _FECHA_EJECUCION;
        private string _HORA_PROCESO;
        private string _RUTA_FICHERO;
        private string _FECHA_REGISTRO;
        private int _IDCAB;
        private string _CODIGO_CESTA;
        private string _NOMBRE_DESTINATARIO;
        private string _DESC_UNID_OFI_ESTAB;
        private string _CALLE_DIRECCION;
        private string _PROVINCIA;
        private string _REGION_DEPARTAMENTO;
        private string _TELEFONO_CONTACTO;
        private string _USUARIO;
        private string _COLONIA_COMUNA_PLANTA_DISTRITO;
        private string _URGENTE;
        private string _PROCESADO_CAB;
        private string _MENSAJE_ERROR_CAB;
        private int _IDPOS;
        private string _PEDIDO_REF_RESERVA;
        private int _IDARTICULO;
        private string _MATERIAL;
        private double _CANTIDAD_PEDIDO_RESERVA;
        private double _QPEDIDO;
        private double _PRECIO;
        private int _PROCESADO_POS;
        private string _MENSAJE_ERROR_POS;
        private int _IDMONEDA;

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

        public string FECHA_REGISTRO
        {
            get
            {
                return _FECHA_REGISTRO;
            }

            set
            {
                _FECHA_REGISTRO = value;
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

        public string CODIGO_CESTA
        {
            get
            {
                return _CODIGO_CESTA;
            }

            set
            {
                _CODIGO_CESTA = value;
            }
        }

        public string NOMBRE_DESTINATARIO
        {
            get
            {
                return _NOMBRE_DESTINATARIO;
            }

            set
            {
                _NOMBRE_DESTINATARIO = value;
            }
        }

        public string DESC_UNID_OFI_ESTAB
        {
            get
            {
                return _DESC_UNID_OFI_ESTAB;
            }

            set
            {
                _DESC_UNID_OFI_ESTAB = value;
            }
        }

        public string CALLE_DIRECCION
        {
            get
            {
                return _CALLE_DIRECCION;
            }

            set
            {
                _CALLE_DIRECCION = value;
            }
        }

        public string PROVINCIA
        {
            get
            {
                return _PROVINCIA;
            }

            set
            {
                _PROVINCIA = value;
            }
        }

        public string REGION_DEPARTAMENTO
        {
            get
            {
                return _REGION_DEPARTAMENTO;
            }

            set
            {
                _REGION_DEPARTAMENTO = value;
            }
        }

        public string TELEFONO_CONTACTO
        {
            get
            {
                return _TELEFONO_CONTACTO;
            }

            set
            {
                _TELEFONO_CONTACTO = value;
            }
        }

        public string USUARIO
        {
            get
            {
                return _USUARIO;
            }

            set
            {
                _USUARIO = value;
            }
        }

        public string COLONIA_COMUNA_PLANTA_DISTRITO
        {
            get
            {
                return _COLONIA_COMUNA_PLANTA_DISTRITO;
            }

            set
            {
                _COLONIA_COMUNA_PLANTA_DISTRITO = value;
            }
        }

        public string URGENTE
        {
            get
            {
                return _URGENTE;
            }

            set
            {
                _URGENTE = value;
            }
        }

        public string PROCESADO_CAB
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

        public string PEDIDO_REF_RESERVA
        {
            get
            {
                return _PEDIDO_REF_RESERVA;
            }

            set
            {
                _PEDIDO_REF_RESERVA = value;
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

        public string MATERIAL
        {
            get
            {
                return _MATERIAL;
            }

            set
            {
                _MATERIAL = value;
            }
        }

        public double CANTIDAD_PEDIDO_RESERVA
        {
            get
            {
                return _CANTIDAD_PEDIDO_RESERVA;
            }

            set
            {
                _CANTIDAD_PEDIDO_RESERVA = value;
            }
        }

        public double QPEDIDO
        {
            get
            {
                return _QPEDIDO;
            }

            set
            {
                _QPEDIDO = value;
            }
        }

        public double PRECIO
        {
            get
            {
                return _PRECIO;
            }

            set
            {
                _PRECIO = value;
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

        public int IDMONEDA
        {
            get
            {
                return _IDMONEDA;
            }

            set
            {
                _IDMONEDA = value;
            }
        }

        #endregion
    }
}
