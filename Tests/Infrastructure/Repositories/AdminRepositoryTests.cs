using Domain.Models.Admin;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Repositories
{
    public class AdminRepositoryTests
    {
        [Fact]
        public async Task GetAdminsAsync_ReturnsAllAdmins()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var admins = new List<AdminModel>
            {
                new AdminModel { Id = Guid.NewGuid().ToString(), UserName = "TestAdmin1", Email = "test1@example.com", Permissions = new List<string>(), FirstName = "Test", LastName = "Admin", Role = "Admin" },
                new AdminModel { Id = Guid.NewGuid().ToString(), UserName = "TestAdmin2", Email = "test2@example.com", Permissions = new List<string>(), FirstName = "Test", LastName = "Admin", Role = "Admin" },
            };
            using (var context = new MSB_Database(options))
            {
                context.Admins.AddRange(admins);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var adminRepository = new AdminRepository(context);

                // Act
                var result = await adminRepository.GetAdminsAsync();

                // Assert
                Assert.Equal(admins.Count, result.Count());
            }
        }

        [Fact]
        public async Task GetAdminAsync_ReturnsAdmin_WhenAdminExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var adminId = Guid.NewGuid().ToString();
            var expectedAdmin = new AdminModel { Id = adminId, UserName = "TestAdmin", Email = "test@example.com", Permissions = new List<string>(), FirstName = "Test", LastName = "Admin", Role = "Admin" };
            using (var context = new MSB_Database(options))
            {
                context.Admins.Add(expectedAdmin);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var adminRepository = new AdminRepository(context);

                // Act
                var result = await adminRepository.GetAdminAsync(Guid.Parse(adminId));

                // Assert
                Assert.Equal(expectedAdmin.Id, result.Id);
            }
        }

        [Fact]
        public async Task CreateAdminAsync_CreatesAdminInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var admin = new AdminModel { Id = Guid.NewGuid().ToString(), UserName = "TestAdmin", Email = "test@example.com", Permissions = new List<string>(), FirstName = "Test", LastName = "Admin", Role = "Admin" };
            using (var context = new MSB_Database(options))
            {
                var adminRepository = new AdminRepository(context);

                // Act
                var result = await adminRepository.CreateAdminAsync(admin);

                // Assert
                Assert.Equal(admin.Id, result.Id);
                Assert.Single(context.Admins);
            }
        }

        [Fact]
        public async Task UpdateAdminAsync_UpdatesAdminInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var adminId = Guid.NewGuid().ToString();
            var originalAdmin = new AdminModel { Id = adminId, UserName = "OriginalAdmin", Email = "original@example.com", Permissions = new List<string>(), FirstName = "Test", LastName = "Admin", Role = "Admin" };
            var updatedAdmin = new AdminModel { Id = adminId, UserName = "UpdatedAdmin", Email = "updated@example.com", Permissions = new List<string>(), FirstName = "Test", LastName = "Admin", Role = "Admin" };
            using (var context = new MSB_Database(options))
            {
                context.Admins.Add(originalAdmin);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var adminRepository = new AdminRepository(context);

                // Act
                var result = await adminRepository.UpdateAdminAsync(Guid.Parse(adminId), updatedAdmin);

                // Assert
                Assert.Equal(updatedAdmin.Id, result.Id);
                Assert.Equal(updatedAdmin.UserName, context.Admins.Single().UserName);
            }
        }

        [Fact]
        public async Task DeleteAdminAsync_DeletesAdminFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var adminId = Guid.NewGuid().ToString();
            var admin = new AdminModel { Id = adminId, UserName = "TestAdmin", Email = "test@example.com", Permissions = new List<string>(), FirstName = "Test", LastName = "Admin", Role = "Admin" };
            using (var context = new MSB_Database(options))
            {
                context.Admins.Add(admin);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var adminRepository = new AdminRepository(context);

                // Act
                var result = await adminRepository.DeleteAdminAsync(Guid.Parse(adminId));

                // Assert
                Assert.True(result);
                Assert.Empty(context.Admins);
            }
        }
    }
}