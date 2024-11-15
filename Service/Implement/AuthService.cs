using Repository.DataAccess.Interface;
using Repository.DTO.ValidationModel;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IEmailSender _emailSender;

        public AuthService(IAuthRepository authRepository, IEmailSender emailSender)
        {
            _authRepository = authRepository;
            _emailSender = emailSender;
        }

        public async Task<string> Login(string username, string password)
        {
            return await _authRepository.Login(username, password);
        }

        public async Task<string> Register(RegisterDTO info)
        {
            return await _authRepository.Register(info);
            /*var result = await _authRepository.Register(info);
            if (result.StartsWith("Check email for confirmation"))
            {
                var subject = "Account Verification";
                var message = _emailSender.GetMailBodyBasicRole(info.Username, info.Password);
                await _emailSender.EmailSendAsync(info.Email, subject, message);
            }*/
        }
    }
}
