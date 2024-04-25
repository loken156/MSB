using Domain.Models.Box;
using Domain.Models.Shelf;
using Infrastructure.Database;
using Infrastructure.Repositories.BoxRepo;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Repositories
{
    public class BoxRepositoryTests
    {
        [Fact]
        public async Task AddBoxAsync_AddsBoxToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var shelfId = Guid.NewGuid();
            var shelf = new ShelfModel { ShelfId = shelfId, ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid() };

            var box = new BoxModel { BoxId = Guid.NewGuid(), Type = "TestBox", TimesUsed = 1, Stock = 10, ImageUrl = "testUrl", UserNotes = "testNotes", OrderId = Guid.NewGuid(), Size = "Large", ShelfId = shelfId };
            using (var context = new MSB_Database(options))
            {
                context.Shelves.Add(shelf);
                await context.SaveChangesAsync();
                IBoxRepository boxRepository = new BoxRepository(context);

                // Act
                var result = await boxRepository.AddBoxAsync(box, shelfId);

                // Assert
                Assert.Equal(box.BoxId, result.BoxId);
                Assert.Single(context.Boxes);
            }
        }

        [Fact]
        public async Task DeleteBoxAsync_DeletesBoxFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var boxId = Guid.NewGuid();
            var box = new BoxModel { BoxId = boxId, Type = "TestBox", TimesUsed = 1, Stock = 10, ImageUrl = "testUrl", UserNotes = "testNotes", OrderId = Guid.NewGuid(), Size = "Large", ShelfId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Boxes.Add(box);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                IBoxRepository boxRepository = new BoxRepository(context);

                // Act
                await boxRepository.DeleteBoxAsync(boxId);

                // Assert
                Assert.Empty(context.Boxes);
            }
        }

        [Fact]
        public async Task GetAllBoxesAsync_ReturnsAllBoxes()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var boxes = new List<BoxModel>
            {
                new BoxModel { BoxId = Guid.NewGuid(), Type = "TestBox1", TimesUsed = 1, Stock = 10, ImageUrl = "testUrl1", UserNotes = "testNotes1", OrderId = Guid.NewGuid(), Size = "Large", ShelfId = Guid.NewGuid() },
                new BoxModel { BoxId = Guid.NewGuid(), Type = "TestBox2", TimesUsed = 2, Stock = 20, ImageUrl = "testUrl2", UserNotes = "testNotes2", OrderId = Guid.NewGuid(), Size = "Small", ShelfId = Guid.NewGuid() },
            };
            using (var context = new MSB_Database(options))
            {
                context.Boxes.AddRange(boxes);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                IBoxRepository boxRepository = new BoxRepository(context);

                // Act
                var result = await boxRepository.GetAllBoxesAsync();

                // Assert
                Assert.Equal(boxes.Count, result.Count());
            }
        }

        [Fact]
        public async Task GetBoxByIdAsync_ReturnsBox_WhenBoxExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var boxId = Guid.NewGuid();
            var expectedBox = new BoxModel { BoxId = boxId, Type = "TestBox", TimesUsed = 1, Stock = 10, ImageUrl = "testUrl", UserNotes = "testNotes", OrderId = Guid.NewGuid(), Size = "Large", ShelfId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Boxes.Add(expectedBox);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                IBoxRepository boxRepository = new BoxRepository(context);

                // Act
                var result = await boxRepository.GetBoxByIdAsync(boxId);

                // Assert
                Assert.Equal(expectedBox.BoxId, result.BoxId);
            }
        }

        [Fact]
        public async Task UpdateBoxAsync_UpdatesBoxInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var boxId = Guid.NewGuid();
            var originalBox = new BoxModel { BoxId = boxId, Type = "OriginalBox", TimesUsed = 1, Stock = 10, ImageUrl = "originalUrl", UserNotes = "originalNotes", OrderId = Guid.NewGuid(), Size = "Large", ShelfId = Guid.NewGuid() };
            var updatedBox = new BoxModel { BoxId = boxId, Type = "UpdatedBox", TimesUsed = 2, Stock = 20, ImageUrl = "updatedUrl", UserNotes = "updatedNotes", OrderId = Guid.NewGuid(), Size = "Small", ShelfId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Boxes.Add(originalBox);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                IBoxRepository boxRepository = new BoxRepository(context);

                // Act
                var result = await boxRepository.UpdateBoxAsync(updatedBox);

                // Assert
                Assert.Equal(updatedBox.BoxId, result.BoxId);
                Assert.Equal(updatedBox.Type, context.Boxes.Single().Type);
            }
        }
    }
}