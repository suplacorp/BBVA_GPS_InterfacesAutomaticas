using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;
using System.Collections;
using System.Reflection;
using Suplacorp.GPS.Utils;
using System.IO;
using Suplacorp.GPS.DAL;
using System.Collections.Specialized;
using System.Globalization;

namespace Suplacorp.GPS.BL
{
    public class InterfazPrefacturaBL : BaseBL<InterfazPrefacturas_RegIniBE>,
                                        IInterfazRegIniBL<InterfazPrefacturas_RegIniBE, InterfazReferencias_RegProcBE>,
                                        IInterfazRegCabBL<InterfazPrefacturas_RegIniBE, InterfazPrefacturas_RegCabBE>,
                                        IInterfazRegPosBL<InterfazPrefacturas_RegIniBE, InterfazPrefacturas_RegCabBE, InterfazPrefacturas_RegPosBE>
    {
        public InterfazPrefacturas_RegIniBE LeerFicheroInterfaz(string nombre_fichero, string ruta_fichero_lectura, List<ValidacionInterfazBE> lstValidacion)
        {
            InterfazPrefacturas_RegIniBE interfazPrefact_RegIniBE = new InterfazPrefacturas_RegIniBE();
            string linea_actual;
            String[] valores_linea_actual;
            string idTipoDetalle_TipoRegistro;

            try
            {
                //1) Validaciones - registro de control inicial para el inicio del fichero
                List<ValidacionInterfazBE> lstValidacionRegistroInicial = new List<ValidacionInterfazBE>();
                //lstValidacionRegistroInicial = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 0);

                //2) Validaciones - Registro de cabecera
                List<ValidacionInterfazBE> lstValidacionRegistroCabecera = new List<ValidacionInterfazBE>();
                //lstValidacionRegistroCabecera = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 1);

                //3) Validaciones - Registro de Posición
                List<ValidacionInterfazBE> lstValidacionRegistroPosicion = new List<ValidacionInterfazBE>();
                //lstValidacionRegistroPosicion = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 2);

                //4) Validaciones - Registro de control para el fin del fichero
                List<ValidacionInterfazBE> lstValidacionRegistroFin = new List<ValidacionInterfazBE>();
                //lstValidacionRegistroFin = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 9);

                using (StreamReader file = new StreamReader(ruta_fichero_lectura))
                {
                    while ((linea_actual = file.ReadLine()) != null)
                    {
                        if (linea_actual.Length > 0 && linea_actual != "")
                        {
                            valores_linea_actual = linea_actual.Split('\t');
                            idTipoDetalle_TipoRegistro = valores_linea_actual[1].ToString();
                            switch (idTipoDetalle_TipoRegistro)
                            {
                                case "0": //1) Registro de control INICIAL para el inicio del fichero
                                    LlenarEntidad_RegIni(ref interfazPrefact_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroInicial);
                                    break;
                                case "1": //2) Registro de CABECERA (cabecera del pedido)

                                    interfazPrefact_RegIniBE.LstInterfazPrefacturas_RegCabBE.Add(LlenarEntidad_RegCab(ref interfazPrefact_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroCabecera));
                                    break;
                                case "2": //3) Registro de POSICIÓN
                                    var lastIndexCab = interfazPrefact_RegIniBE.LstInterfazPrefacturas_RegCabBE.Count - 1;
                                    interfazPrefact_RegIniBE.LstInterfazPrefacturas_RegCabBE[lastIndexCab].LstInterfazPrefacturas_RegPosBE.Add(LlenarEntidad_RegPos(ref interfazPrefact_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroPosicion));

                                    break;
                                case "9": //4) Registro de control para el FIN del fichero
                                    LlenarEntidad_RegFin(ref interfazPrefact_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroFin);
                                    break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ";" + ex.Source.ToString() + ";" + ex.StackTrace.ToString());
            }
            return interfazPrefact_RegIniBE;
        }

        public InterfazPrefacturas_RegCabBE LlenarEntidad_RegCab(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegCab)
        {
            InterfazPrefacturas_RegCabBE interfazPreFact_RegCabBE = new InterfazPrefacturas_RegCabBE();

            try
            {
                //Ejemplo: "000001	1	5105674919	ES11	EUR		   1824883,70	    114466,35	21102013	2013	"
                interfazPreFact_RegCabBE.Numeral = valores_linea_actual[0].ToString();
                interfazPreFact_RegCabBE.Tipo_registro = valores_linea_actual[1].ToString();
                interfazPreFact_RegCabBE.Numero_documento_preliminar = valores_linea_actual[2].ToString();
                interfazPreFact_RegCabBE.Sociedad = valores_linea_actual[3].ToString();
                interfazPreFact_RegCabBE.Clave_moneda = valores_linea_actual[4].ToString();
                interfazPreFact_RegCabBE.Fact_importe_posit_negat = valores_linea_actual[5].ToString();
                interfazPreFact_RegCabBE.Importe_total_factura_sin_impuestos = Convert.ToDecimal(Utilitarios.DecimalFormato_BBVA(valores_linea_actual[6]));
                interfazPreFact_RegCabBE.Importe_total_impuestos = Convert.ToDecimal(Utilitarios.DecimalFormato_BBVA(valores_linea_actual[7]));
                //interfazPreFact_RegCabBE.Fecha_factura = Convert.ToDateTime(valores_linea_actual[8]);

                interfazPreFact_RegCabBE.Fecha_factura = DateTime.ParseExact(valores_linea_actual[8], "ddMMyyyy", CultureInfo.InvariantCulture);

                interfazPreFact_RegCabBE.Ejercicio = valores_linea_actual[9].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            return interfazPreFact_RegCabBE;
        }

        public void LlenarEntidad_RegFin(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegFin)
        {
            try
            {
                //Ejemplo: "000058	9	000002	000056";
                interfaz_RegIniBE.Numero_total_registros_fin = valores_linea_actual[0].ToString();
                interfaz_RegIniBE.Tipo_registro_fin = valores_linea_actual[1].ToString();
                interfaz_RegIniBE.Numero_registros_cab_fin = valores_linea_actual[2].ToString();
                interfaz_RegIniBE.Numero_registros_pos_fin = valores_linea_actual[3].ToString();
                interfaz_RegIniBE.Numero_registros_tipo3_fin = valores_linea_actual[4].ToString();
            }
            catch (Exception ex){
                throw;
            }
        }

        public void LlenarEntidad_RegIni(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegIni)
        {
            try
            {
                //Ejemplo: "000000	0	S	ES	AU	GPSTFAC.txt	21102013	165043"
                interfaz_RegIniBE.Numeral = valores_linea_actual[0].ToString();
                interfaz_RegIniBE.Tipo_registro = valores_linea_actual[1].ToString();
                interfaz_RegIniBE.Tipo_interfaz = valores_linea_actual[2].ToString();
                interfaz_RegIniBE.Pais = valores_linea_actual[3].ToString();
                interfaz_RegIniBE.Identificador_interfaz = valores_linea_actual[4].ToString();
                interfaz_RegIniBE.Nombre_fichero = valores_linea_actual[5].ToString();
                interfaz_RegIniBE.Fecha_ejecucion = DateTime.Parse(Utilitarios.FechaFormatoAAMMDD_BBVA(valores_linea_actual[6]));
                interfaz_RegIniBE.Hora_proceso = Utilitarios.HoraFormatoHHMMSS_BBVA(valores_linea_actual[7]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public InterfazPrefacturas_RegPosBE LlenarEntidad_RegPos(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegPos)
        {
            InterfazPrefacturas_RegPosBE interfazPrefact_RegPosBE = new InterfazPrefacturas_RegPosBE();

            try
            {
                //Ejemplo: "000002	2	8540040514	00010	100006661-001		    330350,20		000000000370000271	     1651,751	KG	"
                interfazPrefact_RegPosBE.Numeral = valores_linea_actual[0].ToString();
                interfazPrefact_RegPosBE.Tipo_registro = valores_linea_actual[1].ToString();
                interfazPrefact_RegPosBE.Numero_documento_compras = valores_linea_actual[2].ToString();
                interfazPrefact_RegPosBE.Posicion_documento_compras = valores_linea_actual[3].ToString();
                interfazPrefact_RegPosBE.Numero_doc_ref_albaran = valores_linea_actual[4].ToString();
                interfazPrefact_RegPosBE.Posicion_fact_import_post_neg = valores_linea_actual[5].ToString();
                interfazPrefact_RegPosBE.Importe_total_posicion_sin_impuestos = Convert.ToDecimal(Utilitarios.DecimalFormato_BBVA(valores_linea_actual[6].ToString()));
                interfazPrefact_RegPosBE.Porcentaje_impuesto = Convert.ToDecimal(Utilitarios.DecimalFormato_BBVA(valores_linea_actual[7].ToString()));
                interfazPrefact_RegPosBE.Numero_material_servicio = valores_linea_actual[8].ToString();
                interfazPrefact_RegPosBE.Cantidad = Convert.ToDecimal(Utilitarios.DecimalFormato_BBVA(valores_linea_actual[9]));
                interfazPrefact_RegPosBE.Unidad_medida_base = valores_linea_actual[10].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            return interfazPrefact_RegPosBE;
        }

        public InterfazReferencias_RegProcBE LlenarEntidad_RegProc(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegProc)
        {
            /*NO SE IMPLEMENTARÁ*/
            throw new NotImplementedException();
        }

        public bool RegistrarInterfaz_RegCab(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE)
        {
            throw new NotImplementedException();
        }

        public bool RegistrarInterfaz_RegIni(ref InterfazPrefacturas_RegIniBE interfaz_RegIniBE)
        {
            string[] result_valores;
            bool result = false;
            bool result_importacion = false;
            try
            {
                //Registrando en BD la entidad
                interfaz_RegIniBE.Ruta_fichero_detino = GlobalVariables.Ruta_fichero_detino_Pref;
                interfaz_RegIniBE.Procesado = 1;                     /* AUN NO SE PROCESA DEBE IR "0" */
                interfaz_RegIniBE.Interfaz.Idinterface = 4;          /* VALORES FIJOS: INTERFAZ PREFACTURA(4) */
                interfaz_RegIniBE.Tiporegistro.Idtiporegistro = 1;   /* VALORES FIJOS */

                /*  Registrando el "REGISTRO INICIAL" */
                result_valores = (new InterfazPrefacturaDAL()).RegistrarRegIni(ref interfaz_RegIniBE).Split(';');
                if (int.Parse(result_valores[0]) != 0)
                {

                    result = true;
                }
                else
                {
                    /* Ocurrió un error en el registro inicial */
                    interfaz_RegIniBE.Id_error = int.Parse(result_valores[1]);
                }
                return result;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }

       }

        /*BORRAR ESTE MÉTODO DE ABAJO, LO USÉ DE MODELO DE OTRA INTERFAZ */
        public bool RegistrarInterfaz_RegIni(ref InterfazSuministros_RegIniBE interfaz_RegIniBE)
        {
            string[] result_valores;
            bool result = false;
            bool result_importacion = false;
            try
            {
                //Registrando en BD la entidad
                interfaz_RegIniBE.Ruta_fichero_detino = GlobalVariables.Ruta_fichero_detino_Sum;
                interfaz_RegIniBE.Procesado = 0;                     /* AUN NO SE PROCESA DEBE IR "0" */
                interfaz_RegIniBE.Interfaz.Idinterface = 2;          /* VALORES FIJOS: Interfaz Suministros(2) */
                interfaz_RegIniBE.Tiporegistro.Idtiporegistro = 1;   /* VALORES FIJOS */

                /*  Registrando el "REGISTRO INICIAL" */
                result_valores = (new InterfazSuministrosDAL()).RegistrarRegIni(ref interfaz_RegIniBE).Split(';');
                if (int.Parse(result_valores[0]) != 0)
                {
                    //Obteniendo el "Idregini" generado
                    interfaz_RegIniBE.Idregini = int.Parse(result_valores[0]);

                    //Registrando cada una de la(s) CABECERA(s) del pedido
                    foreach (var cab in interfaz_RegIniBE.LstInterfazSuministros_RegCabBE)
                    {
                        //Registrando CABECERA(s) (cabecera del pedido)
                        result_valores = (new InterfazSuministrosDAL()).RegistrarCab(ref interfaz_RegIniBE, cab).Split(';');
                        if (int.Parse(result_valores[0]) != 0)
                        {
                            //Obteniendo el "IdCab" generado
                            cab.Idcab = int.Parse(result_valores[0]);

                            //Registrando POSICIONES de la cabecera actual
                            result_valores = (new InterfazSuministrosDAL()).RegistrarPos(cab).Split(';');
                            if (int.Parse(result_valores[0]) != 0)
                            {
                                //Todo ok
                                result_importacion = true;
                            }
                            else
                            {
                                /*Ocurrió un error*/
                            }
                        }
                        else
                        {
                            /*Ocurrió un error en alguno o algunos de los "n" registros del proceso */
                            interfaz_RegIniBE.Id_error = int.Parse(result_valores[1]);
                        }
                    }

                    //GENERAR "TODOS" los Pedidos del proceso actual [MUY IMPORTANTE]
                    if (result_importacion == true)
                    {

                        result_valores = (new InterfazSuministrosDAL()).GenerarPedidosInterfazSum(interfaz_RegIniBE.Idregini).Split(';');
                        if (int.Parse(result_valores[0]) != 0)
                        {

                            //Obtener lista de los pedidos que se acaban de generar
                            List<InterfazSuministros_PedidoBE> lstPedidos = new List<InterfazSuministros_PedidoBE>();
                            lstPedidos = (new InterfazSuministrosDAL()).ObtenerPedidosGenerados_Log(interfaz_RegIniBE.Idregini);

                            //Enviar correo bien detallado al ejecutivo e interesados sobre la generación de los pedidos
                            base.EnviarCorreoElectronico((new InterfazSuministrosDAL()).ObtenerDestinatariosReporteInterfaz(2),
                                "", /* Emails con copia */
                                "Reporte de pedidos importados BBVA - Interfaz Suministros",
                                (lstPedidos[0].RUTA_FICHERO + lstPedidos[0].NOMBRE_FICHERO_DESTINO),
                                (new InterfazSuministrosBL()).GenerarReporte_GeneracionPedidos(lstPedidos));
                            result = true;
                        }
                        else
                        {
                            /*Ocurrió un error*/
                            interfaz_RegIniBE.Id_error = int.Parse(result_valores[1]);
                        }
                    }
                }
                else
                {
                    /* Ocurrió un error en el registro inicial */
                    interfaz_RegIniBE.Id_error = int.Parse(result_valores[1]);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



    }
}
