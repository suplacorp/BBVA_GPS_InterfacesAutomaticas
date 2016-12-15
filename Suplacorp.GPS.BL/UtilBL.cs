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
    public class UtilBL
    {
        
        public UtilBL(){
            
        }
        
        public ListaArticulosNegociadosBBVA ObtenerListaArticulosNegociadosBBVA()
        {
            try{
                return (new UtilDAL()).ObtenerListaArticulosNegociadosBBVA();
            }
            catch{
                throw;
            }
        }

        public Dictionary<int, string> ObtenerListaDepartamentosBBVA() {
            try{
                return (new UtilDAL()).ObtenerListaDepartamentosBBVA();
            }
            catch{
                throw;
            }
        }
        
    }
}
