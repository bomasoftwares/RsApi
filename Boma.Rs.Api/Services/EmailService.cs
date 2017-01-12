using Boma.Rs.Api.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Boma.Rs.Api.Services
{
    public class EmailService: IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await SendEmailToResetPassword(message);
        }

        public async Task SendEmailToResetPassword(IdentityMessage message)
        {
            var conteudo = new StringBuilder();

            conteudo.Append(message.Body);

            var emailFrom = "contato.boma@gmail.com";
            MailMessage msg = new MailMessage(emailFrom, message.Destination);
            msg.Subject = message.Subject;
            msg.SubjectEncoding = Encoding.Default;
            msg.Body = conteudo.ToString();
            msg.BodyEncoding = Encoding.Default;
            msg.IsBodyHtml = true;
            
            var smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailFrom, "boma@2016");

            await smtpClient.SendMailAsync(msg);
        }
    }
}