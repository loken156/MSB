using Application.Queries.Shelf.GetAll;
using Domain.Models.Shelf;
using Infrastructure.Repositories.ShelfRepo;
using Moq;

namespace Tests.Application.Shelf.QueryHandlers
{
    public class GetAllShelvesQueryHandlerTests
    {
        private readonly Mock<IShelfRepository> _mockShelfRepository;
        private readonly GetAllShelvesQueryHandler _handler;

        public GetAllShelvesQueryHandlerTests()
        {
            _mockShelfRepository = new Mock<IShelfRepository>();
            _handler = new GetAllShelvesQueryHandler(_mockShelfRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllShelves()
        {
            // Arrange
            var shelves = new List<ShelfModel>
            {
                new ShelfModel { ShelfId = Guid.NewGuid(), ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid() },
                new ShelfModel { ShelfId = Guid.NewGuid(), ShelfRow = 2, ShelfColumn = 2, Occupancy = true, WarehouseId = Guid.NewGuid() }
            };
            _mockShelfRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(shelves);

            // Act
            var result = await _handler.Handle(new GetAllShelvesQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(shelves.Count, result.Count());
        }

        [Fact]
        public async Task Handle_MapsShelfModelCorrectly()
        {
            // Arrange
            var shelf = new ShelfModel { ShelfId = Guid.NewGuid(), ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid() };
            var shelves = new List<ShelfModel> { shelf };
            _mockShelfRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(shelves);

            // Act
            var result = await _handler.Handle(new GetAllShelvesQuery(), CancellationToken.None);
            var shelfDto = result.First();

            // Assert
            Assert.Equal(shelf.ShelfId, shelfDto.ShelfId);
            Assert.Equal(shelf.ShelfRow, shelfDto.ShelfRow);
            Assert.Equal(shelf.ShelfColumn, shelfDto.ShelfColumn);
            Assert.Equal(shelf.Occupancy, shelfDto.Occupancy);
            Assert.Equal(shelf.WarehouseId, shelfDto.WarehouseId);
        }
    }
}
