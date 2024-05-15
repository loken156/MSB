using Application.Commands.Box.AddBox;
using Application.Commands.Order.AddOrder;
using Application.Dto.Box;
using Application.Dto.Order;
using AutoMapper;
using Domain.Models.Box;
using Domain.Models.Order;
using Domain.Models.Warehouse;
using Infrastructure.Repositories.OrderRepo;
using Infrastructure.Repositories.WarehouseRepo;
using MediatR;
using Moq;

namespace Tests.Application.Order.CommandHandlers
{
    public class AddOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IWarehouseRepository> _mockWarehouseRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IMediator> _mockMediator;
        private readonly AddOrderCommandHandler _handler;

        public AddOrderCommandHandlerTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockWarehouseRepository = new Mock<IWarehouseRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockMediator = new Mock<IMediator>();
            _handler = new AddOrderCommandHandler(_mockOrderRepository.Object, _mockWarehouseRepository.Object, _mockMapper.Object, _mockMediator.Object);

            // Setup default behaviors for mapper
            _mockMapper.Setup(m => m.Map<OrderModel>(It.IsAny<AddOrderDto>())).Returns(new OrderModel());
            _mockMapper.Setup(m => m.Map<BoxModel>(It.IsAny<AddBoxToOrderDto>())).Returns(new BoxModel());
            _mockMapper.Setup(m => m.Map<BoxDto>(It.IsAny<BoxModel>())).Returns(new BoxDto());
        }

        [Fact]
        public async Task Handle_GivenValidCommand_CallsGetWarehouseByIdAsyncOnRepository()
        {
            // Arrange
            _mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new WarehouseModel());
            var command = new AddOrderCommand(new AddOrderDto(), Guid.NewGuid());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockWarehouseRepository.Verify(repo => repo.GetWarehouseByIdAsync(command.WarehouseId), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            _mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((WarehouseModel)null);
            var command = new AddOrderCommand(new AddOrderDto(), Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenValidCommand_CallsAddOrderAsyncOnRepository()
        {
            // Arrange
            _mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new WarehouseModel());
            var command = new AddOrderCommand(new AddOrderDto(), Guid.NewGuid());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockOrderRepository.Verify(repo => repo.AddOrderAsync(It.IsAny<OrderModel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsCreatedOrderModel()
        {
            // Arrange
            var createdOrder = new OrderModel { OrderId = Guid.NewGuid() };
            _mockOrderRepository.Setup(repo => repo.AddOrderAsync(It.IsAny<OrderModel>()))
                .ReturnsAsync(createdOrder);
            _mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new WarehouseModel());
            var command = new AddOrderCommand(new AddOrderDto(), Guid.NewGuid());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(createdOrder, result);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_AddsBoxesToOrder()
        {
            // Arrange
            var command = new AddOrderCommand(new AddOrderDto { Boxes = new[] { new AddBoxToOrderDto() }.ToList() }, Guid.NewGuid());
            _mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new WarehouseModel());
            var createdOrder = new OrderModel { OrderId = Guid.NewGuid() };
            _mockOrderRepository.Setup(repo => repo.AddOrderAsync(It.IsAny<OrderModel>()))
                .ReturnsAsync(createdOrder);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockMediator.Verify(m => m.Send(It.IsAny<AddBoxCommand>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }

    }
}