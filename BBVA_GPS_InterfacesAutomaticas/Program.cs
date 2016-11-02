using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Suplacorp.GPS.BE;
using Suplacorp.GPS.BL;

namespace BBVA_GPS_InterfacesAutomaticas
{
    class Program
    {
        static void Main(string[] args)
        {

            //TESTING1
            //ValidacionInterfazBL _objBL = new ValidacionInterfazBL();
            //_objBL.kike();

            //TESTING2
            string registro_inicial = "000000	0	S	MX	MM	MX_OL1_REFER	01102015	172629													";
            string registro_abajo = "000001	1	MX11	000000000210000222	BLOCK DE 100 HOJAS	UN	PAQ	1	100			Z002		0,000		0,000		10100023	MX11		";
            string registro_ultimo = "000205	9	00205	00000	00000																";
            //string[] valores = registro_inicial.Split("	");       
            //String[] names = registro_inicial.Split('\t');
            //String[] names = registro_ultimo.Split('\t');

            List<ValidacionInterfazBE> _lstValidacion = new List<ValidacionInterfazBE>();
            _lstValidacion = (new ValidacionInterfazBL()).ListarValidaciones_xInterfaz("PE_OL1_REFER");


            //InterfazReferenciasBE _interfazRef = new InterfazReferenciasBE();
            
            Console.ReadLine();
        }
    }
}
