using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Suplacorp.GPS.DAL;
using Suplacorp.GPS.BE;

namespace Suplacorp.GPS.BL
{
    public class ValidacionInterfazBL : BaseBL
    {
        public ValidacionInterfazBL() {

        }

        public void kike() {

            ValidacionInterfazDAL _objDL = new ValidacionInterfazDAL();
            _objDL.kike();
        }

    }
}
