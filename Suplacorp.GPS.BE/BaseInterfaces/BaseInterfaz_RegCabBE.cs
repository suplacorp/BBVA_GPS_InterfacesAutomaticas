using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public abstract class BaseInterfaz_RegCabBE
    {
        #region variables
        private int _idcab;
        private int _idregini;

        private string _numeral;
        private string _tipo_registro;

        private bool _procesado;
        #endregion

        #region constructores
        public BaseInterfaz_RegCabBE(){

        }
        #endregion

        #region Propiedades
        public int Idcab
        {
            get
            {
                return _idcab;
            }

            set
            {
                _idcab = value;
            }
        }

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
        #endregion

    }
}
