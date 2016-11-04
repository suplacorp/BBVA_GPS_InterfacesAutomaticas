using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazSuministros_RegIniBE : BaseInterfaz_RegIniBE
    {
        #region Variables
        private string _numero_registros_cab_fin;
        private string _numero_registros_pos_fin;
        private string _numero_registros_tipo3_fin;
        private List<InterfazSuministros_RegCabBE> _lstInterfazSuministros_RegCabBE;
        #endregion

        #region Constructores
        public InterfazSuministros_RegIniBE() {
            _lstInterfazSuministros_RegCabBE = new List<InterfazSuministros_RegCabBE>();
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

        public List<InterfazSuministros_RegCabBE> LstInterfazSuministros_RegCabBE
        {
            get
            {
                return _lstInterfazSuministros_RegCabBE;
            }

            set
            {
                _lstInterfazSuministros_RegCabBE = value;
            }
        }
        #endregion
    }
}
