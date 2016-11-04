using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazExpediciones_RegPosBE : BaseInterfaz_RegPosBE
    {
        #region Variables
        private string _nro_posic_ped_reserva;
        private string _nro_material;
        private string _unidad_medida_pedido;
        private decimal _cantidad;
        private string _indicador_entrega_final;
        private string _bulto;
        private string _numero_lote;
        #endregion

        #region Constructores
        public InterfazExpediciones_RegPosBE()
        {

        }
        #endregion

        #region Propiedades
        public string Nro_posic_ped_reserva
        {
            get
            {
                return _nro_posic_ped_reserva;
            }

            set
            {
                _nro_posic_ped_reserva = value;
            }
        }

        public string Nro_material
        {
            get
            {
                return _nro_material;
            }

            set
            {
                _nro_material = value;
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

        public decimal Cantidad
        {
            get
            {
                return _cantidad;
            }

            set
            {
                _cantidad = value;
            }
        }

        public string Indicador_entrega_final
        {
            get
            {
                return _indicador_entrega_final;
            }

            set
            {
                _indicador_entrega_final = value;
            }
        }

        public string Bulto
        {
            get
            {
                return _bulto;
            }

            set
            {
                _bulto = value;
            }
        }

        public string Numero_lote
        {
            get
            {
                return _numero_lote;
            }

            set
            {
                _numero_lote = value;
            }
        }

        #endregion
    }
}
