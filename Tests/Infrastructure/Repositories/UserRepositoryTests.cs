using Domain.Models.Address;
using Infrastructure.Database;
using Infrastructure.Entities;
using Infrastructure.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "TestUser1", Email = "test1@example.com", FirstName = "Test", LastName = "User", Addresses = new List<AddressModel>() },
                new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "TestUser2", Email = "test2@example.com", FirstName = "Test", LastName = "User", Addresses = new List<AddressModel>() },
            };
            using (var context = new MSB_Database(options))
            {
                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var userRepository = new UserRepository(context);

                // Act
                var result = await userRepository.GetAllUsersAsync();

                // Assert
                Assert.Equal(users.Count, result.Count());
            }
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var userId = Guid.NewGuid().ToString();
            var expectedUser = new ApplicationUser { Id = userId, UserName = "TestUser", Email = "test@example.com", FirstName = "Test", LastName = "User", Addresses = new List<AddressModel>() };
            using (var context = new MSB_Database(options))
            {
                context.Users.Add(expectedUser);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var userRepository = new UserRepository(context);

                // Act
                var result = await userRepository.GetUserByIdAsync(userId);

                // Assert
                Assert.Equal(expectedUser.Id, result.Id);
            }
        }

        [Fact]
        public async Task UpdateUserAsync_UpdatesUserInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var userId = Guid.NewGuid().ToString();
            var concurrencyStamp = Guid.NewGuid().ToString();
            var originalUser = new ApplicationUser { Id = userId, UserName = "OriginalUser", Email = "original@example.com", FirstName = "Test", LastName = "User", Addresses = new List<AddressModel>(), ConcurrencyStamp = concurrencyStamp };
            var updatedUser = new ApplicationUser { Id = userId, UserName = "UpdatedUser", Email = "updated@example.com", FirstName = "Test", LastName = "User", Addresses = new List<AddressModel>(), ConcurrencyStamp = concurrencyStamp };
            using (var context = new MSB_Database(options))
            {
                context.Users.Add(originalUser);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var userRepository = new UserRepository(context);

                // Act
                await userRepository.UpdateUserAsync(updatedUser);

                // Assert
                Assert.Equal(updatedUser.UserName, context.Users.Single().UserName);
            }
        }


        [Fact]
        public async Task DeleteUserAsync_DeletesUserFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var userId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = userId, UserName = "TestUser", Email = "test@example.com", FirstName = "Test", LastName = "User", Addresses = new List<AddressModel>() };
            using (var context = new MSB_Database(options))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var userRepository = new UserRepository(context);

                // Act
                await userRepository.DeleteUserAsync(userId);

                // Assert
                Assert.Empty(context.Users);
            }
        }

        [Fact]
        public async Task UpdatePasswordAsync_UpdatesPasswordInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var userId = Guid.NewGuid().ToString();
            var originalUser = new ApplicationUser { Id = userId, UserName = "OriginalUser", Email = "original@example.com", FirstName = "Test", LastName = "User", Addresses = new List<AddressModel>(), PasswordHash = "originalHash" };
            var updatedUser = new ApplicationUser { Id = userId, UserName = "OriginalUser", Email = "original@example.com", FirstName = "Test", LastName = "User", Addresses = new List<AddressModel>(), PasswordHash = "updatedHash" };
            using (var context = new MSB_Database(options))
            {
                context.Users.Add(originalUser);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var userRepository = new UserRepository(context);

                // Act
                var result = await userRepository.UpdatePasswordAsync(updatedUser);

                // Assert
                Assert.True(result);
                Assert.Equal(updatedUser.PasswordHash, context.Users.Single().PasswordHash);
            }
        }

    }
}
