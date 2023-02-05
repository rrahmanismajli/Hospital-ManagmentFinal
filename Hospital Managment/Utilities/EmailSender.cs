using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Hospital_Managment.Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("hospitalclinicmessages@hotmail.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("hospitalclinicmessages@hotmail.com", "Manimani12.");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);

            }
            return Task.CompletedTask;
        }
    }
}
