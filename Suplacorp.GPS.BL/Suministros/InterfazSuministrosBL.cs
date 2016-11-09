using System;
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
    public class InterfazSuministrosBL : BaseBL<InterfazSuministros_RegIniBE>, IInterfazRegIniBL<InterfazSuministros_RegIniBE, InterfazReferencias_RegProcBE>, IInterfazRegCabBL<InterfazSuministros_RegIniBE, InterfazSuministros_RegCabBE>
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
                                case "0": //1) Registro de control inicial para el inicio del fichero
                                    LlenarEntidad_RegIni(ref interfazSuministros_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroInicial);
                                    break;
                                case "1": //2) Registro de cabecera
                                    //interfazReferencias_RegIniBE.LstInterfazReferencias_RegProcBE.Add(LlenarEntidad_RegProc(ref interfazReferencias_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroProceso));
                                    break;
                                case "2": //3) Registro de Posición
                                    //interfazReferencias_RegIniBE.LstInterfazReferencias_RegProcBE.Add(LlenarEntidad_RegProc(ref interfazReferencias_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroProceso));
                                    break;
                                case "9": //4) Registro de control para el fin del fichero
                                    //LlenarEntidad_RegFin(ref interfazReferencias_RegIniBE, ref valores_linea_actual, ref lstValidacionRegistroInicial);
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
            catch (Exception ex){
                throw;
            }
        }

        public InterfazSuministros_RegCabBE LlenarEntidad_RegCab(ref InterfazSuministros_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegCab)
        {
            throw new NotImplementedException();
        }

        public void LlenarEntidad_RegFin(ref InterfazSuministros_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegFin)
        {
            throw new NotImplementedException();
        }

        public InterfazReferencias_RegProcBE LlenarEntidad_RegProc(ref InterfazSuministros_RegIniBE interfaz_RegIniBE, ref string[] valores_linea_actual, ref List<ValidacionInterfazBE> lstValidacionRegProc)
        {
            //NO SE IMPLEMENTARÁ (SOLO ES PARA LA INTERFAZ DE REFERENCIAS)
            throw new NotImplementedException();
        }

        public bool RegistrarInterfaz_RegIni(ref InterfazSuministros_RegIniBE interfaz_RegIniBE)
        {
            throw new NotImplementedException();
        }

        public bool RegistrarInterfaz_RegCab(ref InterfazSuministros_RegIniBE interfaz_RegIniBE)
        {
            throw new NotImplementedException();
        }


    }
}
