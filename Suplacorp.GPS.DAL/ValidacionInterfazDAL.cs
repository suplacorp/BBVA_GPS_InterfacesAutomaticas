using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;


using Suplacorp.GPS.BE;

namespace Suplacorp.GPS.DAL
{
    public class ValidacionInterfazDAL : BaseDAL
    {
        #region Constructor
        public ValidacionInterfazDAL()
        {

        }
        #endregion

        public List<ValidacionInterfazBE> ListarValidaciones_xInterfaz(string nombre_fichero)
        {
            List<ValidacionInterfazBE> _lstValidacion = new List<ValidacionInterfazBE>();
            try{
                using (DbCommand _sqlCmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_SEL_CARGARVALIDACIONES_XINTERFAZ")) {
                    sqlDatabase.AddInParameter(_sqlCmd, "NOMBRE_FICHERO", DbType.String, nombre_fichero);
                    using (IDataReader dataReader = sqlDatabase.ExecuteReader(_sqlCmd)){
                        while (dataReader.Read()){
                            _lstValidacion.Add(CargarValidacion(dataReader));
                        }
                    }
                }                
            }
            catch (Exception ex){
                throw;
            }
            return _lstValidacion;
        }

        private ValidacionInterfazBE CargarValidacion(IDataReader dataReader) {
            ValidacionInterfazBE _validacion = new ValidacionInterfazBE();

            _validacion.Id = (Convert.IsDBNull(dataReader["ID"]) ? 0 : Convert.ToInt32(dataReader["ID"]));
            _validacion.Nro_item = (Convert.IsDBNull(dataReader["NRO_ITEM"]) ? 0 : Convert.ToInt32(dataReader["NRO_ITEM"]));
            _validacion.Campo = (Convert.IsDBNull(dataReader["CAMPO"]) ? "" : Convert.ToString(dataReader["CAMPO"]));
            _validacion.Formato = (Convert.IsDBNull(dataReader["FORMATO"]) ? "" : Convert.ToString(dataReader["FORMATO"]));
            _validacion.Longitud = (Convert.IsDBNull(dataReader["LONGITUD"]) ? 0 : Convert.ToInt32(dataReader["LONGITUD"]));
            _validacion.Decimales = (Convert.IsDBNull(dataReader["DECIMALES"]) ? 0 : Convert.ToInt32(dataReader["DECIMALES"]));
            _validacion.Regla_validacion = (Convert.IsDBNull(dataReader["REGLA_VALIDACION"]) ? "" : Convert.ToString(dataReader["REGLA_VALIDACION"]));
            _validacion.Obligator = (Convert.IsDBNull(dataReader["OBLIGATOR"]) ? false : Convert.ToBoolean(dataReader["OBLIGATOR"]));
            _validacion.Logica_mapeo = (Convert.IsDBNull(dataReader["LOGICA_MAPEO"]) ? "" : Convert.ToString(dataReader["LOGICA_MAPEO"]));
            _validacion.Descripcion = (Convert.IsDBNull(dataReader["DESCRIPCION"]) ? "" : Convert.ToString(dataReader["DESCRIPCION"]));
            _validacion.Consideraciones = (Convert.IsDBNull(dataReader["CONSIDERACIONES"]) ? "" : Convert.ToString(dataReader["CONSIDERACIONES"]));

            _validacion.Tipo_interfaz.Idinterface = (Convert.IsDBNull(dataReader["IDINTERFACE"]) ? 0 : Convert.ToInt32(dataReader["IDINTERFACE"]));
            _validacion.Tipo_interfaz.Descripcion = (Convert.IsDBNull(dataReader["DESCRIPCION_INTERFACE"]) ? "" : Convert.ToString(dataReader["DESCRIPCION_INTERFACE"]));

            _validacion.Tipo_registro.Idtiporegistro = (Convert.IsDBNull(dataReader["IDTIPOREGISTRO"]) ? 0 : Convert.ToInt32(dataReader["IDTIPOREGISTRO"]));
            _validacion.Tipo_registro.Descripcion = (Convert.IsDBNull(dataReader["DESCRIPCION_TIPOREGISTRO"]) ? "" : Convert.ToString(dataReader["DESCRIPCION_TIPOREGISTRO"]));

            _validacion.Id_tipodetalle_tiporegistro = (Convert.IsDBNull(dataReader["IDTIPODETALLE_TIPOREGISTRO"]) ? 0 : Convert.ToInt32(dataReader["IDTIPODETALLE_TIPOREGISTRO"]));
            _validacion.Cierra_registro = (Convert.IsDBNull(dataReader["CIERRA_REGISTRO"]) ? false : Convert.ToBoolean(dataReader["CIERRA_REGISTRO"]));

            return _validacion;
        }

        public Dictionary<string, object> CargarVariablesIniciales() {

            Dictionary<string, object> lstVariables = new Dictionary<string, object>();
            try
            {
                using (DbCommand _sqlCmd = sqlDatabase.GetStoredProcCommand("BBVA_GPS_SEL_CARGAR_VARIABLES_GLOBALES"))
                {
                    using (IDataReader dataReader = sqlDatabase.ExecuteReader(_sqlCmd)){
                        while (dataReader.Read()){
                            lstVariables.Add(Convert.IsDBNull(dataReader["IDTABLA"]) ? "" : Convert.ToString(dataReader["IDTABLA"]), dataReader["VALOR"]);
                        }
                    }
                }
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }
            return lstVariables;
        }
    }
}
