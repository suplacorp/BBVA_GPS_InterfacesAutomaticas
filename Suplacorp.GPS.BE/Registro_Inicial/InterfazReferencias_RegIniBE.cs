using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Suplacorp.GPS.BE
{
    public class InterfazReferencias_RegIniBE : BaseInterfaz_RegIniBE
    {
        #region Variables
        private string _numero_registros_proceso_fin;
        private string _numero_registros_tipo2_fin;
        private string _numero_registros_tipo3_fin;
        #endregion

        #region Constructores
        public InterfazReferencias_RegIniBE() {

        }
        #endregion

        #region Propiedades
        public string Numero_registros_proceso_fin
        {
            get
            {
                return _numero_registros_proceso_fin;
            }

            set
            {
                _numero_registros_proceso_fin = value;
            }
        }

        public string Numero_registros_tipo2_fin
        {
            get
            {
                return _numero_registros_tipo2_fin;
            }

            set
            {
                _numero_registros_tipo2_fin = value;
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
        #endregion
        
    }
}
