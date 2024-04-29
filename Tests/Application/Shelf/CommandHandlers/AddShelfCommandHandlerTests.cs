using Application.Commands.Shelf.AddShelf;
using Application.Dto.AddShelf;
using Application.Dto.Box;
using Domain.Models.Shelf;
using Domain.Models.Warehouse;
using Infrastructure.Repositories.ShelfRepo;
using Infrastructure.Repositories.WarehouseRepo;
using Moq;

namespace Tests.Application.Shelf.CommandHandlers
{
    public class AddShelfCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CallsGetWarehouseByIdAsyncOnRepository()
        {
            // Arrange
            var mockShelfRepository = new Mock<IShelfRepository>();
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new WarehouseModel());
            var handler = new AddShelfCommandHandler(mockShelfRepository.Object, mockWarehouseRepository.Object);
            var command = new AddShelfCommand(new AddShelfDto { ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid(), Boxes = new List<BoxDto>() }, Guid.NewGuid(), new List<BoxDto>());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockWarehouseRepository.Verify(repo => repo.GetWarehouseByIdAsync(command.WarehouseId), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockShelfRepository = new Mock<IShelfRepository>();
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((WarehouseModel)null);
            var handler = new AddShelfCommandHandler(mockShelfRepository.Object, mockWarehouseRepository.Object);
            var command = new AddShelfCommand(new AddShelfDto { ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid(), Boxes = new List<BoxDto>() }, Guid.NewGuid(), new List<BoxDto>());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenValidCommand_CallsAddShelfAsyncOnRepository()
        {
            // Arrange
            var mockShelfRepository = new Mock<IShelfRepository>();
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new WarehouseModel());
            var handler = new AddShelfCommandHandler(mockShelfRepository.Object, mockWarehouseRepository.Object);
            var command = new AddShelfCommand(new AddShelfDto { ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid(), Boxes = new List<BoxDto>() }, Guid.NewGuid(), new List<BoxDto>());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockShelfRepository.Verify(repo => repo.AddShelfAsync(It.IsAny<ShelfModel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsCreatedShelfModel()
        {
            // Arrange
            var createdShelf = new ShelfModel { ShelfId = Guid.NewGuid() };
            var mockShelfRepository = new Mock<IShelfRepository>();
            mockShelfRepository.Setup(repo => repo.AddShelfAsync(It.IsAny<ShelfModel>()))
                .ReturnsAsync(createdShelf);
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new WarehouseModel());
            var handler = new AddShelfCommandHandler(mockShelfRepository.Object, mockWarehouseRepository.Object);
            var command = new AddShelfCommand(new AddShelfDto { ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid(), Boxes = new List<BoxDto>() }, Guid.NewGuid(), new List<BoxDto>());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(createdShelf, result);
        }




    }
}