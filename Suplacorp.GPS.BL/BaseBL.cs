using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suplacorp.GPS.BE;
using System.IO;
using Suplacorp.GPS.Utils;

namespace Suplacorp.GPS.BL
{
    public abstract class BaseBL<T>
    {

        #region Constructor
        public BaseBL(){

        }
        #endregion
        
        public void EnviarCorreoElectronico(string emailPara, string emailConCopia, string asunto, string fileName, string cuerpoCorreo) {
            try{
                Email email = new Email();
                email.emailTo = emailPara;
                email.emailCC = emailConCopia;
                email.subject = asunto;
                email.isBodyHtml = true;
                email.fileName = fileName;
                email.body = cuerpoCorreo;
                email.EnviaCorreo();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
