using Application.Commands.Order.UpdateOrder;
using Application.Dto.Order;
using Domain.Models.Order;
using Infrastructure.Repositories.OrderRepo;
using Moq;

namespace Tests.Application.Order.CommandHandlers
{
    public class UpdateOrderCommandHandlerTests
    {
        // Test to verify that UpdateOrder calls GetOrderByIdAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsGetOrderByIdAsyncOnRepository()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.GetOrderByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((OrderModel?)new OrderModel());
            var handler = new UpdateOrderCommandHandler(mockOrderRepository.Object);
            var command = new UpdateOrderCommand(new OrderDto { OrderId = Guid.NewGuid() });

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockOrderRepository.Verify(repo => repo.GetOrderByIdAsync(command.Order.OrderId), Times.Once);
        }

        // Test to verify that UpdateOrder throws an exception
        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.GetOrderByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((OrderModel?)null);
            var handler = new UpdateOrderCommandHandler(mockOrderRepository.Object);
            var command = new UpdateOrderCommand(new OrderDto { OrderId = Guid.NewGuid() });

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }

        // Test to verify that UpdateOrder calls UpdateOrderAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsUpdateOrderAsyncOnRepository()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.GetOrderByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((OrderModel?)new OrderModel());
            var handler = new UpdateOrderCommandHandler(mockOrderRepository.Object);
            var command = new UpdateOrderCommand(new OrderDto { OrderId = Guid.NewGuid() });

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockOrderRepository.Verify(repo => repo.UpdateOrderAsync(It.IsAny<OrderModel>()), Times.Once);
        }

        // Test to verify that UpdateOrder returns updated order model
        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsUpdatedOrderModel()
        {
            // Arrange
            var updatedOrder = new OrderModel { OrderId = Guid.NewGuid() };
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.GetOrderByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((OrderModel?)new OrderModel());
            mockOrderRepository.Setup(repo => repo.UpdateOrderAsync(It.IsAny<OrderModel>()))
                .ReturnsAsync(updatedOrder);
            var handler = new UpdateOrderCommandHandler(mockOrderRepository.Object);
            var command = new UpdateOrderCommand(new OrderDto { OrderId = Guid.NewGuid() });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(updatedOrder, result);
        }
    }
}