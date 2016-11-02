using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class ValidacionInterfazBE
    {
        #region Variables
        private int _id;
        private int _nro_item;
        private string _campo;
        private string _formato;
        private int _longitud;
        private int _decimales;
        private string _regla_validacion;
        private bool _obligator;
        private string _logica_mapeo;
        private string _descripcion;
        private string _consideraciones;

        //private int _idinterface;
        private TipoInterfazBE _tipo_interfaz;
        
        //private int _idtiporegistro;
        private TipoRegistroBE _tipo_registro;

        private int _id_tipodetalle_tiporegistro;

        private bool _cierra_registro;

        #endregion

        #region Constructores
        public ValidacionInterfazBE() {
            _tipo_interfaz = new TipoInterfazBE();
            _tipo_registro = new TipoRegistroBE();
        }

        public ValidacionInterfazBE(int id){
            //Cargar ValidacionInterfazBE por id
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

        public int Nro_item
        {
            get
            {
                return _nro_item;
            }
            set
            {
                if (_nro_item == value) return;
                _nro_item = value;
            }
        }

        public string Campo
        {
            get
            {
                return _campo;
            }
            set
            {
                if (_campo == value) return;
                _campo = value;
            }
        }

        
        public string Formato
        {
            get
            {
                return _formato;
            }
            set
            {
                if (_formato == value) return;
                _formato = value;
            }
        }

        
        public int Longitud
        {
            get
            {
                return _longitud;
            }
            set
            {
                if (_longitud == value) return;
                _longitud = value;
            }
        }

        public int Decimales
        {
            get
            {
                return _decimales;
            }
            set
            {
                if (_decimales == value) return;
                _decimales = value;
            }
        }

        public string Regla_validacion
        {
            get
            {
                return _regla_validacion;
            }
            set
            {
                if (_regla_validacion == value) return;
                _regla_validacion = value;
            }
        }

        public bool Obligator
        {
            get
            {
                return _obligator;
            }
            set
            {
                if (_obligator == value) return;
                _obligator = value;
            }
        }

        public string Logica_mapeo
        {
            get
            {
                return _logica_mapeo;
            }
            set
            {
                if (_logica_mapeo == value) return;
                _logica_mapeo = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                if (_descripcion == value) return;
                _descripcion = value;
            }
        }

        public string Consideraciones
        {
            get
            {
                return _consideraciones;
            }
            set
            {
                if (_consideraciones == value) return;
                _consideraciones = value;
            }
        }

        //public int Idinterface
        //{
        //    get
        //    {
        //        return _idinterface;
        //    }
        //    set
        //    {
        //        if (_idinterface == value) return;
        //        _idinterface = value;
        //    }
        //}
        //public int Idtiporegistro
        //{
        //    get
        //    {
        //        return _idtiporegistro;
        //    }
        //    set
        //    {
        //        if (_idtiporegistro == value) return;
        //        _idtiporegistro = value;
        //    }
        //}

        public TipoRegistroBE Tipo_registro
        {
            get
            {
                return _tipo_registro;
            }
            set
            {
                if (_tipo_registro == value) return;
                _tipo_registro = value;
            }
        }


        public TipoInterfazBE Tipo_interfaz
        {
            get
            {
                return _tipo_interfaz;
            }
            set
            {
                if (_tipo_interfaz == value) return;
                _tipo_interfaz = value;
            }
        }

        public bool Cierra_registro
        {
            get
            {
                return _cierra_registro;
            }
            set
            {
                if (_cierra_registro == value) return;
                _cierra_registro = value;
            }
        }

        public int Id_tipodetalle_tiporegistro
        {
            get
            {
                return _id_tipodetalle_tiporegistro;
            }

            set
            {
                _id_tipodetalle_tiporegistro = value;
            }
        }

        #endregion
    }

    
}
