using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.DAL;
using Suplacorp.GPS.BE;
using System.Collections;
using System.Reflection;

namespace Suplacorp.GPS.BL
{
    public class InterfazReferenciasBL : BaseBL<InterfazReferenciasBE>, IInterfaz<InterfazReferenciasBE>
    {
        Dictionary<int, string> dic_tipo_registro_detalle = new Dictionary<int, string>();

        public InterfazReferenciasBL() {
            dic_tipo_registro_detalle.Add(0, "0");
            
        }

        public List<InterfazReferenciasBE> LeerFicheroInterfaz(string nombre_fichero, string ruta_fichero, List<ValidacionInterfazBE> lstValidacion)
        {
            List<InterfazReferenciasBE> lstInterfazReferencias = new List<InterfazReferenciasBE>();
            int contador = 0;
            string linea_actual;
            System.IO.StreamReader file = null;

            try
            {
                //1) Registro de control inicial para el inicio del fichero
                List<ValidacionInterfazBE> lstValidacionRegistroInicial = new List<ValidacionInterfazBE>();
                lstValidacionRegistroInicial = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 0);

                //2) Registros de proceso
                List<ValidacionInterfazBE> lstValidacionRegistroProceso = new List<ValidacionInterfazBE>();
                lstValidacionRegistroProceso = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 1);

                //3) Registros para fin
                List<ValidacionInterfazBE> lstValidacionRegistroFin = new List<ValidacionInterfazBE>();
                lstValidacionRegistroFin = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 9);

                // Leyendo el archivo
                file = new System.IO.StreamReader(ruta_fichero);
                while ((linea_actual = file.ReadLine()) != null)
                {
                    if (linea_actual.Length > 0 && linea_actual != "") {
                        lstInterfazReferencias.Add(LlenarEntidad(linea_actual, lstValidacionRegistroInicial, lstValidacionRegistroProceso, lstValidacionRegistroFin));
                        //System.Console.WriteLine(line);
                    }
                    contador++;
                }
            }
            catch (Exception ex){
                throw ex;
            }
            finally{
                file.Close();
            }
            return lstInterfazReferencias;
        }

        public InterfazReferenciasBE LlenarEntidad(string linea_actual, params List<ValidacionInterfazBE>[] listas_validacion_x_tiporegistro)
        {
            InterfazReferenciasBE _interfazReferencia;
            Dictionary<int, string> _dict = new Dictionary<int, string>();

            try {

                String[] valores_linea_actual = linea_actual.Split('\t');
                string idTipoDetalle_TipoRegistro = valores_linea_actual[1].ToString();
                _interfazReferencia = new InterfazReferenciasBE();

                switch (idTipoDetalle_TipoRegistro) {
                    case "0": //1) Registro de control inicial para el inicio del fichero
                        _interfazReferencia.Id = 0;
                        _interfazReferencia.Validacion.Id = listas_validacion_x_tiporegistro[Convert.ToInt32(idTipoDetalle_TipoRegistro)][i].Id;
                        _interfazReferencia.Nro_proceso = 0;
                        _interfazReferencia.Valor = valores_linea_actual[i];
                        _interfazReferencia.Nombre_fichero = "NOMBRE FICHERO KIKE";
                        _interfazReferencia.Ruta_fichero = "RUTA FICHERO KIKE";
                        _interfazReferencia.Fecha_ejecucion = DateTime.Today;
                        _interfazReferencia.Fecha_registro = DateTime.Today;
                        _interfazReferencia.Procesado = false;
                        break;
                    case "1": //2) Registros de proceso

                        break;
                    case "9": //3) Registros para fin

                        break;
                }

                /*
                for (int i = 0; i < listas_validacion_x_tiporegistro[Convert.ToInt32(idTipoDetalle_TipoRegistro)].Count; i++) {
                    _interfazReferencia.Id = 0;
                    _interfazReferencia.Validacion.Id = listas_validacion_x_tiporegistro[Convert.ToInt32(idTipoDetalle_TipoRegistro)][i].Id;
                    _interfazReferencia.Nro_proceso = 0;
                    _interfazReferencia.Valor = valores_linea_actual[i];
                    _interfazReferencia.Nombre_fichero = "NOMBRE FICHERO KIKE";
                    _interfazReferencia.Ruta_fichero = "RUTA FICHERO KIKE";
                    _interfazReferencia.Fecha_ejecucion = DateTime.Today;
                    _interfazReferencia.Fecha_registro = DateTime.Today;
                    _interfazReferencia.Procesado = false;
                }*/


            }
            catch (Exception ex){
                throw;
            }
            return _interfazReferencia;
        }

        // Explicit predicate delegate.
        private static bool EncontrarValidaciones_xIdTipoDetalle_TipoRegistro(ValidacionInterfazBE validacion)
        {
            if (validacion.Id_tipodetalle_tiporegistro == 1){
                return true;
            }
            else{
                return false;
            }
        }

        private static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }
        private static FieldInfo[] GetFields(object obj)
        {
            return obj.GetType().GetFields();
        }
    }
}
