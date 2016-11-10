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
                interfazReferencias_RegCabBE.Ciudad_poblacion_provincia = valores_linea_actual[11].ToString();
                interfazReferencias_RegCabBE.Pais = valores_linea_actual[12].ToString();
                interfazReferencias_RegCabBE.Telefono_contacto = valores_linea_actual[13].ToString();
                interfazReferencias_RegCabBE.Usuario = valores_linea_actual[14].ToString();
                interfazReferencias_RegCabBE.Colonia_comuna_planta_distrito = valores_linea_actual[15].ToString();
                interfazReferencias_RegCabBE.Entre_calle1 = valores_linea_actual[16].ToString();
                interfazReferencias_RegCabBE.Entre_calle2 = valores_linea_actual[17].ToString();
                interfazReferencias_RegCabBE.Centro = valores_linea_actual[18].ToString();
                interfazReferencias_RegCabBE.Urgente = valores_linea_actual[19].ToString();
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
            try
            {
                //Registrando en BD la entidad
                interfaz_RegIniBE.Ruta_fichero_detino = GlobalVariables.Ruta_fichero_detino_Ref;          /* ACTUALIZAR ESTO  */
                interfaz_RegIniBE.Nombre_fichero_detino = interfaz_RegIniBE.Nombre_fichero + "_" + interfaz_RegIniBE.Fecha_ejecucion.ToString("yyyyMMdd").Trim() + "_" + interfaz_RegIniBE.Hora_proceso.Replace(":", "").Trim();
                interfaz_RegIniBE.Procesado = 0;                     /* AUN NO SE PROCESA DEBE IR "0" */
                interfaz_RegIniBE.Interfaz.Idinterface = 2;          /* VALORES FIJOS    */
                interfaz_RegIniBE.Tiporegistro.Idtiporegistro = 1;   /* VALORES FIJOS    */

                /*  Registrando el REGISTRO INICIAL */
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
                                //Generar los Pedidos (hacer toda la lógica y el algoritmo)

                                //Notificar con un correo bien explicativo
                                result = true;
                            }
                            else {
                                //Algún error
                            }
                        }
                        else
                        {
                            /*Ocurrió un error en alguno o algunos de los "n" registros del proceso */
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

        public bool RegistrarInterfaz_RegCab(ref InterfazSuministros_RegIniBE interfaz_RegIniBE)
        {
            throw new NotImplementedException();
        }

       
    }
}
