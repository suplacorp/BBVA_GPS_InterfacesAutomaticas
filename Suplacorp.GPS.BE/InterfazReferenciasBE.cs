using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Suplacorp.GPS.BE
{
    public class InterfazReferenciasBE
    {
        #region Variables
        private int _id;
        private int _nro_proceso;
        private string _valor;
        private int _id_validacion;
        private string _nombre_fichero;
        private string _ruta_fichero;
        private DateTime _fecha_ejecucion;
        private DateTime _fecha_registro;
        private bool _procesado;
        #endregion

        #region Constructores
        public InterfazReferenciasBE()
        {

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

        public int Id_validacion
        {
            get
            {
                return _id_validacion;
            }
            set
            {
                if (_id_validacion == value) return;
                _id_validacion = value;
            }
        }

        //
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
