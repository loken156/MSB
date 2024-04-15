using Application.Commands.Order.AddOrder;
using Application.Dto.Order;
using Domain.Models.Order;
using Domain.Models.Warehouse;
using Infrastructure.Repositories.OrderRepo;
using Infrastructure.Repositories.WarehouseRepo;
using Moq;

namespace Tests.Application.Order.CommandHandlers
{
    public class AddOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CallsGetWarehouseByIdAsyncOnRepository()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new WarehouseModel());
            var handler = new AddOrderCommandHandler(mockOrderRepository.Object, mockWarehouseRepository.Object);
            var command = new AddOrderCommand(new OrderDto(), Guid.NewGuid());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockWarehouseRepository.Verify(repo => repo.GetWarehouseByIdAsync(command.WarehouseId), Times.Once);
        }


        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((WarehouseModel)null);
            var handler = new AddOrderCommandHandler(mockOrderRepository.Object, mockWarehouseRepository.Object);
            var command = new AddOrderCommand(new OrderDto(), Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_GivenValidCommand_CallsAddOrderAsyncOnRepository()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new WarehouseModel());
            var handler = new AddOrderCommandHandler(mockOrderRepository.Object, mockWarehouseRepository.Object);
            var command = new AddOrderCommand(new OrderDto(), Guid.NewGuid());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockOrderRepository.Verify(repo => repo.AddOrderAsync(It.IsAny<OrderModel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsCreatedOrderModel()
        {
            // Arrange
            var createdOrder = new OrderModel { OrderId = Guid.NewGuid() };
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.AddOrderAsync(It.IsAny<OrderModel>()))
                .ReturnsAsync(createdOrder);
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            mockWarehouseRepository.Setup(repo => repo.GetWarehouseByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new WarehouseModel());
            var handler = new AddOrderCommandHandler(mockOrderRepository.Object, mockWarehouseRepository.Object);
            var command = new AddOrderCommand(new OrderDto(), Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(createdOrder, result);
        }

    }
}
