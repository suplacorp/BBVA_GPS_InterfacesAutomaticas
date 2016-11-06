using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;
using System.IO;

namespace Suplacorp.GPS.BL
{
    public abstract class BaseBL<T>{

        public BaseBL(){

        }

        public void GuardarFichero(string nombre_fichero_origen, string ruta_fichero_origen, string nombre_fichero_destino, string ruta_fichero_destino){

            try {
                //La clase Path manipula el fichero y directorio, haciéndolo uno solo.
                string ficheroOrigen = System.IO.Path.Combine(ruta_fichero_origen, nombre_fichero_origen + ".txt");
                string ficheroDestino = System.IO.Path.Combine(ruta_fichero_destino, nombre_fichero_destino + ".txt");

                // ¿Directorio Existe?
                if (!System.IO.Directory.Exists(ruta_fichero_destino)){
                    System.IO.Directory.CreateDirectory(ruta_fichero_destino);
                }

                //Copiando y sobreescribiendo si existe fichero
                System.IO.File.Copy(ficheroOrigen, ficheroDestino, true);
            }
            catch (Exception ex){
                throw;
            }
        }


    }
}
