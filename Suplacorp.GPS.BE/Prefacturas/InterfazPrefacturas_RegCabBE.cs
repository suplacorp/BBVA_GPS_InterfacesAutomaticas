using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazPrefacturas_RegCabBE : BaseInterfaz_RegCabBE
    {
        #region Variables
        private string _numero_documento_preliminar;
        private string _sociedad;
        private string _clave_moneda;
        private string _fact_importe_posit_negat;
        private decimal _importe_total_factura_sin_impuestos;
        private decimal _importe_total_impuestos;
        private DateTime _fecha_factura;
        private string _ejercicio;
        private List<InterfazPrefacturas_RegPosBE> _lstInterfazPrefacturas_RegPosBE;
        #endregion

        #region Constructores
        public InterfazPrefacturas_RegCabBE(){
            _lstInterfazPrefacturas_RegPosBE = new List<InterfazPrefacturas_RegPosBE>();
        }
        #endregion

        #region Propiedades
        public string Numero_documento_preliminar
        {
            get
            {
                return _numero_documento_preliminar;
            }

            set
            {
                _numero_documento_preliminar = value;
            }
        }

        public string Sociedad
        {
            get
            {
                return _sociedad;
            }

            set
            {
                _sociedad = value;
            }
        }

        public string Clave_moneda
        {
            get
            {
                return _clave_moneda;
            }

            set
            {
                _clave_moneda = value;
            }
        }

        public string Fact_importe_posit_negat
        {
            get
            {
                return _fact_importe_posit_negat;
            }

            set
            {
                _fact_importe_posit_negat = value;
            }
        }

        public decimal Importe_total_factura_sin_impuestos
        {
            get
            {
                return _importe_total_factura_sin_impuestos;
            }

            set
            {
                _importe_total_factura_sin_impuestos = value;
            }
        }

        public decimal Importe_total_impuestos
        {
            get
            {
                return _importe_total_impuestos;
            }

            set
            {
                _importe_total_impuestos = value;
            }
        }

        public DateTime Fecha_factura
        {
            get
            {
                return _fecha_factura;
            }

            set
            {
                _fecha_factura = value;
            }
        }

        public string Ejercicio
        {
            get
            {
                return _ejercicio;
            }

            set
            {
                _ejercicio = value;
            }
        }

        public List<InterfazPrefacturas_RegPosBE> LstInterfazPrefacturas_RegPosBE
        {
            get
            {
                return _lstInterfazPrefacturas_RegPosBE;
            }

            set
            {
                _lstInterfazPrefacturas_RegPosBE = value;
            }
        }
        #endregion

    }
}
