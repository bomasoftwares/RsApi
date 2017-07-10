using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace Boma.RedeSocial.Crosscut.Services
{
    public class EmailService
    {
        public void SendEmail(string body, string subject, string destination)
        {
            var message = new MailMessage();
            message.From = new MailAddress("contato.boma@gmail.com","Contato Boma");
            message.To.Add(new MailAddress(destination));
            message.Subject = subject;

            message.Body = body;
            message.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            var credentials = new NetworkCredential("contato.boma@gmail.com", "boma@2016");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;

            smtpClient.Send(message);
        }
    }
}
