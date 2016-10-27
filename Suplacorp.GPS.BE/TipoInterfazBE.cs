using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class TipoInterfazBE
    {
        #region Variables
        private int _idinterface;
        private string _descripcion;
        #endregion

        #region Constructores
        public TipoInterfazBE()
        {

        }
        #endregion

        #region Propiedades
        public int Idinterface
        {
            get
            {
                return _idinterface;
            }
            set
            {
                if (_idinterface == value) return;
                _idinterface = value;
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
