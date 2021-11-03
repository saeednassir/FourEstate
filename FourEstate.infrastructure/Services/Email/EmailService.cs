using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task Send(string to, string subject, string body)
        {
            // create message
            var message = new MailMessage();

            message.From = new MailAddress("betaaspcourse@gmail.com", "Product App");
            message.Subject = subject;
            message.Body = body;
            message.To.Add(new MailAddress(to)); 
            message.IsBodyHtml = false;


            try
            {
                var emailClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("betaaspcourse@gmail.com", "Ahmed11$")
                };


                await emailClient.SendMailAsync(message);
            }catch(Exception e)
            {

            }
            // send email
         
        }
    }
}
