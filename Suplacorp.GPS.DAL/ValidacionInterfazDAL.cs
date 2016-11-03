﻿using System;
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
        private SqlDatabase _sqlDatabase;

        public ValidacionInterfazDAL() {
            _sqlDatabase = new SqlDatabase(base.strConnectionString);
        }

        public void kike() {
            
            //TEST
            SqlDatabase sqlDatabase = new SqlDatabase(base.strConnectionString);
            using (IDataReader reader = sqlDatabase.ExecuteReader(CommandType.Text, "SELECT TOP 1 * FROM usuariosweb")){
                DisplayRowValues(reader);
            }
        }
        static void DisplayRowValues(IDataReader reader){

            while (reader.Read()){
                for (int i = 0; i < reader.FieldCount; i++){
                    Console.WriteLine("{0} = {1}", reader.GetName(i), reader[i].ToString());
                }
                Console.WriteLine();
            }
        }

        public List<ValidacionInterfazBE> ListarValidaciones_xInterfaz(string nombre_fichero)
        {
            List<ValidacionInterfazBE> _lstValidacion = new List<ValidacionInterfazBE>();
            try
            {
                using (DbCommand _sqlCmd = _sqlDatabase.GetStoredProcCommand("BBVA_GPS_SEL_CARGARVALIDACIONES_XINTERFAZ")) {
                    _sqlDatabase.AddInParameter(_sqlCmd, "NOMBRE_FICHERO", DbType.String, nombre_fichero);
                    using (IDataReader dataReader = _sqlDatabase.ExecuteReader(_sqlCmd)){
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

    }
}
