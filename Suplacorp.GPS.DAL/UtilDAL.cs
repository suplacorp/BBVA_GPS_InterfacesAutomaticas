using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using Suplacorp.GPS.BE;
using Suplacorp.GPS.Utils;

namespace Suplacorp.GPS.DAL
{
    public class UtilDAL : BaseDAL
    {
        public UtilDAL()
        {

        }

        public ListaArticulosNegociadosBBVA ObtenerListaArticulosNegociadosBBVA()
        {

            ListaArticulosNegociadosBBVA lstArticulosBBVA = new ListaArticulosNegociadosBBVA();
            try
            {
                using (IDataReader reader = sqlDatabase.ExecuteReader(CommandType.Text,
                    "SELECT " + "\r\n" +
                    "  " + "\r\n" +
                    "   CA.CODIGOEXTERNO, " + "\r\n" +
                    "   CA.IDARTICULO, " + "\r\n" +
                    "   A.DESCRIPCION " + "\r\n" +
                    "FROM CLIENTE_ARTICULO CA(NOLOCK) " + "\r\n" +
                    "INNER JOIN ARTICULO A(NOLOCK) ON A.IDARTICULO = CA.IDARTICULO  " + "\r\n" +
                    "WHERE CA.IDCLIENTE = 342 AND ISNULL(CA.CODIGOEXTERNO, '') <> ''  " + "\r\n" +
                    "ORDER BY CAST(CA.CODIGOEXTERNO AS INT) ASC  "))
                {
                while (reader.Read())
                        {
                        //lstArticulosBBVA.Add(CargarValidacion(dataReader));
                        lstArticulosBBVA.Add(
                            (Convert.IsDBNull(reader["CODIGOEXTERNO"]) ? 0 : Convert.ToInt32(reader["CODIGOEXTERNO"])), 
                            new Cliente_ArticuloBE() {
                                Codigoexterno_INT = Convert.IsDBNull(reader["CODIGOEXTERNO"]) ? 0 : Convert.ToInt32(reader["CODIGOEXTERNO"]),
                                Idarticulo = Convert.IsDBNull(reader["IDARTICULO"]) ? 0 : Convert.ToInt32(reader["IDARTICULO"]),
                                DescripcionArticulo = Convert.IsDBNull(reader["DESCRIPCION"]) ? "" : Convert.ToString(reader["DESCRIPCION"])
                            });
                    }
                }
            }
            catch{
                throw;
            }
            return lstArticulosBBVA;
        }
    }
}
