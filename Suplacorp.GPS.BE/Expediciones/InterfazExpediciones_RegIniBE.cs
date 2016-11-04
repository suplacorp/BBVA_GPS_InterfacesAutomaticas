using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazExpediciones_RegIniBE : BaseInterfaz_RegIniBE
    {
        #region Variables
        private string _numero_registros_cab_fin;
        private string _numero_registros_pos_fin;
        private string _numero_registros_tipo3_fin;
        private List<InterfazExpediciones_RegCabBE> _lstInterfazExpediciones_RegCabBE;
        #endregion

        #region Constructores
        public InterfazExpediciones_RegIniBE(){
            _lstInterfazExpediciones_RegCabBE = new List<InterfazExpediciones_RegCabBE>();
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

        public List<InterfazExpediciones_RegCabBE> LstInterfazExpediciones_RegCabBE
        {
            get
            {
                return _lstInterfazExpediciones_RegCabBE;
            }

            set
            {
                _lstInterfazExpediciones_RegCabBE = value;
            }
        }
        #endregion
    }
}
