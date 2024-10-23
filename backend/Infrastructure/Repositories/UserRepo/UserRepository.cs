using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

// This class implements the IUserRepository interface and provides methods for managing ApplicationUser entities in the MSB_Database.
// The class includes methods to:
// - Retrieve all users asynchronously with GetAllUsersAsync()
// - Retrieve a specific user by ID asynchronously with GetUserByIdAsync(string id)
// - Retrieve a user by email asynchronously with GetByEmailAsync(string email)
// - Update a user asynchronously with UpdateUserAsync(ApplicationUser user)
// - Update a user's password asynchronously with UpdatePasswordAsync(ApplicationUser user)
// - Delete a user asynchronously with DeleteUserAsync(string id)
// Entity Framework Core is used for database operations, ensuring asynchronous save changes to the database.

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
            var users = await _database.Users
                .Include(u => u.Addresses)
                .Include(u => u.Orders) // Eagerly load orders
                .AsSplitQuery()          // This splits the query to load related data separately
                .ToListAsync();
            return users.Cast<IAppUser>().ToList();
        }

        public async Task<IAppUser> GetUserByIdAsync(string id)
        {
            var user = await _database.Users
                .Include(u => u.Addresses)  // Eagerly load the addresses
                .Include(u => u.Orders)     // Eagerly load the orders
                .FirstOrDefaultAsync(u => u.Id == id);  // Fetch the user by ID

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