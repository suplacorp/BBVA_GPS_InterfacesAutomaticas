using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class TipoRegistroBE
    {
        #region Variables
        private int _idtiporegistro;
        private string _descripcion;
        #endregion

        #region Constructores
        public TipoRegistroBE()
        {

        }
        #endregion

        #region Propiedades
        public int Idtiporegistro
        {
            get
            {
                return _idtiporegistro;
            }
            set
            {
                if (_idtiporegistro == value) return;
                _idtiporegistro = value;
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
        #endregion

    }
}
