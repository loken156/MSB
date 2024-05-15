using Domain.Models.Box;
using Infrastructure.Database;
using Infrastructure.Repositories.BoxRepo;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Repositories
{
    public class BoxRepositoryTests
    {
        private DbContextOptions<MSB_Database> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensure a unique name
                .Options;

            return options;
        }

        [Fact]
        public async Task AddBoxAsync_AddsBoxToDatabase()
        {
            // Arrange
            var options = CreateNewContextOptions();

            var box = new BoxModel { BoxId = Guid.NewGuid(), Type = "TestBox", TimesUsed = 1, Stock = 10, ImageUrl = "testUrl", UserNotes = "testNotes", Size = "Large" };
            using (var context = new MSB_Database(options))
            {
                var boxRepository = new BoxRepository(context);

                // Act
                var result = await boxRepository.AddBoxAsync(box);

                // Assert
                Assert.Equal(box.BoxId, result.BoxId);
                Assert.Single(context.Boxes);
            }
        }

        [Fact]
        public async Task DeleteBoxAsync_DeletesBoxFromDatabase()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var boxId = Guid.NewGuid();
            var box = new BoxModel { BoxId = boxId, Type = "TestBox", TimesUsed = 1, Stock = 10, ImageUrl = "testUrl", UserNotes = "testNotes", Size = "Large" };
            using (var context = new MSB_Database(options))
            {
                context.Boxes.Add(box);
                await context.SaveChangesAsync();
            }

            using (var context = new MSB_Database(options))
            {
                var boxRepository = new BoxRepository(context);

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
            var options = CreateNewContextOptions();
            var boxes = new[]
            {
                new BoxModel { BoxId = Guid.NewGuid(), Type = "TestBox1", TimesUsed = 1, Stock = 10, ImageUrl = "testUrl1", UserNotes = "testNotes1", Size = "Large" },
                new BoxModel { BoxId = Guid.NewGuid(), Type = "TestBox2", TimesUsed = 2, Stock = 20, ImageUrl = "testUrl2", UserNotes = "testNotes2", Size = "Small" },
            };

            using (var context = new MSB_Database(options))
            {
                context.Boxes.AddRange(boxes);
                await context.SaveChangesAsync();
            }

            using (var context = new MSB_Database(options))
            {
                var boxRepository = new BoxRepository(context);

                // Act
                var result = await boxRepository.GetAllBoxesAsync();

                // Assert
                Assert.Equal(boxes.Length, result.Count());
            }
        }

        [Fact]
        public async Task GetBoxByIdAsync_ReturnsBox_WhenBoxExists()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var boxId = Guid.NewGuid();
            var expectedBox = new BoxModel { BoxId = boxId, Type = "TestBox", TimesUsed = 1, Stock = 10, ImageUrl = "testUrl", UserNotes = "testNotes", Size = "Large" };

            using (var context = new MSB_Database(options))
            {
                context.Boxes.Add(expectedBox);
                await context.SaveChangesAsync();
            }

            using (var context = new MSB_Database(options))
            {
                var boxRepository = new BoxRepository(context);

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
            var options = CreateNewContextOptions();
            var boxId = Guid.NewGuid();
            var originalBox = new BoxModel { BoxId = boxId, Type = "OriginalBox", TimesUsed = 1, Stock = 10, ImageUrl = "originalUrl", UserNotes = "originalNotes", Size = "Large" };

            using (var context = new MSB_Database(options))
            {
                context.Boxes.Add(originalBox);
                await context.SaveChangesAsync();
            }

            var updatedBox = new BoxModel { BoxId = boxId, Type = "UpdatedBox", TimesUsed = 2, Stock = 20, ImageUrl = "updatedUrl", UserNotes = "updatedNotes", Size = "Small" };

            using (var context = new MSB_Database(options))
            {
                var boxRepository = new BoxRepository(context);

                // Act
                var result = await boxRepository.UpdateBoxAsync(updatedBox);

                // Assert
                Assert.Equal(updatedBox.BoxId, result.BoxId);
                var updatedEntry = context.Boxes.Single();
                Assert.Equal("UpdatedBox", updatedEntry.Type);
                Assert.Equal(2, updatedEntry.TimesUsed);
                Assert.Equal(20, updatedEntry.Stock);
                Assert.Equal("updatedUrl", updatedEntry.ImageUrl);
                Assert.Equal("updatedNotes", updatedEntry.UserNotes);
                Assert.Equal("Small", updatedEntry.Size);
            }
        }
    }
}