using Application.Commands.Warehouse.AddWarehouse;
using Application.Dto.AddWarehouse;
using Domain.Models.Warehouse;
using Infrastructure.Repositories.WarehouseRepo;
using Moq;

namespace Tests.Application.Warehouse.CommandHandlers
{
    public class AddWarehouseCommandHandlerTests
    {
        // Test to verify that AddWarehouse calls AddWarehouseAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsAddWarehouseAsyncOnRepository()
        {
            // Arrange
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            var handler = new AddWarehouseCommandHandler(mockWarehouseRepository.Object);
            var command = new AddWarehouseCommand(new AddWarehouseDto { WarehouseName = "Test Warehouse" });

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockWarehouseRepository.Verify(repo => repo.AddWarehouseAsync(It.IsAny<WarehouseModel>()), Times.Once);
        }

        // Test to verify that AddWarehouse returns created warehouse model
        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsCreatedWarehouseModel()
        {
            // Arrange
            var createdWarehouse = new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "Test Warehouse" };
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            mockWarehouseRepository.Setup(repo => repo.AddWarehouseAsync(It.IsAny<WarehouseModel>()))
                .ReturnsAsync(createdWarehouse);
            var handler = new AddWarehouseCommandHandler(mockWarehouseRepository.Object);
            var command = new AddWarehouseCommand(new AddWarehouseDto { WarehouseName = "Test Warehouse" });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(createdWarehouse, result);
        }

    }
}