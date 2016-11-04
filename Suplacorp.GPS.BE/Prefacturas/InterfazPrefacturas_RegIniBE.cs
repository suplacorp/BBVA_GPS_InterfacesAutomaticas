using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazPrefacturas_RegIniBE : BaseInterfaz_RegIniBE
    {
        #region Variables
        private string _numero_registros_cab_fin;
        private string _numero_registros_pos_fin;
        private string _numero_registros_tipo3_fin;
        private List<InterfazPrefacturas_RegCabBE> _lstInterfazPrefacturas_RegCabBE;
        #endregion

        #region Constructores
        public InterfazPrefacturas_RegIniBE(){
            _lstInterfazPrefacturas_RegCabBE = new List<InterfazPrefacturas_RegCabBE>();
        }
        #endregion

        #region Propiedades
        public string Numero_registros_cab_fin
        {
            get
            {
                return _numero_registros_cab_fin;
            }

            set
            {
                _numero_registros_cab_fin = value;
            }
        }

        public string Numero_registros_pos_fin
        {
            get
            {
                return _numero_registros_pos_fin;
            }

            set
            {
                _numero_registros_pos_fin = value;
            }
        }

        public string Numero_registros_tipo3_fin
        {
            get
            {
                return _numero_registros_tipo3_fin;
            }

            set
            {
                _numero_registros_tipo3_fin = value;
            }
        }

        public List<InterfazPrefacturas_RegCabBE> LstInterfazPrefacturas_RegCabBE
        {
            get
            {
                return _lstInterfazPrefacturas_RegCabBE;
            }

            set
            {
                _lstInterfazPrefacturas_RegCabBE = value;
            }
        }
        #endregion

    }
}
