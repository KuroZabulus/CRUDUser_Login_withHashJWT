using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project_Cursus_Group3.Data;
using Repository.Data;
using Repository.Data.Entities;
using Repository.DataAccess.Interface;
using Repository.DTO.ViewModel;
using Repository.SupabaseFileUploader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Repository.DataAccess.Implement
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly TestDbContext _context;
        private readonly IMapper _mapper;
        private readonly UploadFile _fileUploader;

        //TODO: learn to use include, sort by and other function of the select function

        public UserRepository(TestDbContext dbContext, IMapper mapper, UploadFile fileUploader) : base(dbContext)
        {
            _context = dbContext;
            _mapper = mapper;
            _fileUploader = fileUploader;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUserAsync()
        {
            var query = Entities.AsNoTracking()
                .Include(u => u.Role)
                .AsQueryable();
            return await query.Select(u => _mapper.Map<UserViewModel>(u)).ToListAsync();
        }

        public async Task<UserViewModel> GetUserByEmailAsync(string email)
        {
            var query = await Entities.FirstOrDefaultAsync(u => u.Email == email);
            return _mapper.Map<UserViewModel>(query);
        }

        public async Task<UserViewModel> GetUserByFullnameAsync(string fullname)
        {
            var query = await Entities.FirstOrDefaultAsync(u => u.Fullname == fullname);
            return _mapper.Map<UserViewModel>(query);
        }

        public async Task<UserViewModel> GetUserByIdAsync(int id)
        {
            var query = await Entities.FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<UserViewModel>(query);
        }

        public async Task<UserViewModel> GetUserByPhoneNumberAsync(string phone)
        {
            var query = await Entities.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            return _mapper.Map<UserViewModel>(query);
        }

        public async Task<UserViewModel> GetUserByUsernameAsync(string username)
        {
            var query = await Entities.FirstOrDefaultAsync(u => u.Username == username);
            return _mapper.Map<UserViewModel>(query);
        }

        public async Task<string> UpdateImageAvatar(IFormFile image)
        {
            if(image == null)
            {
                return "No image were uploaded";
            }
            var url = await _fileUploader.UploadImageAsync(image);

            if(url.StartsWith("\nDetail:"))
            {
                return "An error in UploadFile has occurred! " + url;
            }

            return url;
        }
    }
}
