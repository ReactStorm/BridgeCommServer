using AutoMapper;
using Microsoft.EntityFrameworkCore;
using serverApp.Data;
using serverApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ProductDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> RegisterAsync(UserModel model)
        {
            var response = _mapper.Map<Users>(model);
            _context.Users.Add(response);
            await _context.SaveChangesAsync();
            return response.Id;
        }
        public async Task<List<UserModel>> GetAllUsers()
        {
            var response = await _context.Users.ToListAsync();
           return  _mapper.Map<List<UserModel>>(response);
        }
        public async Task<UserModel> GetUserAsync(int id)
        {
            var model = await _context.Users.FindAsync(id);
            return _mapper.Map<UserModel>(model);

        }
        public async Task<int> UpdateUserAsync(int Id,UserModel model)
        {
            var user = _mapper.Map<Users>(model);
            user.Id = Id;
            _context.Users.Update(user);
            var response = await _context.SaveChangesAsync();
            return response;
        }
        public async Task<int> RemoveUserAsync(int id)
        {
            var user = new Users()
            {
                Id = id
            };
            _context.Users.Remove(user);
           var response = await _context.SaveChangesAsync();
            return response;
        }
        public async Task<UserModel> GetByEmail(string email)
        {
            var response = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return _mapper.Map<UserModel>(response);
        }
    }
}
