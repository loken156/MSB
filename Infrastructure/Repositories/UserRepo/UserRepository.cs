using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly MSB_Database _database;

        public UserRepository(MSB_Database mSB_Database)
        {
            _database = mSB_Database;
        }

        public async Task DeleteUserAsync(string id)
        {
            var DeleteUserId = await _database.Users.FindAsync(id);
            if (DeleteUserId != null)
            {
                _database.Users.Remove(DeleteUserId);
                await _database.SaveChangesAsync();
            }
        }

        public async Task<List<IAppUser>> GetAllUsersAsync()
        {
            var users = await _database.Users.Include(u => u.Addresses).ToListAsync();
            return users.Cast<IAppUser>().ToList();
        }


        public async Task<IAppUser> GetUserByIdAsync(string id)
        {
            var user = await _database.Users.FindAsync(id);
            return user as IAppUser ?? throw new KeyNotFoundException($"User with id {id} not found");
        }

        public async Task<IAppUser> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("EmployeeEmail cannot be null or empty", nameof(email));
            }
            var user = await _database.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            return user as IAppUser ?? throw new KeyNotFoundException($"User with email {email} not found");
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            _database.Users.Update(user);
            await _database.SaveChangesAsync();
        }

        public async Task<bool> UpdatePasswordAsync(ApplicationUser user)
        {
            var userEntity = await _database.Users.FindAsync(user.Id);
            if (userEntity == null)
            {
                return false; // User not found, return false
            }

            userEntity.PasswordHash = user.PasswordHash; // Assume new password hash has already been set
            await _database.SaveChangesAsync();
            return true; // Password update successful
        }
    }
}