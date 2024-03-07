using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Photos.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //in the future can implement logic to send email :-)
            // Outgoing 
            string mailFrom = "adminserver@op.pl";
            string mailFromDisplayName = "Photos Software Administration";
            string mailServer = "smtp.poczta.onet.pl";
            SmtpClient client = new SmtpClient(mailServer);
            client.Port = 587;// 465;
            // Use asusming OAuth authentication (need to create an app password)
            client.Credentials = new NetworkCredential(mailFrom, _configuration["MailPass"]);
            client.EnableSsl = true;
            // Prepare Mail 
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailFrom, mailFromDisplayName);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = htmlMessage;
            try
            {
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Task.CompletedTask;
        }
    }
}
