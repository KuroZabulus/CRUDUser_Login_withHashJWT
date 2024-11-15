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

        string GetMailBodyBasicRole(string username, string password);

        string GetMailBodyAdvancedRole(string username, string password);
    }
}
