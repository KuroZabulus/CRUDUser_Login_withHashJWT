using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEmailSender
    {
        Task<bool> EmailSendAsync(string email, string subject, string message);

        string GetMailBody(string username, string password);
    }
}
