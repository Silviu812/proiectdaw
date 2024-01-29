using Microsoft.EntityFrameworkCore;
using System.Net;
using web_api.Data;
using web_api.Models;

namespace web_api.Services
{
    public class UserService
    {
        private readonly UserDbContext _userDbContext;
        public UserService(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _userDbContext.Users.ToListAsync();
        }

        public async Task<List<string>> GetAllUserNames()
        {
            return await _userDbContext.Users.Select(x => x.Username).ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _userDbContext.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUserName(string username)
        {
            return await _userDbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> CreateUser(User user)
        {
            await _userDbContext.Users.AddAsync(user);
            await _userDbContext.SaveChangesAsync();
            return user;
        }

        public async void DeleteUser(User user)
        {
            _userDbContext.Users.Remove(user);
            await _userDbContext.SaveChangesAsync();
        }

        public async void SetUserRole(int id, string newRole)
        {
            User? dbUser = await _userDbContext.Users.FindAsync(id);
            if (dbUser == null)
                return;

            dbUser.Role = newRole;

            await _userDbContext.SaveChangesAsync();
        }

        public async void SetUserRole(string username, string newRole)
        {
            User? dbUser = await _userDbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (dbUser == null)
                return;

            dbUser.Role = newRole;

            await _userDbContext.SaveChangesAsync();
        }
    }
}
