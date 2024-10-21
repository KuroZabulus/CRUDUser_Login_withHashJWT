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

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<string> Login(string username, string password)
        {
            return await _authRepository.Login(username, password);
        }

        public async Task<string> Register(RegisterDTO info)
        {
            return await _authRepository.Register(info);
        }
    }
}
