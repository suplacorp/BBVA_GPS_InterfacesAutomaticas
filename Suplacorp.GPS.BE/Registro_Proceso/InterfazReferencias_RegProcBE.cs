using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Suplacorp.GPS.BE
{
    public class InterfazReferencias_RegProcBE
    {
        #region Variables

        private int _idproc;
        private int _idregini;
        private string _numeral;
        private string _tipo_registro;
        private string _sociedad;
        private string _codigo_material;
        private string _texto_breve_material;
        private string _unidad_medida_base;
        private string _unidad_medida_pedido;
        private string _contador_conv_und_med_base;
        private string _denominador_conv_und_med_base;
        private string _status_mat_todos_centros;
        private string _status_mat_especif_centro;
        private string _tipo_aprov;
        private string _indicador_breve;
        private decimal _peso_bruto;
        private string _unidad_peso;
        private decimal _volumen;
        private string _unidad_volumen;
        private string _codigo_antiguo_material;
        private string _centro;
        private bool _procesado;

        #endregion

        #region Constructores
        public InterfazReferencias_RegProcBE() {

        }
        #endregion

        #region Propiedades
        public int Idproc
        {
            get
            {
                return _idproc;
            }

            set
            {
                _idproc = value;
            }
        }

        public int Idregini1
        {
            get
            {
                return _idregini;
            }

            set
            {
                _idregini = value;
            }
        }

        public string Numeral1
        {
            get
            {
                return _numeral;
            }

            set
            {
                _numeral = value;
            }
        }

        public string Tipo_registro1
        {
            get
            {
                return _tipo_registro;
            }

            set
            {
                _tipo_registro = value;
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

        public string Codigo_material
        {
            get
            {
                return _codigo_material;
            }

            set
            {
                _codigo_material = value;
            }
        }

        public string Texto_breve_material
        {
            get
            {
                return _texto_breve_material;
            }

            set
            {
                _texto_breve_material = value;
            }
        }

        public string Unidad_medida_base
        {
            get
            {
                return _unidad_medida_base;
            }

            set
            {
                _unidad_medida_base = value;
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

        public string Contador_conv_und_med_base
        {
            get
            {
                return _contador_conv_und_med_base;
            }

            set
            {
                _contador_conv_und_med_base = value;
            }
        }

        public string Denominador_conv_und_med_base
        {
            get
            {
                return _denominador_conv_und_med_base;
            }

            set
            {
                _denominador_conv_und_med_base = value;
            }
        }

        public string Status_mat_todos_centros
        {
            get
            {
                return _status_mat_todos_centros;
            }

            set
            {
                _status_mat_todos_centros = value;
            }
        }

        public string Status_mat_especif_centro
        {
            get
            {
                return _status_mat_especif_centro;
            }

            set
            {
                _status_mat_especif_centro = value;
            }
        }

        public string Tipo_aprov
        {
            get
            {
                return _tipo_aprov;
            }

            set
            {
                _tipo_aprov = value;
            }
        }

        public string Indicador_breve
        {
            get
            {
                return _indicador_breve;
            }

            set
            {
                _indicador_breve = value;
            }
        }

        public decimal Peso_bruto
        {
            get
            {
                return _peso_bruto;
            }

            set
            {
                _peso_bruto = value;
            }
        }

        public string Unidad_peso
        {
            get
            {
                return _unidad_peso;
            }

            set
            {
                _unidad_peso = value;
            }
        }

        public decimal Volumen
        {
            get
            {
                return _volumen;
            }

            set
            {
                _volumen = value;
            }
        }

        public string Unidad_volumen
        {
            get
            {
                return _unidad_volumen;
            }

            set
            {
                _unidad_volumen = value;
            }
        }

        public string Codigo_antiguo_material
        {
            get
            {
                return _codigo_antiguo_material;
            }

            set
            {
                _codigo_antiguo_material = value;
            }
        }

        public string Centro
        {
            get
            {
                return _centro;
            }

            set
            {
                _centro = value;
            }
        }

        public bool Procesado1
        {
            get
            {
                return _procesado;
            }

            set
            {
                _procesado = value;
            }
        }
        #endregion

    }
}
