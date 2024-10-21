using Azure.Identity;
using Repository.DTO.ValidationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataAccess.Interface
{
    public interface IAuthRepository
    {
        Task<string> Register(RegisterDTO info);
        Task<string> Login(string username, string password);
    }
}
