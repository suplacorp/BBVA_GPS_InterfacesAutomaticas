using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Suplacorp.GPS.BE
{
    public class InterfazReferenciasBE : BaseInterfazBE
    {
        #region Constructores
        public InterfazReferenciasBE() {

        }

        public InterfazReferenciasBE(int nro_proceso, string valor, ValidacionInterfazBE validacion, string nombre_fichero, string ruta_fichero, DateTime fecha_ejecucion, DateTime fecha_registro, bool procesado)
        {
            this.Nro_proceso = nro_proceso;
            this.Valor = valor;
            this.Validacion = validacion;
            this.Nombre_fichero = nombre_fichero;
            this.Ruta_fichero = ruta_fichero;
            this.Fecha_ejecucion = fecha_registro;
            this.Fecha_registro = fecha_registro;
            this.Procesado = procesado;
        }
        #endregion
    }
}
