using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locar.Servicos
{
    public class EmailSender : IEmail
    {
        public Task EnviarEmail(string email, string assunto, string mensagem)
        {
            throw new NotImplementedException();
        }
    }
}
