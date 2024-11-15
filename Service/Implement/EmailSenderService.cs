using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using Repository.CustomFunctions.TokenHandler;
using Repository.Data;
using Repository.DTO.ValidationModel;
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
        private readonly JWTTokenProvider _tokenProvider;
        //private readonly TestDbContext _dbcontext;

        public EmailSenderService(IConfiguration configuration, ILogger<EmailSenderService> logger, JWTTokenProvider tokenProvider)
        {
            _configuration = configuration;
            _logger = logger;
            _tokenProvider = tokenProvider;
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

        public string GetMailBodyAdvancedRole(string username, string password)
        {
            string apiUrl = _configuration["Host:https"];
            string token = _tokenProvider.GenerateVerificationToken(username);
            string url = $"{apiUrl}/api/auth-token/confirm-email?token={token}";

            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendLine("<h1 style='color: #000000';>Clear All CATHY</h1>");
            mailBody.AppendLine($"<p style='color: #000000'>Hi {username}, you've received this email because you are beautiful <:)</p>");
            mailBody.AppendFormat("<p style='color: #000000'>Password (highlight to view): ");
            mailBody.AppendLine($"<span style='background-color: black;color: transparent'>{password}</span>");
            mailBody.AppendLine("</p>");
            mailBody.AppendLine($"<a href='{url}' style=' display: block;");
            mailBody.AppendLine($"text-align: center;");
            mailBody.AppendLine($"font-weight: bold;");
            mailBody.AppendLine($"background-color: #008CBA;");
            mailBody.AppendLine($"font-size: 16px;");
            mailBody.AppendLine($"border-radius: 10px;");
            mailBody.AppendLine($"color:#ffffff;");
            mailBody.AppendLine($"cursor:pointer;");
            mailBody.AppendLine($"width:100%;");
            mailBody.AppendLine($"padding:10px;'>");
            mailBody.AppendLine($"Confirm Email");
            mailBody.AppendLine($"</a>");
            mailBody.AppendLine($"</div>");
            mailBody.AppendLine("<p style='color: #000000'>If you didn't create this account, please notify your assigned Don Quixote to receive a trip to the La Mancha >:)</p>");
            //Add admin contact information here
            mailBody.AppendLine("<h5 style='color: #000000'>Best regards,<br>Dante Limbus</h5>");
            return mailBody.ToString();
        }

        public string GetMailBodyBasicRole(string username, string password)
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
