using Microsoft.AspNetCore.Http;
using Repository.DTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataAccess.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserViewModel>> GetAllUserAsync();
        Task<UserViewModel> GetUserByUsernameAsync(string username);
        Task<UserViewModel> GetUserByFullnameAsync(string fullname);
        Task<UserViewModel> GetUserByIdAsync(int id);
        Task<UserViewModel> GetUserByPhoneNumberAsync(string phone);
        Task<UserViewModel> GetUserByEmailAsync(string email);
        Task<string> UpdateImageAvatar(IFormFile? image);
        Task<string> GenerateCertificate(string username, string subject);
    }
}
