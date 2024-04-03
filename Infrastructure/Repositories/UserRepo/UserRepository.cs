﻿using Infrastructure.Database;
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


        public async Task<ApplicationUser> AddUserAsync(ApplicationUser user)
        {
            await _database.Users.AddAsync(user);
            await _database.SaveChangesAsync();

            return await Task.FromResult(user);

        }

        public async Task DeleteUserAsync(Guid id)
        {
            var DeleteUserId = await _database.Users.FindAsync(id);
            if (DeleteUserId != null)
            {
                _database.Users.Remove(DeleteUserId);
                await _database.SaveChangesAsync();
            }
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _database.Users.Include(u => u.Addresses).ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid id)
        {
            return await _database.Users.FindAsync(id);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }
            return await _database.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            _database.Users.Update(user);
            await _database.SaveChangesAsync();
        }

        public async Task<bool> UpdatePasswordAsync(ApplicationUser user)
        {
            var userEntity = await _database.Users.FindAsync(user.UserId);
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
