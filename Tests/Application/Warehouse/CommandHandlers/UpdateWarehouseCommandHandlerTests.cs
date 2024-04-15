using Application.Commands.Warehouse.UpdateWarehouse;
using Application.Dto.Warehouse;
using Domain.Models.Warehouse;
using Infrastructure.Repositories.WarehouseRepo;
using Moq;

namespace Tests.Application.Warehouse.CommandHandlers
{
    public class UpdateWarehouseCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CallsUpdateWarehouseAsyncOnRepository()
        {
            // Arrange
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            var handler = new UpdateWarehouseCommandHandler(mockWarehouseRepository.Object);
            var command = new UpdateWarehouseCommand(new WarehouseDto { WarehouseId = Guid.NewGuid(), WarehouseName = "Test Warehouse" });

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockWarehouseRepository.Verify(repo => repo.UpdateWarehouseAsync(It.IsAny<WarehouseModel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsUpdatedWarehouseModel()
        {
            // Arrange
            var updatedWarehouse = new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "Test Warehouse" };
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            mockWarehouseRepository.Setup(repo => repo.UpdateWarehouseAsync(It.IsAny<WarehouseModel>()))
                .ReturnsAsync(updatedWarehouse);
            var handler = new UpdateWarehouseCommandHandler(mockWarehouseRepository.Object);
            var command = new UpdateWarehouseCommand(new WarehouseDto { WarehouseId = Guid.NewGuid(), WarehouseName = "Test Warehouse" });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(updatedWarehouse, result);
        }
    }
}

