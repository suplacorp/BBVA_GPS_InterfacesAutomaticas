using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public abstract class BaseInterfaz_RegIniBE
    {
        #region variables
        private int _idregini;
        private int _nro_proceso;
        private string _numeral;
        private string _tipo_registro; 
        private string _tipo_interfaz; 
        private string _pais;
        private string _identificador_interfaz;
        private string _nombre_fichero;
        private DateTime _fecha_ejecucion;
        private string _hora_proceso;
        private string _ruta_fichero_detino;
        private TipoInterfazBE interfaz;
        private TipoRegistroBE tiporegistro;
        private bool _procesado;
        private string _numero_total_registros_fin;
        private string _tipo_registro_fin;
        #endregion

        #region constructores
        public BaseInterfaz_RegIniBE(){
            interfaz = new TipoInterfazBE();
            tiporegistro = new TipoRegistroBE();
        }
        #endregion

        #region Propiedades
        public int Idregini
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

        public int Nro_proceso
        {
            get
            {
                return _nro_proceso;
            }

            set
            {
                _nro_proceso = value;
            }
        }

        public string Numeral
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

        public string Tipo_registro
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

        public string Tipo_interfaz
        {
            get
            {
                return _tipo_interfaz;
            }

            set
            {
                _tipo_interfaz = value;
            }
        }

        public string Pais
        {
            get
            {
                return _pais;
            }

            set
            {
                _pais = value;
            }
        }

        public string Identificador_interfaz
        {
            get
            {
                return _identificador_interfaz;
            }

            set
            {
                _identificador_interfaz = value;
            }
        }

        public string Nombre_fichero
        {
            get
            {
                return _nombre_fichero;
            }

            set
            {
                _nombre_fichero = value;
            }
        }

        public DateTime Fecha_ejecucion
        {
            get
            {
                return _fecha_ejecucion;
            }

            set
            {
                _fecha_ejecucion = value;
            }
        }

        public string Hora_proceso
        {
            get
            {
                return _hora_proceso;
            }

            set
            {
                _hora_proceso = value;
            }
        }

        public string Ruta_fichero_detino
        {
            get
            {
                return _ruta_fichero_detino;
            }

            set
            {
                _ruta_fichero_detino = value;
            }
        }

        public TipoInterfazBE Interfaz
        {
            get
            {
                return interfaz;
            }

            set
            {
                interfaz = value;
            }
        }

        public TipoRegistroBE Tiporegistro
        {
            get
            {
                return tiporegistro;
            }

            set
            {
                tiporegistro = value;
            }
        }

        public bool Procesado
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

        public string Numero_total_registros_fin
        {
            get
            {
                return _numero_total_registros_fin;
            }

            set
            {
                _numero_total_registros_fin = value;
            }
        }

        public string Tipo_registro_fin
        {
            get
            {
                return _tipo_registro_fin;
            }

            set
            {
                _tipo_registro_fin = value;
            }
        }
        #endregion

    }
}
