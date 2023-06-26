using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace Rovie.Services
{
    public class EmailSender : IEmailSender
    {
        private static SmtpClient smtp;

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //TODO Create email service
            await Task.CompletedTask;

            try
            {
                MailAddress from = new MailAddress("rovie.sup@gmail.com", "ROVIE support");
                MailAddress to = new MailAddress(email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = $"Ключь для доступа к вашей комнате";
                m.Body = "Ключ: "; //  + Key.GetUniqueKey(10)
                smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(from.Address, "kmpiknnqigrvyhej");
                await smtp.SendMailAsync(m);
                Console.WriteLine("Письмо отправлено");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
