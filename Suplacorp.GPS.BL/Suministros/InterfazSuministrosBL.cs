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

namespace Suplacorp.GPS.BL
{
    public class InterfazSuministrosBL : BaseBL<InterfazSuministros_RegIniBE>, 
                                            IInterfazRegIniBL<InterfazSuministros_RegIniBE, InterfazReferencias_RegProcBE>, 
                                            IInterfazRegCabBL<InterfazSuministros_RegIniBE, InterfazSuministros_RegCabBE>,
                                            IInterfazRegPosBL<InterfazSuministros_RegIniBE, InterfazSuministros_RegCabBE, InterfazSuministros_RegPosBE>
    {
        #region Constructor
        public InterfazSuministrosBL()
        {

        }
        #endregion

        public InterfazSuministros_RegIniBE LeerFicheroInterfaz(string nombre_fichero, string ruta_fichero_lectura, List<ValidacionInterfazBE> lstValidacion)
        {
            //EN PROCESO
            InterfazSuministros_RegIniBE interfazSuministros_RegIniBE = new InterfazSuministros_RegIniBE();
            string linea_actual;
            String[] valores_linea_actual;
            string idTipoDetalle_TipoRegistro;

            string codigo_cesta_actual = "";

            try
            {
                //1) Validaciones - registro de control inicial para el inicio del fichero
                List<ValidacionInterfazBE> lstValidacionRegistroInicial = new List<ValidacionInterfazBE>();
                lstValidacionRegistroInicial = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 0);
                
                //2) Validaciones - Registro de cabecera
                List<ValidacionInterfazBE> lstValidacionRegistroCabecera = new List<ValidacionInterfazBE>();
                lstValidacionRegistroCabecera = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 1);

                //3) Validaciones - Registro de Posición
                List<ValidacionInterfazBE> lstValidacionRegistroPosicion = new List<ValidacionInterfazBE>();
                lstValidacionRegistroPosicion = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 2);

                //4) Validaciones - Registro de control para el fin del fichero
                List<ValidacionInterfazBE> lstValidacionRegistroFin = new List<ValidacionInterfazBE>();
                lstValidacionRegistroFin = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 9);

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
                                    LlenarEntidad_RegIni(ref interfazSuministros_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroInicial);
                                    break;
                                case "1": //2) Registro de CABECERA (cabecera del pedido)

                                    interfazSuministros_RegIniBE.LstInterfazSuministros_RegCabBE.Add(LlenarEntidad_RegCab(ref interfazSuministros_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroCabecera));
                                    break;
                                case "2": //3) Registro de POSICIÓN
                                    var lastIndexCab = interfazSuministros_RegIniBE.LstInterfazSuministros_RegCabBE.Count - 1;
                                    interfazSuministros_RegIniBE.LstInterfazSuministros_RegCabBE[lastIndexCab].LstInterfazSuministros_RegPosBE.Add(LlenarEntidad_RegPos(ref interfazSuministros_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroPosicion));

                                    break;
                                case "9": //4) Registro de control para el FIN del fichero
                                    LlenarEntidad_RegFin(ref interfazSuministros_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroFin);
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
            return interfazSuministros_RegIniBE;
        }

        #region LlenarEntidades
        public void LlenarEntidad_RegIni(ref InterfazSuministros_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegIni)
        {
            try
            {
                //Ejemplo: "000000	0	S	PE	SU	PE_OL1_SUMIN	27102015	094631"
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

        public InterfazSuministros_RegCabBE LlenarEntidad_RegCab(ref InterfazSuministros_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegCab)
        {
            InterfazSuministros_RegCabBE interfazReferencias_RegCabBE = new InterfazSuministros_RegCabBE();

            try
            {
                //Ejemplo: "000001	1	0100084029	Prueba Direccion 2	MX11003313	CALLE DE PRUEBA 1		65984	CHIHUAHUA		COL	MX			CHIHUAHUA			MX11		"
                interfazReferencias_RegCabBE.Numeral = valores_linea_actual[0].ToString();
                interfazReferencias_RegCabBE.Tipo_registro = valores_linea_actual[1].ToString();
                interfazReferencias_RegCabBE.Codigo_cesta = valores_linea_actual[2].ToString();
                interfazReferencias_RegCabBE.Nombre_destinatario = valores_linea_actual[3].ToString();
                interfazReferencias_RegCabBE.Desc_unid_ofi_estab = valores_linea_actual[4].ToString();
                interfazReferencias_RegCabBE.Calle_direccion = valores_linea_actual[5].ToString();
                interfazReferencias_RegCabBE.Nro_direccion_entrega = valores_linea_actual[6].ToString();
                interfazReferencias_RegCabBE.Codigo_postal = valores_linea_actual[7].ToString();
                interfazReferencias_RegCabBE.Provincia = valores_linea_actual[8].ToString();
                interfazReferencias_RegCabBE.Apartado_alternativo = valores_linea_actual[9].ToString();
                interfazReferencias_RegCabBE.Region_departamento = valores_linea_actual[10].ToString();
                interfazReferencias_RegCabBE.Pais = valores_linea_actual[11].ToString();
                interfazReferencias_RegCabBE.Telefono_contacto = valores_linea_actual[12].ToString();
                interfazReferencias_RegCabBE.Usuario = valores_linea_actual[13].ToString();
                interfazReferencias_RegCabBE.Colonia_comuna_planta_distrito = valores_linea_actual[14].ToString();
                interfazReferencias_RegCabBE.Entre_calle1 = valores_linea_actual[15].ToString();
                interfazReferencias_RegCabBE.Entre_calle2 = valores_linea_actual[16].ToString();
                interfazReferencias_RegCabBE.Centro = valores_linea_actual[17].ToString();
                interfazReferencias_RegCabBE.Urgente = valores_linea_actual[18].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            return interfazReferencias_RegCabBE;
        }

        public InterfazSuministros_RegPosBE LlenarEntidad_RegPos(ref InterfazSuministros_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegPos)
        {
            InterfazSuministros_RegPosBE interfazSuministros_RegPosBE = new InterfazSuministros_RegPosBE();

            try
            {
                //Ejemplo: "000002	2	0100084029  	P	8500057740	00010	000000000210000239	25112015	MX11003313	2,000         	PAQ	"
                interfazSuministros_RegPosBE.Numeral = valores_linea_actual[0].ToString();
                interfazSuministros_RegPosBE.Tipo_registro = valores_linea_actual[1].ToString();
                interfazSuministros_RegPosBE.Codigo_cesta = valores_linea_actual[2].ToString();
                interfazSuministros_RegPosBE.Movimiento_clase_doc = valores_linea_actual[3].ToString();
                interfazSuministros_RegPosBE.Pedido_ref_reserva = valores_linea_actual[4].ToString();
                interfazSuministros_RegPosBE.Posicion_ped_reserva = valores_linea_actual[5].ToString();
                interfazSuministros_RegPosBE.Material = valores_linea_actual[6].ToString();
                interfazSuministros_RegPosBE.Fecha_entrega = DateTime.Parse(Utilitarios.FechaFormatoAAMMDD_BBVA(valores_linea_actual[7]));
                interfazSuministros_RegPosBE.Centro_coste = valores_linea_actual[8].ToString();
                interfazSuministros_RegPosBE.Cantidad_pedido_reserva = decimal.Parse(Utilitarios.DecimalFormato_BBVA(valores_linea_actual[9])); /*DECIMAL*/
                interfazSuministros_RegPosBE.Unidad_medida_pedido = valores_linea_actual[10].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            return interfazSuministros_RegPosBE;
        }

        public void LlenarEntidad_RegFin(ref InterfazSuministros_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegFin)
        {
            try
            {
                //Ejemplo: "000020	9	00003	00017	00000";
                interfaz_RegIniBE.Numero_total_registros_fin = valores_linea_actual[0].ToString();
                interfaz_RegIniBE.Tipo_registro_fin = valores_linea_actual[1].ToString();
                interfaz_RegIniBE.Numero_registros_cab_fin = valores_linea_actual[2].ToString();
                interfaz_RegIniBE.Numero_registros_pos_fin = valores_linea_actual[3].ToString();
                interfaz_RegIniBE.Numero_registros_tipo3_fin = valores_linea_actual[4].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public InterfazReferencias_RegProcBE LlenarEntidad_RegProc(ref InterfazSuministros_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegProc)
        {
            //NO SE IMPLEMENTARÁ (SOLO ES PARA LA INTERFAZ DE REFERENCIAS)
            throw new NotImplementedException();
        }

        #endregion


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
                interfaz_RegIniBE.Interfaz.Idinterface = 2;          /* VALORES FIJOS: Interfaz Suministros   */
                interfaz_RegIniBE.Tiporegistro.Idtiporegistro = 1;   /* VALORES FIJOS    */

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
                            else {
                                /*Ocurrió un error*/
                            }
                        }
                        else
                        {
                            /*Ocurrió un error en alguno o algunos de los "n" registros del proceso */
                                interfaz_RegIniBE.Id_error = int.Parse(result_valores[1]);
                        }
                    }

                    //Generar TODOS los Pedidos del proceso [MUY IMPORTANTE]
                    if (result_importacion == true){
                        result_valores = (new InterfazSuministrosDAL()).GenerarPedidosInterfazSum(interfaz_RegIniBE.Idregini).Split(';');
                        if (int.Parse(result_valores[0]) != 0)
                        {
                            //Enviar correo bien detallado al ejecutivo e interesados sobre la generación de los pedidos
                            //List<object> _lstObjectos = new List<object>();
                            //_lstObjectos = (new InterfazSuministrosDAL()).ObtenerPedidosGenerados_Log(interfaz_RegIniBE.Idregini);

                            result = true;
                        }
                        else
                        {
                            /*Ocurrió un error*/
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

        public bool RegistrarInterfaz_RegCab(ref InterfazSuministros_RegIniBE interfaz_RegIniBE)
        {
            throw new NotImplementedException();
        }


        public string GenerarReporte_GeneracionPedidos(List<InterfazSuministros_PedidoBE> lstPedidos) {
            NameValueCollection lstCabeceras = new NameValueCollection();
            StringBuilder correoReporte = new StringBuilder();
            List<InterfazSuministros_PedidoBE> lstPedidos_xCabecera;
            int indexCabecera = 1;
            string correoAux = "";
            double total_pedido = 0;
            double total_proceso = 0;

            try {

                if (lstPedidos.Count > 0)
                {
                    /* Obteniendo todas las cabeceras de los pedidos generados */
                    foreach (var cab in lstPedidos){
                        lstCabeceras.Add(cab.GetType().GetProperty("IDCAB").GetValue(cab, null).ToString(), "");
                    }

                    correoAux = 
                    "<style type='text/css'>" + "\r\n" +
                    ".error {" + "\r\n" +
                    "   background-color:red;" + "\r\n" +
                    "   color: white " + "\r\n" +
                    "} " + "\r\n" +
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
                    "       <td class='espacioIzquierda'>Nro.Proceso</td> " + "\r\n" +
                    "       <td>"+ lstPedidos[0].NRO_PROCESO.ToString() + "</td> " + "\r\n" +
                    "   </tr> " + "\r\n" +
                    "   <tr>" + "\r\n" +
                    "        <td>Fecha Ejecución</td> " + "\r\n" +
                    "        <td>" + lstPedidos[0].FECHA_EJECUCION.ToString() + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Hora Proceso</td> " + "\r\n" +
                    "        <td>" + lstPedidos[0].HORA_PROCESO.ToString() + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Archivo</td> " + "\r\n" +
                    "        <td>" + lstPedidos[0].NOMBRE_FICHERO_DESTINO.ToString() + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Fecha Registro</td> " + "\r\n" +
                    "        <td>" + lstPedidos[0].FECHA_REGISTRO.ToString() + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "</table> " + "\r\n" +
                    "<br /> " + "\r\n" +
                    "<b><u>Pedidos Generados:</u></b>" + "\r\n" +
                    " " + "\r\n" +
                    " " + "\r\n" +
                    "";
                    correoReporte.Append(correoAux);

                    /* CABECERAS DE PEDIDOS */
                    foreach (var idcab in lstCabeceras) {

                        lstPedidos_xCabecera = new List<InterfazSuministros_PedidoBE>();
                        lstPedidos_xCabecera = lstPedidos.FindAll(x => x.IDCAB.ToString() == idcab.ToString());

                        correoAux = "<hr /> " + "\r\n" +
                        "<table border='0' class='tamanoTabla'> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td colspan = '2' align='center' style='background-color:dodgerblue'><b>["+ indexCabecera.ToString() + "/"+ lstCabeceras.Count.ToString() + "] Pedido </b></td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td class='espacioIzquierda'>IdCab</td> " + "\r\n" +
                        "        <td>"+ lstPedidos_xCabecera[0].IDCAB.ToString() + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td><b>Num.Pedido</b></td> " + "\r\n" +
                        "        <td><b>" + lstPedidos_xCabecera[0].PEDIDO_REF_RESERVA.ToString() + "</b></td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td>Código Cesta</td> " + "\r\n" +
                        "        <td>" + lstPedidos_xCabecera[0].CODIGO_CESTA.ToString() + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td>Nombre Destinatario</td> " + "\r\n" +
                        "        <td>" + lstPedidos_xCabecera[0].NOMBRE_DESTINATARIO.ToString() + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td>Centro de Costo</td> " + "\r\n" +
                        "        <td>" + lstPedidos_xCabecera[0].DESC_UNID_OFI_ESTAB.ToString() + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td>Dirección</td> " + "\r\n" +
                        "        <td>" + lstPedidos_xCabecera[0].CALLE_DIRECCION.ToString() + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td>Departamento</td> " + "\r\n" +
                        "        <td>" + lstPedidos_xCabecera[0].REGION_DEPARTAMENTO.ToString() + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td>Provincia</td> " + "\r\n" +
                        "        <td>" + lstPedidos_xCabecera[0].PROVINCIA.ToString() + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td>Distrito</td> " + "\r\n" +
                        "        <td>" + lstPedidos_xCabecera[0].COLONIA_COMUNA_PLANTA_DISTRITO.ToString() + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr> " + "\r\n" +
                        "        <td>Urgente</td> " + "\r\n" +
                        "        <td>" + lstPedidos_xCabecera[0].URGENTE.ToString() + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "    <tr class='"+ (lstPedidos_xCabecera[0].MENSAJE_ERROR_CAB.ToString().Length > 0 ? "error" : "") +"'> " + "\r\n" +
                        "        <td>Mensaje Error</td> " + "\r\n" +
                        "        <td>" + lstPedidos_xCabecera[0].MENSAJE_ERROR_CAB.ToString() + "</td> " + "\r\n" +
                        "    </tr> " + "\r\n" +
                        "</table>";
                        correoReporte.Append(correoAux);

                        correoAux = 
                            "<table border='1' class='tamanoTabla'> " + "\r\n" +
                            "<tr style='text-align:center;background-color:#A5A5A5;font-weight:bold'> " + "\r\n" +
                            "    <td>IDPos</td> " + "\r\n" +
                            "    <td>IDArtículo</td> " + "\r\n" +
                            "    <td>Cod. Externo</td> " + "\r\n" +
                            "    <td>Cantidad</td> " + "\r\n" +
                            "    <td>Precio</td> " + "\r\n" +
                            "    <td>Procesado</td> " + "\r\n" +
                            "    <td>Mensaje Error</td> " + "\r\n" +
                            "</tr>";
                        correoReporte.Append(correoAux);

                        foreach (var pos in lstPedidos_xCabecera) {

                            total_pedido = total_pedido + (pos.CANTIDAD_PEDIDO_RESERVA * pos.PRECIO);
                            correoAux = 
                            "<tr class='"+(pos.MENSAJE_ERROR_POS.ToString().Length > 0 ? "error": "") +"'> " + "\r\n" +
                            "   <td align='center'>" + pos.IDPOS.ToString() + "</td> " + "\r\n" +
                            "   <td align='center'>" + pos.IDARTICULO.ToString() + "</td> " + "\r\n" +
                            "   <td align='center'>" + pos.MATERIAL.ToString() + "</td> " + "\r\n" +
                            "   <td align='right'>" + pos.CANTIDAD_PEDIDO_RESERVA.ToString() + "</td> " + "\r\n" +
                            "   <td align='right'>" + pos.PRECIO.ToString() + "</td> " + "\r\n" +
                            "   <td align='center'>" + pos.PROCESADO_POS.ToString() + "</td> " + "\r\n" +
                            "   <td>" + pos.MENSAJE_ERROR_POS.ToString() + "</td> " + "\r\n" +
                            "</tr>";
                            correoReporte.Append(correoAux);
                        }
                        /*Total por cabecera */
                        correoAux = 
                            "<tr> " + "\r\n" +
                            "        <td colspan='3'><b>Total</b></td> " + "\r\n" +
                            "        <td colspan='2' align='right'><b>S/. "+ total_pedido.ToString() + "</b></td> " + "\r\n" +
                            "    </tr> " + "\r\n" +
                            "</table> " + "\r\n" +
                            "<hr />";
                        total_proceso = total_proceso + total_pedido;
                        total_pedido = 0;
                        indexCabecera++;
                        correoReporte.Append(correoAux);
                    }

                    correoAux = "<br /><br /><font style='background-color:yellow;font-size:x-large'><b>Total Proceso: S/. "+ total_proceso.ToString() + "</b></font>";
                    correoReporte.Append(correoAux);

                }
                return correoReporte.ToString();
            }
            catch (Exception ex) {
                throw;
            }
        }

        public List<InterfazSuministros_PedidoBE> Kike() {
            List<InterfazSuministros_PedidoBE> _lstObjectos = new List<InterfazSuministros_PedidoBE>();
            _lstObjectos = (new InterfazSuministrosDAL()).ObtenerPedidosGenerados_Log(38);
            return _lstObjectos;
        }
    }
}
