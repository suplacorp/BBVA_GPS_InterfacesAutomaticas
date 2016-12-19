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
                interfazPreFact_RegCabBE.Fecha_factura = DateTime.ParseExact(valores_linea_actual[8], "ddMMyyyy", CultureInfo.InvariantCulture);
                interfazPreFact_RegCabBE.Ejercicio = valores_linea_actual[9].ToString();
                interfazPreFact_RegCabBE.Procesado = true;
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
                //interfaz_RegIniBE.Numero_registros_tipo3_fin = valores_linea_actual[4].ToString();
                interfaz_RegIniBE.Numero_registros_tipo3_fin = "00000"; /*LO PUSE ESTÁTICO, HASTA QUE EL BBVA RESPONDA SI VIAJARÁ O NO.*/
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
                interfazPrefact_RegPosBE.Procesado = 1;
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
                    //Obteniendo el "Idregini" generado
                    interfaz_RegIniBE.Idregini = int.Parse(result_valores[0]); 

                    //Registrando cada una de la(s) CABECERA(s) del pedido
                    foreach (var cab in interfaz_RegIniBE.LstInterfazPrefacturas_RegCabBE)
                    {
                        //Registrando CABECERA(s) (cabecera del pedido)
                        result_valores = (new InterfazPrefacturaDAL()).RegistrarCab(ref interfaz_RegIniBE, cab).Split(';');
                        if (int.Parse(result_valores[0]) != 0)
                        {
                            //Obteniendo el "IdCab" generado
                            cab.Idcab = int.Parse(result_valores[0]);

                            //Registrando POSICIONES de la cabecera actual
                            result_valores = (new InterfazPrefacturaDAL()).RegistrarPos(cab).Split(';');
                            if (int.Parse(result_valores[0]) != 0)
                            {
                                //Todo ok
                                result_importacion = true;
                            }
                        }
                        else
                        {
                            /*Ocurrió un error en alguno o algunos de los "n" registros del proceso */
                            interfaz_RegIniBE.Id_error = int.Parse(result_valores[1]);
                        }
                    }
                    result = true;
                }
                else{
                    /* OCURRIÓ UN ERROR EN EL REGISTRO INICIAL */
                    interfaz_RegIniBE.Id_error = int.Parse(result_valores[1]);

                    base.EnviarCorreoElectronico(
                        new InterfazExpedicionesDAL().ObtenerDestinatariosReporteInterfaz(4),"", "[ERROR - Int. Prefactura]", "",
                        (base.FormatearMensajeError_HTML(null, interfaz_RegIniBE.Id_error, "Int. Prefactura")), null);
                }
            }
            catch (Exception ex) {
                /*NOTIFICACIÓN [ERROR] POR EMAIL*/
                base.EnviarCorreoElectronico(
                    new InterfazExpedicionesDAL().ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Prefacturas), "", "[ERROR - Int. Prefactura]", "",
                    (base.FormatearMensajeError_HTML(ex, 0, "Int. Prefactura")), null);
                /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                Console.WriteLine(base.FormatearMensajeError_CONSOLA(ex, 0, "Int. Prefactura"));
                /* ELIMINACIÓN DE REGISTRO INICIAL, "RESET DE TODO" EL PROCESO*/
                (new InterfazPrefacturaDAL()).Resetear_Proceso_Interfaz((int)GlobalVariables.Interfaz.Prefacturas, interfaz_RegIniBE.Idregini);
            }
            return result;
        }

        public bool NotificarInterfazPreFactura(InterfazPrefacturas_RegIniBE interfazPreFact_RegIniBE) {
            bool result = false;

            try
            {
                /* CONSULTAR LAS DESCRIPCIONES DE [TODOS] LOS ARTÍCULOS NEGOCIADOS CON BBVA PARA ENVIAR POR CORREO EL REPORTE CON LAS DESCRIPCIONES CORRECTAS
                * SE HACE ÉSTO YA QUE EN ESTE PUNTO NO CONTAMOS CON LOS "IDARTÍCULOS" NI "DESCRIPCIONES", SOLO CON EL CODIGOEXTERNO QUE VINIERON DE LA INTERFAZ
                */
                ListaArticulosNegociadosBBVA lstArticulosNegociados = new ListaArticulosNegociadosBBVA();
                lstArticulosNegociados = (new UtilBL()).ObtenerListaArticulosNegociadosBBVA();
                foreach (var cab in interfazPreFact_RegIniBE.LstInterfazPrefacturas_RegCabBE)
                {
                    foreach (var pos in cab.LstInterfazPrefacturas_RegPosBE)
                    {
                        pos.Idarticulo = (lstArticulosNegociados.Exists(x => x.Key == Int32.Parse(pos.Numero_material_servicio)) ? lstArticulosNegociados.Find(x => x.Key == Int32.Parse(pos.Numero_material_servicio)).Value.Idarticulo : 0);
                        pos.Descripcion_art = (lstArticulosNegociados.Exists(x => x.Key == Int32.Parse(pos.Numero_material_servicio)) ? lstArticulosNegociados.Find(x => x.Key == Int32.Parse(pos.Numero_material_servicio)).Value.DescripcionArticulo : "NO SE ENCONTRÓ EL ARTÍCULO");
                    }
                }

                //ENVIAR CORREO BIEN DETALLADO AL EJECUTIVO E INTERESADOS SOBRE LA GENERACIÓN DE LOS PEDIDOS
                base.EnviarCorreoElectronico((new InterfazPrefacturaDAL()).ObtenerDestinatariosReporteInterfaz(2),
                    "", /* Emails con copia */
                    "[Int. Prefactura] Reporte.",
                    (interfazPreFact_RegIniBE.Ruta_fichero_detino + interfazPreFact_RegIniBE.Nombre_fichero_detino),
                    (new InterfazPrefacturaBL()).GenerarReporte_InterfazPreFactura(interfazPreFact_RegIniBE), null);

                result = true;
            }
            catch (Exception ex) {
                throw;
            }
            return result;
        }

        public string GenerarReporte_InterfazPreFactura(InterfazPrefacturas_RegIniBE interfazPreFact_RegIniBE) {

            StringBuilder correoReporte = new StringBuilder();
            string correoAux = "";

            decimal importe_sin_impuesto_xpos = 0;
            decimal importe_impuesto_xpos = 0;
            decimal importe_total_con_impuesto_xpos = 0;

            decimal total_sin_impuesto_xpos = 0;
            decimal total_impuesto_xpos = 0;
            decimal total_general_xpos = 0;

            decimal total_sin_impuesto_xcab = 0;
            decimal total_impuesto_xcab = 0;
            decimal total_general_xcab = 0;

            try
            {
                if (interfazPreFact_RegIniBE.LstInterfazPrefacturas_RegCabBE.Count > 0)
                {
                    /*ESCRIBIENDO EL [REGISTRO INICIAL] DE LA INTERFAZ DE PREFACTURA */
                    correoAux =
                    "<style type='text/css'>" + "\r\n" +
                    ".espacioIzquierda { " + "\r\n" +
                    "   width: 150px; " + "\r\n" +
                    "} " + "\r\n" +
                    ".tamanoTabla { " + "\r\n" +
                    "   width: 800px; " + "\r\n" +
                    "} " + "\r\n" +
                    "</style > " + "\r\n" +
                    "<br/><br/> " + "\r\n" +
                    "<b><u>Información General del Proceso</u></b>" + "\r\n" +
                    " " + "\r\n" +
                    "<table> " + "\r\n" +
                    "   <tr> " + "\r\n" +
                    "       <td class='espacioIzquierda'>Numeral</td> " + "\r\n" +
                    "       <td>" + interfazPreFact_RegIniBE.Numeral.ToString() + "</td> " + "\r\n" +
                    "   </tr> " + "\r\n" +
                    "   <tr> " + "\r\n" +
                    "       <td class='espacioIzquierda'>País</td> " + "\r\n" +
                    "       <td>" + interfazPreFact_RegIniBE.Pais.ToString() + "</td> " + "\r\n" +
                    "   </tr> " + "\r\n" +
                    "   <tr>" + "\r\n" +
                    "        <td>Fecha Ejecución</td> " + "\r\n" +
                    "        <td>" + interfazPreFact_RegIniBE.Fecha_ejecucion.ToString() + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Hora Proceso</td> " + "\r\n" +
                    "        <td>" + interfazPreFact_RegIniBE.Hora_proceso.ToString() + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Archivo</td> " + "\r\n" +
                    "        <td>" + interfazPreFact_RegIniBE.Nombre_fichero_detino.ToString() + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Número total de registros</td> " + "\r\n" +
                    "        <td>" + Utilitarios.CompletarConCeros_Izquierda(interfazPreFact_RegIniBE.Numero_total_registros_fin,6) + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Número de registros cabecera</td> " + "\r\n" +
                    "        <td>" + Utilitarios.CompletarConCeros_Izquierda(interfazPreFact_RegIniBE.Numero_registros_cab_fin,6) + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Número de registros posición</td> " + "\r\n" +
                    "        <td>" + Utilitarios.CompletarConCeros_Izquierda(interfazPreFact_RegIniBE.Numero_registros_pos_fin,6) + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "</table> " + "\r\n" +
                    "<br /> " + "\r\n" +
                    "<b><u>Cabeceras generadas:</u></b>" + "\r\n" +
                    " " + "\r\n" +
                    " " + "\r\n" +
                    "";
                    correoReporte.Append(correoAux);

                    /* ESCRIBIENDO CADA [REGISTRO CABECERA] DE LA INTERFAZ DE EXPEDICIONES */
                    foreach (var cab in interfazPreFact_RegIniBE.LstInterfazPrefacturas_RegCabBE)
                    {
                        correoAux = "<hr /> " + "\r\n" +
                         "<table border='0' class='tamanoTabla'> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td colspan = '2' align='center' style='background-color:dodgerblue'><b>[" + cab.LstInterfazPrefacturas_RegPosBE.Count.ToString() + " items] </b></td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td class='espacioIzquierda'>IdCab</td> " + "\r\n" +
                         "        <td>" + cab.Idcab.ToString() + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td><b>Numeral</b></td> " + "\r\n" +
                         "        <td><b>" + Utilitarios.CompletarConCeros_Izquierda(cab.Numeral, 6) + "</b></td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Tipo de registro</td> " + "\r\n" +
                         "        <td>" + cab.Tipo_registro + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Número documento preliminar</td> " + "\r\n" +
                         "        <td>" + cab.Numero_documento_preliminar + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Sociedad</td> " + "\r\n" +
                         "        <td>" + cab.Sociedad + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Moneda</td> " + "\r\n" +
                         "        <td>" + cab.Clave_moneda + " (S/.)</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Fact. Importe Positivo Negativo</td> " + "\r\n" +
                         "        <td>" + cab.Fact_importe_posit_negat + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td><b>Importe Total Fact. SIN IMPUESTOS</b></td> " + "\r\n" +
                         "        <td><b>" + cab.Importe_total_factura_sin_impuestos.ToString("N") + "</b></td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td><b>Importe Total IMPUESTOS</b></td> " + "\r\n" +
                         "        <td><b>" + cab.Importe_total_impuestos.ToString("N") + "</b></td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Fecha Factura</td> " + "\r\n" +
                         "        <td>" + cab.Fecha_factura.ToString("ddMMyyyy") + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "        <td>Ejercicio</td> " + "\r\n" +
                         "        <td>" + cab.Ejercicio + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "</table>";
                        correoReporte.Append(correoAux);

                        correoAux =
                            "<table border='1' class='tamanoTabla'> " + "\r\n" +
                            "<tr style='text-align:center;background-color:#A5A5A5;font-weight:bold'>" + "\r\n" +
                            "    <td>Numeral</td> " + "\r\n" +
                            "    <td>Nro. doc. compras</td> " + "\r\n" +
                            "    <td>Posición doc. compras</td> " + "\r\n" +
                            "    <td>Numero doc. Ref Albaran (GUIAS)</td> " + "\r\n" +
                            "    <td>Posición Fact. Import. Post. Negit.</td> " + "\r\n" +
                            "    <td>Importe total Posicion SIN IMPUESTOS</td> " + "\r\n" +
                            "    <td>% Impuesto</td> " + "\r\n" +
                            "    <td>Importe IMPUESTOS</td> " + "\r\n" +
                            "    <td>Importe total CON IMPUESTOS</td> " + "\r\n" +
                            "    <td>Número material servicio</td> " + "\r\n" +
                            "    <td>IDArtículo</td> " + "\r\n" +
                            "    <td>Desc. Artículo</td> " + "\r\n" +
                            "    <td>Cantidad</td> " + "\r\n" +
                            "    <td>Unidad Medida Base</td> " + "\r\n" +
                            "</tr>";
                        correoReporte.Append(correoAux);

                        /*ESCRIBIENDO CADA [POSICIÓN DE LA CABECERA ACTUAL] DE LA INTERFAZ DE EXPEDICIONES */
                        foreach (var pos in cab.LstInterfazPrefacturas_RegPosBE)
                        {

                            importe_sin_impuesto_xpos = pos.Importe_total_posicion_sin_impuestos;
                            importe_impuesto_xpos = (pos.Importe_total_posicion_sin_impuestos * (pos.Porcentaje_impuesto / 100));
                            importe_total_con_impuesto_xpos = (pos.Importe_total_posicion_sin_impuestos * ((pos.Porcentaje_impuesto / 100) + 1));

                            correoAux =
                           "<tr> " + "\r\n" +
                           "   <td align='center'>" + Utilitarios.CompletarConCeros_Izquierda(pos.Numeral.ToString(), 6) + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Numero_documento_compras + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Posicion_documento_compras + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Numero_doc_ref_albaran + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Posicion_fact_import_post_neg + "</td> " + "\r\n" +
                           "   <td align='right'>" + importe_sin_impuesto_xpos.ToString("N") + "</td> " + "\r\n" +
                           "   <td align='right'>" + pos.Porcentaje_impuesto + "</td> " + "\r\n" +
                           "   <td align='right'>" + importe_impuesto_xpos.ToString("N") + "</td> " + "\r\n" +
                           "   <td align='right'>" + importe_total_con_impuesto_xpos.ToString("N") + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Numero_material_servicio + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Idarticulo + "</td> " + "\r\n" +
                           "   <td align='left'>" + pos.Descripcion_art + "</td> " + "\r\n" +
                           "   <td align='right'>" + pos.Cantidad + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Unidad_medida_base + "</td> " + "\r\n" +
                           "</tr>";
                            correoReporte.Append(correoAux);

                            total_sin_impuesto_xpos = total_sin_impuesto_xpos + importe_sin_impuesto_xpos;
                            total_impuesto_xpos = total_impuesto_xpos + importe_impuesto_xpos;
                            total_general_xpos = total_general_xpos + importe_total_con_impuesto_xpos;
                        }

                        /*TOTALES POR CABECERA ACTUAL*/
                        correoAux =
                           "<tr style='text-align:center;background-color:yellow;font-weight:bold'> " + "\r\n" +
                           "   <td align='Left' colspan='5'>Totales</td> " + "\r\n" +
                           "   <td align='right'>" + total_sin_impuesto_xpos.ToString("N") + "</td> " + "\r\n" +
                           "   <td align='right'></td> " + "\r\n" +
                           "   <td align='right'>" + total_impuesto_xpos.ToString("N") + "</td> " + "\r\n" +
                           "   <td align='right'>" + total_general_xpos.ToString("N") + "</td> " + "\r\n" +
                           "</tr>";
                        correoReporte.Append(correoAux);

                        /*CERRANDO CABECERA ACTUAL*/
                        correoAux = "</table> " + "\r\n";
                        correoReporte.Append(correoAux);

                        /*VARIABLES ACUMULADORES DE TODO EL PROCESO*/
                        total_sin_impuesto_xcab = total_sin_impuesto_xcab + total_sin_impuesto_xpos;
                        total_impuesto_xcab = total_impuesto_xcab + total_impuesto_xpos;
                        total_general_xcab = total_general_xcab + total_general_xpos;

                        /*RESET DE VARIABLES ACUMULADORES POR CABECERA*/
                        total_sin_impuesto_xpos = 0;
                        total_impuesto_xpos = 0;
                        total_general_xpos = 0;
                    }
                    correoAux = "<br /><br /><font style='background-color:yellow;font-size:x-large'></b></font>";
                    correoReporte.Append(correoAux);


                    correoAux = "<hr /> " + "\r\n" +
                         "<table border='1' class='tamanoTabla' style='background-color:yellow;font-size:large'> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td class='espacioIzquierda'>Cant. Prefacturas</td> " + "\r\n" +
                         "        <td>" + interfazPreFact_RegIniBE.LstInterfazPrefacturas_RegCabBE.Count + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td class='espacioIzquierda'>Moneda</td> " + "\r\n" +
                         "        <td>" + interfazPreFact_RegIniBE.LstInterfazPrefacturas_RegCabBE[0].Clave_moneda + " (S/.)</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td class='espacioIzquierda'>Importe total sin impuestos</td> " + "\r\n" +
                         "        <td>" + total_sin_impuesto_xcab.ToString("N") + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td class='espacioIzquierda'>Importe total impuestos</td> " + "\r\n" +
                         "        <td>" + total_impuesto_xcab.ToString("N") + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td class='espacioIzquierda'>Importe total con impuestos</td> " + "\r\n" +
                         "        <td>" + total_general_xcab.ToString("N") + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "</table>";
                    correoReporte.Append(correoAux);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return correoReporte.ToString();
        }

    }
}
