using serverApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp.Repository
{
    public interface IUserRepository
    {
        Task<int> RegisterAsync(UserModel model);
        Task<UserModel> GetUserAsync(int id);
        Task<List<UserModel>> GetAllUsers();
        Task<int> UpdateUserAsync(int Id, UserModel model);
        Task<int> RemoveUserAsync(int id);
        Task<UserModel> GetByEmail(string email);
    }
}
