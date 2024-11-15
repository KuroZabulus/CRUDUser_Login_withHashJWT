using Microsoft.AspNetCore.Http;
using Repository.DataAccess.Interface;
using Repository.DTO.ViewModel;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task<IEnumerable<UserViewModel>> GetAllUserAsync()
        {
            return userRepository.GetAllUserAsync();
        }

        public Task<UserViewModel> GetUserByEmailAsync(string email)
        {
            return userRepository.GetUserByEmailAsync(email);
        }

        public Task<UserViewModel> GetUserByFullnameAsync(string fullname)
        {
            return userRepository.GetUserByFullnameAsync(fullname);    
        }

        public Task<UserViewModel> GetUserByIdAsync(int id)
        {
            return userRepository.GetUserByIdAsync(id);
        }

        public Task<UserViewModel> GetUserByPhoneNumberAsync(string phone)
        {
            return userRepository.GetUserByPhoneNumberAsync(phone);
        }

        public Task<UserViewModel> GetUserByUsernameAsync(string username)
        {
            return userRepository.GetUserByUsernameAsync(username);
        }

        public Task<string> UpdateImageAvatar(IFormFile? image)
        {
            return userRepository.UpdateImageAvatar(image);
        }
    }
}
