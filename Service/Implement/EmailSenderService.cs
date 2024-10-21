using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using Repository.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSenderService> _logger;
        //private readonly TestDbContext _dbcontext;

        public EmailSenderService(IConfiguration configuration, ILogger<EmailSenderService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            //_dbcontext = dbcontext;
        }
        public async Task<bool> EmailSendAsync(string email, string subject, string message)
        {
            bool status = false;
            try
            {
                var host = _configuration["SmtpEmail:Host"];
                var port = int.Parse(_configuration["SmtpEmail:Port"]);
                var from = _configuration["SmtpEmail:Username"];
                var password = _configuration["SmtpEmail:Password"];
                var enableSsl = bool.Parse(_configuration["SmtpEmail:EnableSsl"]);

                var emailSend = new MimeMessage();

                emailSend.From.Add(new MailboxAddress("Sender Name here", from));
                emailSend.To.Add(new MailboxAddress("Receiver Name here", email));

                emailSend.Subject = subject;
                emailSend.Body = new TextPart("html")
                {
                    Text = message
                };
                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(host, port, enableSsl);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate(from, password);

                    smtp.Send(emailSend);
                    smtp.Disconnect(true);
                }
                status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending the email.");
                status = false;
            }
            return status;
        }

        public string GetMailBody(string username, string password)
        {
            StringBuilder mailBody = new StringBuilder(); 
            mailBody.AppendLine("<h1 style='color: #000000';>Clear All [Y][A][C][H][T]</h1>");
            mailBody.AppendLine($"<p style='color: #000000'>Hi {username}, you've received this email because fuck you >:(</p>");
            mailBody.AppendFormat("<p style='color: #000000'>Password (highlight to view): ");
            mailBody.AppendLine($"<span style='background-color: black;color: transparent'>{password}</span>");
            mailBody.AppendLine("</p>");
            mailBody.AppendLine("<p style='color: #000000'>If you didn't create this account, please notify your T.Corp officer to receive a W.Corp ticket >:)</p>");
            //Add admin contact information here
            mailBody.AppendLine("<h5 style='color: #000000'>Best regards,<br>Erlking Heathcliff</h5>");
            return mailBody.ToString();
        }
    }
}
