using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    public class InterfazSuministros_RegCabBE : BaseInterfaz_RegCabBE
    {
        #region Variables
        private string _codigo_cesta;
        private string _nombre_destinatario;
        private string _desc_unid_ofi_estab;
        private string _calle_direccion;
        private string _nro_direccion_entrega;
        private string _codigo_postal;
        private string _provincia;
        private string _apartado_alternativo;
        private string _region_departamento;
        private string _ciudad_poblacion_provincia;
        private string _pais;
        private string _telefono_contacto;
        private string _usuario;
        private string _colonia_comuna_planta_distrito;
        private string _entre_calle1;
        private string _entre_calle2;
        private string _Centro;
        private string _urgente;
        private List<InterfazSuministros_RegPosBE> _lstInterfazSuministros_RegPosBE;
        #endregion

        #region Constructores
        public InterfazSuministros_RegCabBE() {
            LstInterfazSuministros_RegPosBE = new List<InterfazSuministros_RegPosBE>();
        }
        #endregion

        #region Propiedades
        public string Codigo_cesta
        {
            get
            {
                return _codigo_cesta;
            }

            set
            {
                _codigo_cesta = value;
            }
        }

        public string Nombre_destinatario
        {
            get
            {
                return _nombre_destinatario;
            }

            set
            {
                _nombre_destinatario = value;
            }
        }

        public string Desc_unid_ofi_estab
        {
            get
            {
                return _desc_unid_ofi_estab;
            }

            set
            {
                _desc_unid_ofi_estab = value;
            }
        }

        public string Calle_direccion
        {
            get
            {
                return _calle_direccion;
            }

            set
            {
                _calle_direccion = value;
            }
        }

        public string Nro_direccion_entrega
        {
            get
            {
                return _nro_direccion_entrega;
            }

            set
            {
                _nro_direccion_entrega = value;
            }
        }

        public string Codigo_postal
        {
            get
            {
                return _codigo_postal;
            }

            set
            {
                _codigo_postal = value;
            }
        }

        public string Provincia
        {
            get
            {
                return _provincia;
            }

            set
            {
                _provincia = value;
            }
        }

        public string Apartado_alternativo
        {
            get
            {
                return _apartado_alternativo;
            }

            set
            {
                _apartado_alternativo = value;
            }
        }

        public string Region_departamento
        {
            get
            {
                return _region_departamento;
            }

            set
            {
                _region_departamento = value;
            }
        }

        public string Ciudad_poblacion_provincia
        {
            get
            {
                return _ciudad_poblacion_provincia;
            }

            set
            {
                _ciudad_poblacion_provincia = value;
            }
        }

        public string Pais
        {
            get
            {
                return _pais;
            }

            set
            {
                _pais = value;
            }
        }

        public string Telefono_contacto
        {
            get
            {
                return _telefono_contacto;
            }

            set
            {
                _telefono_contacto = value;
            }
        }

        public string Usuario
        {
            get
            {
                return _usuario;
            }

            set
            {
                _usuario = value;
            }
        }

        public string Colonia_comuna_planta_distrito
        {
            get
            {
                return _colonia_comuna_planta_distrito;
            }

            set
            {
                _colonia_comuna_planta_distrito = value;
            }
        }

        public string Entre_calle1
        {
            get
            {
                return _entre_calle1;
            }

            set
            {
                _entre_calle1 = value;
            }
        }

        public string Entre_calle2
        {
            get
            {
                return _entre_calle2;
            }

            set
            {
                _entre_calle2 = value;
            }
        }

        public string Centro
        {
            get
            {
                return _Centro;
            }

            set
            {
                _Centro = value;
            }
        }

        public string Urgente
        {
            get
            {
                return _urgente;
            }

            set
            {
                _urgente = value;
            }
        }

        public List<InterfazSuministros_RegPosBE> LstInterfazSuministros_RegPosBE
        {
            get
            {
                return _lstInterfazSuministros_RegPosBE;
            }

            set
            {
                _lstInterfazSuministros_RegPosBE = value;
            }
        }
        #endregion
    }
}
