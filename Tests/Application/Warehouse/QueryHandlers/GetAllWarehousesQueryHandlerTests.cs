using Application.Queries.Warehouse.GetAll;
using Domain.Models.Warehouse;
using Infrastructure.Repositories.WarehouseRepo;
using Moq;

namespace Tests.Application.Warehouse.QueryHandlers
{
    public class GetAllWarehousesQueryHandlerTests
    {
        private readonly Mock<IWarehouseRepository> _mockWarehouseRepository;
        private readonly GetAllWarehousesQueryHandler _handler;

        public GetAllWarehousesQueryHandlerTests()
        {
            _mockWarehouseRepository = new Mock<IWarehouseRepository>();
            _handler = new GetAllWarehousesQueryHandler(_mockWarehouseRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllWarehouses()
        {
            // Arrange
            var warehouses = new List<WarehouseModel>
            {
                new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "Warehouse 1" },
                new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "Warehouse 2" }
            };
            _mockWarehouseRepository.Setup(m => m.GetAllWarehousesAsync()).ReturnsAsync(warehouses);

            // Act
            var result = await _handler.Handle(new GetAllWarehousesQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(warehouses.Count, result.Count());
        }

        [Fact]
        public async Task Handle_MapsWarehouseModelCorrectly()
        {
            // Arrange
            var warehouse = new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "Warehouse 1" };
            var warehouses = new List<WarehouseModel> { warehouse };
            _mockWarehouseRepository.Setup(m => m.GetAllWarehousesAsync()).ReturnsAsync(warehouses);

            // Act
            var result = await _handler.Handle(new GetAllWarehousesQuery(), CancellationToken.None);
            var warehouseDto = result.First();

            // Assert
            Assert.Equal(warehouse.WarehouseId, warehouseDto.WarehouseId);
            Assert.Equal(warehouse.WarehouseName, warehouseDto.WarehouseName);
        }
    }
}