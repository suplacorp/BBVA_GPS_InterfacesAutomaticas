using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suplacorp.GPS.DAL
{
    public class BaseDAL
    {
        protected string strConnectionString;

        public BaseDAL() {
            strConnectionString = DAL.CreateConnection("ConnectionString");
            
        }

    }
}
