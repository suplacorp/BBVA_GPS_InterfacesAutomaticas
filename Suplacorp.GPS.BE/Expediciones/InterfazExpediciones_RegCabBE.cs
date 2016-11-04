using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazExpediciones_RegCabBE : BaseInterfaz_RegCabBE
    {
        #region Variables
        private string _tipo_movimiento;
        private string _tipo_expedicion;
        private string _nro_doc_compras_reserva;
        private DateTime _fecha_contabilizacion;
        private string _numero_cesta;
        private string _texto_cabecera_documento;
        private List<InterfazExpediciones_RegPosBE> _lstInterfazExpediciones_RegPosBE;
        #endregion

        #region Constructores
        public InterfazExpediciones_RegCabBE(){
            _lstInterfazExpediciones_RegPosBE = new List<InterfazExpediciones_RegPosBE>();
        }
        #endregion

        #region Propiedades
        public string Tipo_movimiento
        {
            get
            {
                return _tipo_movimiento;
            }

            set
            {
                _tipo_movimiento = value;
            }
        }

        public string Tipo_expedicion
        {
            get
            {
                return _tipo_expedicion;
            }

            set
            {
                _tipo_expedicion = value;
            }
        }

        public string Nro_doc_compras_reserva
        {
            get
            {
                return _nro_doc_compras_reserva;
            }

            set
            {
                _nro_doc_compras_reserva = value;
            }
        }

        public DateTime Fecha_contabilizacion
        {
            get
            {
                return _fecha_contabilizacion;
            }

            set
            {
                _fecha_contabilizacion = value;
            }
        }

        public string Numero_cesta
        {
            get
            {
                return _numero_cesta;
            }

            set
            {
                _numero_cesta = value;
            }
        }

        public string Texto_cabecera_documento
        {
            get
            {
                return _texto_cabecera_documento;
            }

            set
            {
                _texto_cabecera_documento = value;
            }
        }

        public List<InterfazExpediciones_RegPosBE> LstInterfazExpediciones_RegPosBE
        {
            get
            {
                return _lstInterfazExpediciones_RegPosBE;
            }

            set
            {
                _lstInterfazExpediciones_RegPosBE = value;
            }
        }
        #endregion
    }
}
