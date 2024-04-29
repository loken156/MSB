using Application.Queries.Warehouse.GetByID;
using Domain.Models.Warehouse;
using Infrastructure.Repositories.WarehouseRepo;
using Moq;

namespace Tests.Application.Warehouse.QueryHandlers
{
    public class GetWarehouseByIdQueryHandlerTests
    {
        private readonly Mock<IWarehouseRepository> _mockWarehouseRepository;
        private readonly GetWarehouseByIdQueryHandler _handler;

        public GetWarehouseByIdQueryHandlerTests()
        {
            _mockWarehouseRepository = new Mock<IWarehouseRepository>();
            _handler = new GetWarehouseByIdQueryHandler(_mockWarehouseRepository.Object);
        }

        [Fact]
        public async Task Handle_GivenValidId_ReturnsWarehouseDto()
        {
            // Arrange
            var warehouseId = Guid.NewGuid();
            var warehouse = new WarehouseModel
            {
                WarehouseId = warehouseId,
                WarehouseName = "Warehouse 1"
            };
            _mockWarehouseRepository.Setup(m => m.GetWarehouseByIdAsync(warehouseId)).ReturnsAsync(warehouse);

            // Act
            var result = await _handler.Handle(new GetWarehouseByIdQuery(warehouseId), CancellationToken.None);

            // Assert
            Assert.Equal(warehouse.WarehouseId, result.WarehouseId);
            Assert.Equal(warehouse.WarehouseName, result.WarehouseName);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ThrowsException()
        {
            // Arrange
            var warehouseId = Guid.NewGuid();
            _mockWarehouseRepository.Setup(m => m.GetWarehouseByIdAsync(warehouseId)).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetWarehouseByIdQuery(warehouseId), CancellationToken.None));
        }
    }
}