using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Project_Cursus_Group3.Data;
using Repository.Data;
using Repository.DataAccess.Interface;
using Repository.DTO.ValidationModel;
using Repository.TokenHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataAccess.Implement
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;
        private readonly TestDbContext _dbcontext;
        private readonly JWTTokenProvider _tokenProvider;

        public AuthRepository(IUserRepository userRepo, IConfiguration configuration, TestDbContext dbcontext, JWTTokenProvider provider)
        {
            _userRepo = userRepo;
            _configuration = configuration;
            _dbcontext = dbcontext;
            _tokenProvider = provider;
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _dbcontext.Users.Include(u => u.Role)
                                                .AsNoTracking()
                                                .Where(u => u.Username == username)
                                                .SingleOrDefaultAsync();
            if(user == null) return "User not found";
            if(user != null && !BCrypt.Net.BCrypt.Verify(password, user.Password)) return "Invalid password";
            //Create token
            var token = _tokenProvider.CreateToken(user);
            return "Login successful\n" + token;
        }

        public async Task<string> Register(RegisterDTO info)
        {
            try
            {
                var existingUsername = await _userRepo.GetUserByUsernameAsync(info.Username);
                if (existingUsername != null) return "User name already exists";

                var existingPhone = await _userRepo.GetUserByPhoneNumberAsync(info.PhoneNumber);
                if (existingPhone != null) return "Phone number already exists";

                var existingEmail = await _userRepo.GetUserByEmailAsync(info.Email);
                if (existingEmail != null) return "Email already exists";

                if (info.RoleId == 1) 
                {
                    info.Status = "active";
                }
                if (info.RoleId == 2 || info.RoleId == 3)
                {
                    info.Status = "pending";
                }
                User user = new User
                {
                    Username = info.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(info.Password),
                    RoleId = info.RoleId,
                    PhoneNumber = info.PhoneNumber,
                    Email = info.Email,
                    Fullname = info.Fullname,
                    Address = null,
                    ImageAvatar = null,
                    Status = info.Status.ToLower()
                };
                
                _dbcontext.Users.Add(user);
                await _dbcontext.SaveChangesAsync();

                if (info.RoleId == 1)
                {
                    return "Register successful.";
                }
                else if (info.RoleId == 2)
                {
                    return "Check email for confirmation.";
                }
                return "User registered successfully.";
            }
            catch (Exception ex)
            {
                return $"Internal server error: {ex.Message}";
            }
        }
    }
}
