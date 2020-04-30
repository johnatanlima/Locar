using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Locar.Servicos
{
    public class EmailSender : IEmail
    {
        private ConfiguracaoEmail _configEmail;

        public EmailSender(IOptions<ConfiguracaoEmail> configEmail)
        {
            _configEmail = configEmail.Value;
        }

        public async Task EnviarEmail(string email, string assunto, string mensagem)
        {
            var destinatario = String.IsNullOrEmpty(email) ? _configEmail.Email : email;

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_configEmail.Email, "Nathan")
            };

            mailMessage.To.Add(new MailAddress(destinatario));
            mailMessage.Subject = assunto;
            mailMessage.Body = mensagem;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;

            using(SmtpClient smtpClient = new SmtpClient(_configEmail.Endereco, _configEmail.Porta))
            {
                smtpClient.Credentials = new NetworkCredential(_configEmail.Email, _configEmail.Senha);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }

    }
}
