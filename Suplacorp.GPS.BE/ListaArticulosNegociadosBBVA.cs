using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.BE
{
    /*
     * Esta LISTA de artículos soportará tener KEYS(cliente_articulo.codigoexterno) DUPLICADOS,
     * ¿por qué?, debido a que hay algunos codigosexternos que se duplican y quiero asegurarme de que 
     * el aplicativo no se caiga al llenar una colección con keys duplicados.
     */

    public class ListaArticulosNegociadosBBVA : List<KeyValuePair<int, Cliente_ArticuloBE>>
    {
        public void Add(int key, Cliente_ArticuloBE value) {
            var element = new KeyValuePair<int, Cliente_ArticuloBE>(key, value);
            this.Add(element);
        }
    }


    public class Cliente_ArticuloBE {

        private int codigoexterno_INT;
        private string codigoexterno_STR;
        private int idarticulo;
        private string descripcionArticulo;

        public int Codigoexterno_INT
        {
            get
            {
                return codigoexterno_INT;
            }

            set
            {
                codigoexterno_INT = value;
            }
        }

        public string Codigoexterno_STR
        {
            get
            {
                return codigoexterno_STR;
            }

            set
            {
                codigoexterno_STR = value;
            }
        }

        public int Idarticulo
        {
            get
            {
                return idarticulo;
            }

            set
            {
                idarticulo = value;
            }
        }

        public string DescripcionArticulo
        {
            get
            {
                return descripcionArticulo;
            }

            set
            {
                descripcionArticulo = value;
            }
        }
    }
}
