using Domain.Models.Address;
using Infrastructure.Database;
using Infrastructure.Repositories.OrderRepo;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Repositories
{
    public class AddressRepositoryTests
    {
        [Fact]
        public async Task AddAddressAsync_AddsAddressToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique in-memory database for each test
                .Options;
            var address = new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Test Street", UserId = "TestUserId" };
            using (var context = new MSB_Database(options))
            {
                var addressRepository = new AddressRepository(context);

                // Act
                await addressRepository.AddAddressAsync(address);
                await context.SaveChangesAsync(); // Ensure changes are saved to the database

                // Assert
                var savedAddress = await context.Addresses.FirstAsync();
                Assert.Equal(address.AddressId, savedAddress.AddressId);
                Assert.Single(context.Addresses);
            }
        }

        [Fact]
        public async Task GetAllAddressesAsync_ReturnsAllAddresses()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique in-memory database for each test
                .Options;
            var addresses = new List<AddressModel>
            {
                new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Test Street 1", UserId = "TestUserId1" },
                new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Test Street 2", UserId = "TestUserId2" },
            };
            using (var context = new MSB_Database(options))
            {
                context.Addresses.AddRange(addresses);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var addressRepository = new AddressRepository(context);

                // Act
                var result = await addressRepository.GetAllAddressesAsync();

                // Assert
                Assert.Equal(addresses.Count, result.Count());
            }
        }

        [Fact]
        public async Task GetAddressByIdAsync_ReturnsAddress_WhenAddressExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique in-memory database for each test
                .Options;
            var addressId = Guid.NewGuid();
            var expectedAddress = new AddressModel { AddressId = addressId, StreetName = "Test Street", UserId = "TestUserId" };
            using (var context = new MSB_Database(options))
            {
                context.Addresses.Add(expectedAddress);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var addressRepository = new AddressRepository(context);

                // Act
                var result = await addressRepository.GetAddressByIdAsync(addressId);

                // Assert
                Assert.Equal(expectedAddress.AddressId, result.AddressId);
            }
        }

        [Fact]
        public async Task UpdateAddressAsync_UpdatesAddressInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique in-memory database for each test
                .Options;
            var addressId = Guid.NewGuid();
            var originalAddress = new AddressModel { AddressId = addressId, StreetName = "Original Street", UserId = "TestUserId" };
            var updatedAddress = new AddressModel { AddressId = addressId, StreetName = "Updated Street", UserId = "TestUserId" };
            using (var context = new MSB_Database(options))
            {
                context.Addresses.Add(originalAddress);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var addressRepository = new AddressRepository(context);

                // Act
                var result = await addressRepository.UpdateAddressAsync(updatedAddress);

                // Assert
                Assert.Equal(updatedAddress.AddressId, result.AddressId);
                Assert.Equal(updatedAddress.StreetName, context.Addresses.Single().StreetName);
            }
        }

        [Fact]
        public async Task DeleteAddressAsync_DeletesAddressFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique in-memory database for each test
                .Options;
            var addressId = Guid.NewGuid();
            var address = new AddressModel { AddressId = addressId, StreetName = "Test Street", UserId = "TestUserId" };
            using (var context = new MSB_Database(options))
            {
                context.Addresses.Add(address);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var addressRepository = new AddressRepository(context);

                // Act
                await addressRepository.DeleteAddressAsync(addressId);

                // Assert
                Assert.Empty(context.Addresses);
            }
        }
    }
}