using Application.Queries.Shelf.GetByID;
using Domain.Models.Shelf;
using Infrastructure.Repositories.ShelfRepo;
using Moq;

namespace Tests.Application.Shelf.QueryHandlers
{
    public class GetShelfByIdQueryHandlerTests
    {
        private readonly Mock<IShelfRepository> _mockShelfRepository;
        private readonly GetShelfByIdQueryHandler _handler;

        public GetShelfByIdQueryHandlerTests()
        {
            _mockShelfRepository = new Mock<IShelfRepository>();
            _handler = new GetShelfByIdQueryHandler(_mockShelfRepository.Object);
        }

        // Test to verify that GetShelfById calls GetShelfByIdAsync on repository
        [Fact]
        public async Task Handle_GivenValidId_ReturnsShelfDto()
        {
            // Arrange
            var shelfId = Guid.NewGuid();
            var shelf = new ShelfModel
            {
                ShelfId = shelfId,
                ShelfRows = 1,
                ShelfColumn = 1,
                Occupancy = false,
                WarehouseId = Guid.NewGuid()
            };
            _mockShelfRepository.Setup(m => m.GetShelfByIdAsync(shelfId)).ReturnsAsync(shelf);

            // Act
            var result = await _handler.Handle(new GetShelfByIdQuery(shelfId), CancellationToken.None);

            // Assert
            Assert.Equal(shelf.ShelfId, result.ShelfId);
            Assert.Equal(shelf.ShelfRows, result.ShelfRows);
            Assert.Equal(shelf.ShelfColumn, result.ShelfColumn);
            Assert.Equal(shelf.Occupancy, result.Occupancy);
            Assert.Equal(shelf.WarehouseId, result.WarehouseId);
        }

        // Test to verify that GetShelfById throws an exception
        [Fact]
        public async Task Handle_GivenInvalidId_ThrowsException()
        {
            // Arrange
            var shelfId = Guid.NewGuid();
            _mockShelfRepository.Setup(m => m.GetShelfByIdAsync(shelfId)).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetShelfByIdQuery(shelfId), CancellationToken.None));
        }
    }
}