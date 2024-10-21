using Repository.DTO.ValidationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAuthService
    {
        Task<string> Register(RegisterDTO info);
        Task<string> Login(string username, string password);
    }
}
