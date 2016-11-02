using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public abstract class BaseInterfazBE
    {
        #region variables
        private int _id;
        private int _nro_proceso;
        private string _valor;
        //private int _id_validacion;
        private ValidacionInterfazBE _validacion;
        private string _nombre_fichero;
        private string _ruta_fichero;
        private DateTime _fecha_ejecucion;
        private DateTime _fecha_registro;
        private bool _procesado;
        #endregion

        #region constructores
        public BaseInterfazBE(){
            _validacion = new BE.ValidacionInterfazBE();
        }
        public BaseInterfazBE(int id){
            //Cargar InterfazBE en base a su id
        }
        #endregion

        #region Propiedades
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == value) return;
                _id = value;
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
                if (_nro_proceso == value) return;
                _nro_proceso = value;
            }
        }
        public string Valor
        {
            get
            {
                return _valor;
            }
            set
            {
                if (_valor == value) return;
                _valor = value;
            }
        }

        public ValidacionInterfazBE Validacion
        {
            get
            {
                return _validacion;
            }
            set
            {
                if (_validacion == value) return;
                _validacion = value;
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
                if (_nombre_fichero == value) return;
                _nombre_fichero = value;
            }
        }

        public string Ruta_fichero
        {
            get
            {
                return _ruta_fichero;
            }
            set
            {
                if (_ruta_fichero == value) return;
                _ruta_fichero = value;
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
                if (_fecha_ejecucion == value) return;
                _fecha_ejecucion = value;
            }
        }

        public DateTime Fecha_registro
        {
            get
            {
                return _fecha_registro;
            }
            set
            {
                if (_fecha_registro == value) return;
                _fecha_registro = value;
            }
        }

        public Boolean Procesado
        {
            get
            {
                return _procesado;
            }
            set
            {
                if (_procesado == value) return;
                _procesado = value;
            }
        }
        #endregion
    }
}
