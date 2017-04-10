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
    public class InterfazExpedicionesBL : BaseBL<InterfazExpediciones_ExpedicionBE>
    {

        public bool ObtenerExpedicionesGeneradas(ref InterfazExpediciones_RegIniBE intExpediciones)
        {
            try{
                return (new InterfazExpedicionesDAL()).ObtenerExpedicionesGeneradas(ref intExpediciones);
            }
            catch{
                throw;
            }
        }

        public bool GenerarInterfazExpediciones(ref int idregini, string nombre_fichero_destino, ref int errorID)
        {
            string[] result_valores;
            bool result = false;
            //int errorID = 0;
            
            try{
                //REGISTRANDO EN BD LA INTERFAZ DE EXPEDICIONES (GENERANDO LA INTERFAZ DE EXPEDICIONES)
                result_valores = (new InterfazExpedicionesDAL()).GenerarInterfazExpediciones(nombre_fichero_destino).Split(';');
                idregini = int.Parse(result_valores[0]);

                if (idregini != 0){
                    result = true;
                }
                else if(int.Parse(result_valores[1]) != 0) {

                    errorID = int.Parse(result_valores[1]);
                    /* OCURRIÓ UN ERROR EN EL REGISTRO INICIAL */
                    base.EnviarCorreoElectronico(
                        new InterfazExpedicionesDAL().ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - Int. Expediciones]", "",
                        (base.FormatearMensajeError_HTML(null, errorID, "Int. Expediciones")), null);
                    /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                    Console.WriteLine(base.FormatearMensajeError_CONSOLA(null, errorID, "Int. Expediciones"));
                }
            }
            catch (Exception ex) {
                /*NOTIFICACIÓN [ERROR] POR EMAIL*/
                base.EnviarCorreoElectronico(
                    new InterfazExpedicionesDAL().ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - Int. Expediciones]", "",
                    (base.FormatearMensajeError_HTML(ex, 0, "Int. Expediciones")), null);
                /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                Console.WriteLine(base.FormatearMensajeError_CONSOLA(ex, 0, "Int. Expediciones"));
                /* ELIMINACIÓN DE REGISTRO INICIAL, "RESET DE TODO" EL PROCESO*/
                (new InterfazExpedicionesDAL()).Resetear_Proceso_Interfaz((int)GlobalVariables.Interfaz.Expediciones, idregini);
            }
            return result;
        }

        public string ObtenerDestinatariosReporteInterfaz(int idInterfaz)
        {
            string result = "";

            try{
                result = (new InterfazExpedicionesDAL()).ObtenerDestinatariosReporteInterfaz(idInterfaz);   
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public string GenerarReporte_GeneracionInterfazExpediciones(InterfazExpediciones_RegIniBE intExpediciones)
        {
            StringBuilder correoReporte = new StringBuilder();
            string correoAux = "";
            
            try
            {
                if (intExpediciones.LstInterfazExpediciones_RegCabBE.Count > 0)
                {
                    /*ESCRIBIENDO EL [REGISTRO INICIAL] DE LA INTERFAZ DE EXPEDICIONES */
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
                    "       <td class='espacioIzquierda'>Nro.Proceso</td> " + "\r\n" +
                    "       <td>" + intExpediciones.Nro_proceso.ToString() + "</td> " + "\r\n" +
                    "   </tr> " + "\r\n" +
                    "   <tr>" + "\r\n" +
                    "        <td>Fecha Ejecución</td> " + "\r\n" +
                    "        <td>" + intExpediciones.Fecha_ejecucion.ToString() + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Hora Proceso</td> " + "\r\n" +
                    "        <td>" + intExpediciones.Hora_proceso.ToString() + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Archivo</td> " + "\r\n" +
                    "        <td>" + intExpediciones.Nombre_fichero_detino.ToString() + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "    <tr> " + "\r\n" +
                    "        <td>Fecha Registro</td> " + "\r\n" +
                    "        <td>" + intExpediciones.Fecha_ejecucion + "</td> " + "\r\n" +
                    "    </tr> " + "\r\n" +
                    "</table> " + "\r\n" +
                    "<br /> " + "\r\n" +
                    "<b><u>Cabeceras generadas:</u></b>" + "\r\n" +
                    " " + "\r\n" +
                    " " + "\r\n" +
                    "";
                    correoReporte.Append(correoAux);

                    /* ESCRIBIENDO CADA [REGISTRO CABECERA] DE LA INTERFAZ DE EXPEDICIONES */
                    foreach (var cab in intExpediciones.LstInterfazExpediciones_RegCabBE)
                    {
                        correoAux = "<hr /> " + "\r\n" +
                         "<table border='0' class='tamanoTabla'> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td colspan = '2' align='center' style='background-color:dodgerblue'><b>[" + cab.LstInterfazExpediciones_RegPosBE.Count.ToString() + " items] </b></td> " + "\r\n" +
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
                         "        <td>Tipo de movimiento</td> " + "\r\n" +
                         "        <td>" + cab.Tipo_movimiento + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Tipo expedición</td> " + "\r\n" +
                         "        <td>" + cab.Tipo_expedicion + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Número documento compra reserva</td> " + "\r\n" +
                         "        <td>" + cab.Nro_doc_compras_reserva + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Fecha contabilización</td> " + "\r\n" +
                         "        <td>" + cab.Fecha_contabilizacion + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Número de cesta</td> " + "\r\n" +
                         "        <td>" + cab.Numero_cesta + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "    <tr> " + "\r\n" +
                         "        <td>Texto cabecera documento (guías/albarán)</td> " + "\r\n" +
                         "        <td>" + cab.Texto_cabecera_documento + "</td> " + "\r\n" +
                         "    </tr> " + "\r\n" +
                         "</table>";
                        correoReporte.Append(correoAux);

                        correoAux =
                            "<table border='1' class='tamanoTabla'> " + "\r\n" +
                            "<tr style='text-align:center;background-color:#A5A5A5;font-weight:bold'>" + "\r\n" +
                            "    <td>IDPos</td> " + "\r\n" +
                            "    <td>Numeral</td> " + "\r\n" +
                            "    <td>Tipo registro</td> " + "\r\n" +
                            "    <td>Nro. Posic ped. Reserva</td> " + "\r\n" +
                            "    <td>Nro. Material</td> " + "\r\n" +
                            "    <td>IDArtículo</td> " + "\r\n" +
                            "    <td>Descripción</td> " + "\r\n" +
                            "    <td>Unidad medida pedido</td> " + "\r\n" +
                            "    <td>Cantidad</td> " + "\r\n" +
                            "    <td>Indicador entrega final</td> " + "\r\n" +
                            "    <td>Bulto</td> " + "\r\n" +
                            "    <td>Numero lote</td> " + "\r\n" +
                            "</tr>";
                        correoReporte.Append(correoAux);

                        /*ESCRIBIENDO CADA [POSICIÓN DE LA CABECERA ACTUAL] DE LA INTERFAZ DE EXPEDICIONES */
                        foreach (var pos in cab.LstInterfazExpediciones_RegPosBE)
                        {
                            correoAux =
                           "<tr> " + "\r\n" +
                           "   <td align='center'>" + pos.Idpos.ToString() + "</td> " + "\r\n" +
                           "   <td align='center'>" + Utilitarios.CompletarConCeros_Izquierda(pos.Numeral.ToString(), 6) + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Tipo_registro.ToString() + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Nro_posic_ped_reserva + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Nro_material + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Idarticulo + "</td> " + "\r\n" +
                           "   <td align='left'>" + pos.Descripcion_art + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Unidad_medida_pedido + "</td> " + "\r\n" +
                           "   <td align='right'>" + pos.Cantidad.ToString() + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Indicador_entrega_final + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Bulto + "</td> " + "\r\n" +
                           "   <td align='center'>" + pos.Numero_lote + "</td> " + "\r\n" +
                           "</tr>";
                            correoReporte.Append(correoAux);
                        }
                        correoAux = "</table> " + "\r\n";
                        correoReporte.Append(correoAux);
                    }

                    correoAux = "<br /><br /><font style='background-color:yellow;font-size:x-large'></b></font>";
                    correoReporte.Append(correoAux);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                /*NOTIFICACIÓN [ERROR] POR EMAIL*/
                base.EnviarCorreoElectronico(
                    new InterfazExpedicionesDAL().ObtenerDestinatariosReporteInterfaz((int)GlobalVariables.Interfaz.Expediciones), "", "[ERROR - Int. Expediciones]", "",
                    (base.FormatearMensajeError_HTML(ex, 0, "Int. Expediciones")), null);
                /*NOTIFICACIÓN [ERROR] POR CONSOLA DEL APLICATIVO*/
                Console.WriteLine(base.FormatearMensajeError_CONSOLA(ex, 0, "Int. Expediciones"), null);
            }
            return correoReporte.ToString();
        }

    }
}
