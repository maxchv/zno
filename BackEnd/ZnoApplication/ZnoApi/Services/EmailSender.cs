using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Zno.Server.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            string login = "";
            string password = "";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(login, password);

            MailMessage mail = new MailMessage(login, email);
            mail.Body = message;
            mail.Subject = subject;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }
    }
}