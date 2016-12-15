using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazSuministros_RegPosBE : BaseInterfaz_RegPosBE
    {
        #region Variables
        private string _codigo_cesta;
        private string _movimiento_clase_doc;
        private string _pedido_ref_reserva;
        private string _posicion_ped_reserva;
        private string _material;
        private int _idarticulo;
        private string _descripcion_art;
        private DateTime _fecha_entrega;
        private string _centro_coste;
        private decimal _cantidad_pedido_reserva;
        private string _unidad_medida_pedido;
        #endregion

        #region Constructores
        public InterfazSuministros_RegPosBE() {

        }
        #endregion

        #region Propiedades
        public string Codigo_cesta
        {
            get
            {
                return _codigo_cesta;
            }

            set
            {
                _codigo_cesta = value;
            }
        }

        public string Movimiento_clase_doc
        {
            get
            {
                return _movimiento_clase_doc;
            }

            set
            {
                _movimiento_clase_doc = value;
            }
        }

        public string Pedido_ref_reserva
        {
            get
            {
                return _pedido_ref_reserva;
            }

            set
            {
                _pedido_ref_reserva = value;
            }
        }

        public string Posicion_ped_reserva
        {
            get
            {
                return _posicion_ped_reserva;
            }

            set
            {
                _posicion_ped_reserva = value;
            }
        }

        public string Material
        {
            get
            {
                return _material;
            }

            set
            {
                _material = value;
            }
        }

        public DateTime Fecha_entrega
        {
            get
            {
                return _fecha_entrega;
            }

            set
            {
                _fecha_entrega = value;
            }
        }

        public string Centro_coste
        {
            get
            {
                return _centro_coste;
            }

            set
            {
                _centro_coste = value;
            }
        }

        public decimal Cantidad_pedido_reserva
        {
            get
            {
                return _cantidad_pedido_reserva;
            }

            set
            {
                _cantidad_pedido_reserva = value;
            }
        }

        public string Unidad_medida_pedido
        {
            get
            {
                return _unidad_medida_pedido;
            }

            set
            {
                _unidad_medida_pedido = value;
            }
        }

        public int Idarticulo
        {
            get
            {
                return _idarticulo;
            }

            set
            {
                _idarticulo = value;
            }
        }

        public string Descripcion_art
        {
            get
            {
                return _descripcion_art;
            }

            set
            {
                _descripcion_art = value;
            }
        }

        #endregion
    }
}
