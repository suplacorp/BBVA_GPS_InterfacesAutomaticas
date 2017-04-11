using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.DAL
{
    public class DAL
    {
        //ENCRIPTAR ESTA CADENA A LA MANERA SUPLACORINA(LUEGO).
        public static string CreateConnection(string psRegValueName) {
            //DESARROLLO SERVIDOR DESARROLLO
            //return "Data Source=10.7.2.132;Initial Catalog=SuplacorpDB;User Id=sa;Password=S8p5A2o7P";

            //DESARROLLO SERVIDOR SUPLACORPDB
            //return "Data Source=10.7.2.251;Initial Catalog=SuplacorpDB_BBVA;User Id=sa;Password=S8p5A2o7P";

            //PRODUCCIÓN
            return "Data Source=10.7.2.251;Initial Catalog=SuplacorpDB;User Id=sa;Password=S8p5A2o7P";



        }
    }
}
