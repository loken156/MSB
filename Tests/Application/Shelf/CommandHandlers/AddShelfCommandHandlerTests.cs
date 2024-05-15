using Application.Commands.Shelf.AddShelf;
using Application.Dto.AddShelf;
using AutoMapper;
using Domain.Models.Shelf;
using Domain.Models.Warehouse;
using Infrastructure.Repositories.ShelfRepo;
using Infrastructure.Repositories.WarehouseRepo;
using Moq;

namespace Tests.Application.Shelf.CommandHandlers
{
    public class AddShelfCommandHandlerTests
    {
        private readonly Mock<IShelfRepository> _mockShelfRepository;
        private readonly Mock<IWarehouseRepository> _mockWarehouseRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddShelfCommandHandler _handler;

        public AddShelfCommandHandlerTests()
        {
            _mockShelfRepository = new Mock<IShelfRepository>();
            _mockWarehouseRepository = new Mock<IWarehouseRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new AddShelfCommandHandler(_mockShelfRepository.Object, _mockWarehouseRepository.Object, _mockMapper.Object);

            // Setup default behaviors for mapper
            _mockMapper.Setup(m => m.Map<ShelfModel>(It.IsAny<AddShelfDto>())).Returns(new ShelfModel());
        }

        [Fact]
        public async Task Handle_GivenValidCommand_CallsGetWarehouseByNameAsyncOnRepository()
        {
            // Arrange
            var warehouseName = "TestWarehouse";
            _mockWarehouseRepository.Setup(repo => repo.GetWarehouseByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new WarehouseModel());
            var command = new AddShelfCommand(new AddShelfDto(), warehouseName);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockWarehouseRepository.Verify(repo => repo.GetWarehouseByNameAsync(warehouseName), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var warehouseName = "InvalidWarehouse";
            _mockWarehouseRepository.Setup(repo => repo.GetWarehouseByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((WarehouseModel)null);
            var command = new AddShelfCommand(new AddShelfDto(), warehouseName);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenValidCommand_CallsAddShelfAsyncOnRepository()
        {
            // Arrange
            var warehouseName = "TestWarehouse";
            _mockWarehouseRepository.Setup(repo => repo.GetWarehouseByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new WarehouseModel());
            var command = new AddShelfCommand(new AddShelfDto(), warehouseName);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockShelfRepository.Verify(repo => repo.AddShelfAsync(It.IsAny<ShelfModel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsCreatedShelfModel()
        {
            // Arrange
            var createdShelf = new ShelfModel { ShelfId = Guid.NewGuid() };
            _mockShelfRepository.Setup(repo => repo.AddShelfAsync(It.IsAny<ShelfModel>()))
                .ReturnsAsync(createdShelf);
            _mockWarehouseRepository.Setup(repo => repo.GetWarehouseByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new WarehouseModel());
            var command = new AddShelfCommand(new AddShelfDto(), "TestWarehouse");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(createdShelf, result);
        }
    }
}