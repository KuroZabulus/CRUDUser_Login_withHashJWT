using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_Cursus_Group3.Data;
using Repository.Data;
using Repository.DataAccess.Interface;
using Repository.DTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataAccess.Implement
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly TestDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(TestDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUserAsync()
        {
            var query = Entities.AsNoTracking().Include(u => u.Role).AsQueryable();
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
    }
}
