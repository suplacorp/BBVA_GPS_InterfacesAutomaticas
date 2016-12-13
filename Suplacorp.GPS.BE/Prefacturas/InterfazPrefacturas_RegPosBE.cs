using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazPrefacturas_RegPosBE : BaseInterfaz_RegPosBE
    {
        #region Variables
        private string _numero_documento_compras;
        private string _posicion_documento_compras;
        private string _numero_doc_ref_albaran;
        private string _posicion_fact_import_post_neg;
        private decimal _importe_total_posicion_sin_impuestos;
        private decimal _porcentaje_impuesto;
        private string _numero_material_servicio;
        private int _idarticulo;
        private string _descripcion_art;
        private decimal _cantidad;
        private string _unidad_medida_base;
        #endregion

        #region Constructores
        public InterfazPrefacturas_RegPosBE(){

        }
        #endregion

        #region Propiedades
        public string Numero_documento_compras
        {
            get
            {
                return _numero_documento_compras;
            }

            set
            {
                _numero_documento_compras = value;
            }
        }

        public string Posicion_documento_compras
        {
            get
            {
                return _posicion_documento_compras;
            }

            set
            {
                _posicion_documento_compras = value;
            }
        }

        public string Numero_doc_ref_albaran
        {
            get
            {
                return _numero_doc_ref_albaran;
            }

            set
            {
                _numero_doc_ref_albaran = value;
            }
        }

        public string Posicion_fact_import_post_neg
        {
            get
            {
                return _posicion_fact_import_post_neg;
            }

            set
            {
                _posicion_fact_import_post_neg = value;
            }
        }

        public decimal Importe_total_posicion_sin_impuestos
        {
            get
            {
                return _importe_total_posicion_sin_impuestos;
            }

            set
            {
                _importe_total_posicion_sin_impuestos = value;
            }
        }

        public decimal Porcentaje_impuesto
        {
            get
            {
                return _porcentaje_impuesto;
            }

            set
            {
                _porcentaje_impuesto = value;
            }
        }

        public string Numero_material_servicio
        {
            get
            {
                return _numero_material_servicio;
            }

            set
            {
                _numero_material_servicio = value;
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
