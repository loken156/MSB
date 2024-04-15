using Application.Commands.Shelf.UpdateShelf;
using Application.Dto.Shelf;
using Domain.Models.Shelf;
using Infrastructure.Repositories.ShelfRepo;
using Moq;

namespace Tests.Application.Shelf.CommandHandlers
{
    public class UpdateShelfCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CallsUpdateShelfAsyncOnRepository()
        {
            // Arrange
            var mockShelfRepository = new Mock<IShelfRepository>();
            var handler = new UpdateShelfCommandHandler(mockShelfRepository.Object);
            var command = new UpdateShelfCommand(new ShelfDto { ShelfId = Guid.NewGuid(), ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid() });

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockShelfRepository.Verify(repo => repo.UpdateShelfAsync(It.Is<ShelfModel>(model => model.ShelfId == command.UpdatedShelf.ShelfId)), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsUpdatedShelfModel()
        {
            // Arrange
            var updatedShelf = new ShelfModel { ShelfId = Guid.NewGuid() };
            var mockShelfRepository = new Mock<IShelfRepository>();
            mockShelfRepository.Setup(repo => repo.UpdateShelfAsync(It.IsAny<ShelfModel>()))
                .ReturnsAsync(updatedShelf);
            var handler = new UpdateShelfCommandHandler(mockShelfRepository.Object);
            var command = new UpdateShelfCommand(new ShelfDto { ShelfId = updatedShelf.ShelfId, ShelfRow = 1, ShelfColumn = 1, Occupancy = false, WarehouseId = Guid.NewGuid() });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(updatedShelf, result);
        }
    }
}
