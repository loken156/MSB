using Domain.Models.Driver;
using Infrastructure.Database;
using Infrastructure.Repositories.DriverRepo;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Repositories
{
    public class DriverRepositoryTests
    {
        [Fact]
        public async Task AddDriver_AddsDriverToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var driver = new DriverModel
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Test",
                LastName = "Driver",
                LicenseNumber = "123456",
                Email = "test@example.com",
                Role = "Driver",
                UserName = "TestDriver"
            };
            using (var context = new MSB_Database(options))
            {
                var driverRepository = new DriverRepository(context);

                // Act
                driverRepository.AddDriver(driver);
                await context.SaveChangesAsync();

                // Assert
                Assert.Single(context.Drivers);
            }
        }

        [Fact]
        public async Task GetAllDrivers_ReturnsAllDrivers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var drivers = new List<DriverModel>
            {
                new DriverModel
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Test",
                    LastName = "Driver1",
                    LicenseNumber = "123456",
                    Email = "driver1@example.com",
                    Role = "Driver",
                    UserName = "Driver1"
                },
                new DriverModel
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Test",
                    LastName = "Driver2",
                    LicenseNumber = "789012",
                    Email = "driver2@example.com",
                    Role = "Driver",
                    UserName = "Driver2"
                },
            };
            using (var context = new MSB_Database(options))
            {
                context.Drivers.AddRange(drivers);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var driverRepository = new DriverRepository(context);

                // Act
                var result = await driverRepository.GetAllDrivers();

                // Assert
                Assert.Equal(drivers.Count, result.Count());
            }
        }

        [Fact]
        public async Task GetDriverById_ReturnsDriver_WhenDriverExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var driverId = Guid.NewGuid().ToString();
            var expectedDriver = new DriverModel
            {
                Id = driverId,
                FirstName = "Test",
                LastName = "Driver",
                LicenseNumber = "123456",
                Email = "test@example.com",
                Role = "Driver",
                UserName = "TestDriver"
            };
            using (var context = new MSB_Database(options))
            {
                context.Drivers.Add(expectedDriver);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var driverRepository = new DriverRepository(context);

                // Act
                var result = await driverRepository.GetDriverByIdAsync(Guid.Parse(driverId));

                // Assert
                Assert.Equal(expectedDriver.Id, result.Id);
            }
        }

        [Fact]
        public async Task UpdateDriver_UpdatesDriverInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var driverId = Guid.NewGuid().ToString();
            var originalDriver = new DriverModel
            {
                Id = driverId,
                FirstName = "Original",
                LastName = "Driver",
                LicenseNumber = "123456",
                Email = "original@example.com",
                Role = "Driver",
                UserName = "OriginalDriver"
            };
            var updatedDriver = new DriverModel
            {
                Id = driverId,
                FirstName = "Updated",
                LastName = "Driver",
                LicenseNumber = "789012",
                Email = "updated@example.com",
                Role = "Driver",
                UserName = "UpdatedDriver"
            };
            using (var context = new MSB_Database(options))
            {
                context.Drivers.Add(originalDriver);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var driverRepository = new DriverRepository(context);

                // Act
                await driverRepository.UpdateDriver(updatedDriver);
                await context.SaveChangesAsync();

                // Assert
                Assert.Equal(updatedDriver.Id, context.Drivers.Single().Id);
                Assert.Equal(updatedDriver.FirstName, context.Drivers.Single().FirstName);
            }
        }

        [Fact]
        public async Task DeleteDriver_DeletesDriverFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var driverId = Guid.NewGuid().ToString();
            var driver = new DriverModel
            {
                Id = driverId,
                FirstName = "Test",
                LastName = "Driver",
                LicenseNumber = "123456",
                Email = "test@example.com",
                Role = "Driver",
                UserName = "TestDriver"
            };
            using (var context = new MSB_Database(options))
            {
                context.Drivers.Add(driver);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var driverRepository = new DriverRepository(context);

                // Act
                driverRepository.DeleteDriver(Guid.Parse(driverId));
                await context.SaveChangesAsync();

                // Assert
                Assert.Empty(context.Drivers);
            }
        }


    }
}
