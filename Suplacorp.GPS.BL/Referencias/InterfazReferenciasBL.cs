﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.DAL;
using Suplacorp.GPS.BE;
using System.Collections;
using System.Reflection;
using Suplacorp.GPS.Utils;
using System.IO;

namespace Suplacorp.GPS.BL
{
    public class InterfazReferenciasBL : BaseBL<InterfazReferencias_RegIniBE>, IInterfazRegIniBL<InterfazReferencias_RegIniBE, InterfazReferencias_RegProcBE>
    {
        #region Constructor
        public InterfazReferenciasBL()
        {

        }
        #endregion

        #region LlenarEntidades
        public void LlenarEntidad_RegIni(ref InterfazReferencias_RegIniBE interfaz_RegIniBE, ref String[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegIni)
        {
            try
            {
                //Ejemplo: "000000	0	S	MX	MM	MX_OL1_REFER	01102015	172629		
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

        public InterfazReferencias_RegProcBE LlenarEntidad_RegProc(ref InterfazReferencias_RegIniBE interfaz_RegIniBE, ref String[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegProc)
        {

            InterfazReferencias_RegProcBE interfazReferencias_RegProcBE = new InterfazReferencias_RegProcBE();
            try
            {
                //Ejemplo: "000001	1	MX11	000000000210000222	BLOCK DE 100 HOJAS	UN	PAQ	1	100			Z002		0,000		0,000		10100023	MX11		";
                interfazReferencias_RegProcBE.Numeral = valores_linea_actual[0].ToString();
                interfazReferencias_RegProcBE.Tipo_registro = valores_linea_actual[1].ToString();
                interfazReferencias_RegProcBE.Sociedad = valores_linea_actual[2].ToString();
                interfazReferencias_RegProcBE.Codigo_material = valores_linea_actual[3].ToString();
                interfazReferencias_RegProcBE.Texto_breve_material = valores_linea_actual[4].ToString();
                interfazReferencias_RegProcBE.Unidad_medida_base = valores_linea_actual[5].ToString();
                interfazReferencias_RegProcBE.Unidad_medida_pedido = valores_linea_actual[6].ToString();
                interfazReferencias_RegProcBE.Contador_conv_und_med_base = valores_linea_actual[7].ToString();
                interfazReferencias_RegProcBE.Denominador_conv_und_med_base = valores_linea_actual[8].ToString();
                interfazReferencias_RegProcBE.Status_mat_todos_centros = valores_linea_actual[9].ToString();
                interfazReferencias_RegProcBE.Status_mat_especif_centro = valores_linea_actual[10].ToString();
                interfazReferencias_RegProcBE.Tipo_aprov = valores_linea_actual[11].ToString();
                interfazReferencias_RegProcBE.Indicador_breve = valores_linea_actual[12].ToString();
                interfazReferencias_RegProcBE.Peso_bruto = decimal.Parse(Utilitarios.DecimalFormato_BBVA(valores_linea_actual[13])); /*DECIMAL*/
                interfazReferencias_RegProcBE.Unidad_peso = valores_linea_actual[14].ToString();
                interfazReferencias_RegProcBE.Volumen = decimal.Parse(Utilitarios.DecimalFormato_BBVA(valores_linea_actual[15]));    /*DECIMAL*/
                interfazReferencias_RegProcBE.Unidad_volumen = valores_linea_actual[16].ToString();
                interfazReferencias_RegProcBE.Codigo_antiguo_material = valores_linea_actual[17].ToString();
                interfazReferencias_RegProcBE.Centro = valores_linea_actual[18].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            return interfazReferencias_RegProcBE;
        }

        public void LlenarEntidad_RegFin(ref InterfazReferencias_RegIniBE interfaz_RegIniBE, ref String[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegFin)
        {
            try
            {
                //Ejemplo: "000205	9	00205	00000	00000																";
                interfaz_RegIniBE.Numero_total_registros_fin = valores_linea_actual[0].ToString();
                interfaz_RegIniBE.Tipo_registro_fin = valores_linea_actual[1].ToString();
                interfaz_RegIniBE.Numero_registros_proceso_fin = valores_linea_actual[2].ToString();
                interfaz_RegIniBE.Numero_registros_tipo2_fin = valores_linea_actual[3].ToString();
                interfaz_RegIniBE.Numero_registros_tipo3_fin = valores_linea_actual[4].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        public InterfazReferencias_RegIniBE LeerFicheroInterfaz(string nombre_fichero, string ruta_fichero_lectura, List<ValidacionInterfazBE> lstValidacion)
        {
            InterfazReferencias_RegIniBE interfazReferencias_RegIniBE = new InterfazReferencias_RegIniBE();
            string linea_actual;
            String[] valores_linea_actual;
            string idTipoDetalle_TipoRegistro;

            try
            {
                //1) Validaciones - registro de control inicial para el inicio del fichero
                List<ValidacionInterfazBE> lstValidacionRegistroInicial = new List<ValidacionInterfazBE>();
                lstValidacionRegistroInicial = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 0);
                //2) Validaciones - registros de proceso
                List<ValidacionInterfazBE> lstValidacionRegistroProceso = new List<ValidacionInterfazBE>();
                lstValidacionRegistroProceso = lstValidacion.FindAll(s => s.Id_tipodetalle_tiporegistro == 1);
                //3) Validaciones - registros para fin
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
                                case "0": //1) Registro de control inicial para el inicio del fichero
                                    LlenarEntidad_RegIni(ref interfazReferencias_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroInicial);
                                    break;
                                case "1": //2) Registros de proceso
                                    interfazReferencias_RegIniBE.LstInterfazReferencias_RegProcBE.Add(LlenarEntidad_RegProc(ref interfazReferencias_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroProceso));
                                    break;
                                case "9": //3) Registros para fin
                                    LlenarEntidad_RegFin(ref interfazReferencias_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroInicial);
                                    break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex){
                Console.WriteLine(ex.Message + ";" + ex.Source.ToString() + ";" + ex.StackTrace.ToString());
            }

            return interfazReferencias_RegIniBE;
        }

        public bool RegistrarInterfaz_RegIni(ref InterfazReferencias_RegIniBE interfaz_RegIniBE)
        {
            string[] result_valores;
            bool result = false;
            try
            {
                //Registrando en BD la entidad
                interfaz_RegIniBE.Ruta_fichero_detino = GlobalVariables.Ruta_fichero_detino_Ref;          /* ACTUALIZAR ESTO  */
                interfaz_RegIniBE.Nombre_fichero_detino = interfaz_RegIniBE.Nombre_fichero + "_" + interfaz_RegIniBE.Fecha_ejecucion.ToString("yyyyMMdd").Trim() + "_" + interfaz_RegIniBE.Hora_proceso.Replace(":", "").Trim();        /* ACTUALIZAR ESTO  */
                interfaz_RegIniBE.Procesado = 0;                     /* AUN NO SE PROCESA DEBE IR "0" */
                interfaz_RegIniBE.Interfaz.Idinterface = 1;          /* VALORES FIJOS    */
                interfaz_RegIniBE.Tiporegistro.Idtiporegistro = 1;   /* VALORES FIJOS    */

                /*  Registró el registro inicial correctamente */
                result_valores = (new InterfazReferenciasDAL()).RegistrarRegIni(ref interfaz_RegIniBE).Split(';');
                if (int.Parse(result_valores[0]) != 0)
                {
                    //Registrando el "Idregini" generado
                    interfaz_RegIniBE.Idregini = int.Parse(result_valores[0]);

                    //Registrando proceso (detalle del registro inicial)
                    result_valores = (new InterfazReferenciasDAL()).RegistrarProc(ref interfaz_RegIniBE).Split(';');
                    if (int.Parse(result_valores[0]) == interfaz_RegIniBE.LstInterfazReferencias_RegProcBE.Count()){
                        result =  true;
                    }
                    else{
                        /*Ocurrió un error en alguno o algunos de los "n" registros del proceso */
                        interfaz_RegIniBE.Id_error = int.Parse(result_valores[1]);
                    }
                }
                else{
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

        public bool ActualizarClienteArticulo_IntRef(ref InterfazReferencias_RegIniBE interfaz_RegIniBE) {
            string[] result_valores;
            bool result = false;
            try{
                result_valores = (new InterfazReferenciasDAL()).ActualizarClienteArticulo_IntRef(ref interfaz_RegIniBE).Split(';');
                if (int.Parse(result_valores[0]) != 0){
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return result;
        }

    }
}
