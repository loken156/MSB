using Domain.Models.Shelf;
using Infrastructure.Database;
using Infrastructure.Repositories.ShelfRepo;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Repositories
{
    public class ShelfRepositoryTests
    {
        // Test to verify that GetAllAsync returns all shelves
        [Fact]
        public async Task GetAllAsync_ReturnsAllShelves()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var shelves = new List<ShelfModel>
            {
                new ShelfModel { ShelfId = Guid.NewGuid(), ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid() },
                new ShelfModel { ShelfId = Guid.NewGuid(), ShelfRow = 2, ShelfColumn = 2, Occupancy = true, WarehouseId = Guid.NewGuid() },
            };
            using (var context = new MSB_Database(options))
            {
                context.Shelves.AddRange(shelves);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var shelfRepository = new ShelfRepository(context);

                // Act
                var result = await shelfRepository.GetAllAsync();

                // Assert
                Assert.Equal(shelves.Count, result.Count());
            }
        }

        // Test to verify that GetShelfByIdAsync returns shelf when shelf exists
        [Fact]
        public async Task GetShelfByIdAsync_ReturnsShelf_WhenShelfExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var shelfId = Guid.NewGuid();
            var expectedShelf = new ShelfModel { ShelfId = shelfId, ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Shelves.Add(expectedShelf);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var shelfRepository = new ShelfRepository(context);

                // Act
                var result = await shelfRepository.GetShelfByIdAsync(shelfId);

                // Assert
                Assert.Equal(expectedShelf.ShelfId, result.ShelfId);
            }
        }

        // Test to verify that AddShelfAsync creates shelf in database
        [Fact]
        public async Task AddShelfAsync_CreatesShelfInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var shelf = new ShelfModel { ShelfId = Guid.NewGuid(), ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                var shelfRepository = new ShelfRepository(context);

                // Act
                var result = await shelfRepository.AddShelfAsync(shelf);

                // Assert
                Assert.Equal(shelf.ShelfId, result.ShelfId);
                Assert.Single(context.Shelves);
            }
        }

        // Test to verify that UpdateShelfAsync updates shelf in database
        [Fact]
        public async Task UpdateShelfAsync_UpdatesShelfInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var shelfId = Guid.NewGuid();
            var originalShelf = new ShelfModel { ShelfId = shelfId, ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid() };
            var updatedShelf = new ShelfModel { ShelfId = shelfId, ShelfRow = 2, ShelfColumn = 2, Occupancy = true, WarehouseId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Shelves.Add(originalShelf);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var shelfRepository = new ShelfRepository(context);

                // Act
                var result = await shelfRepository.UpdateShelfAsync(updatedShelf);

                // Assert
                Assert.Equal(updatedShelf.ShelfId, result.ShelfId);
                Assert.Equal(updatedShelf.ShelfRow, context.Shelves.Single().ShelfRow);
            }
        }

        // Test to verify that DeleteShelfAsync deletes shelf from database
        [Fact]
        public async Task DeleteShelfAsync_DeletesShelfFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var shelfId = Guid.NewGuid();
            var shelf = new ShelfModel { ShelfId = shelfId, ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Shelves.Add(shelf);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var shelfRepository = new ShelfRepository(context);

                // Act
                await shelfRepository.DeleteShelfAsync(shelfId);

                // Assert
                Assert.Empty(context.Shelves);
            }
        }
    }
}