using Application.Commands.Order.DeleteOrder;
using Infrastructure.Repositories.OrderRepo;
using Moq;

namespace Tests.Application.Order.CommandHandlers
{
    public class DeleteOrderCommandHandlerTests
    {
        // Test to verify that DeleteOrder calls DeleteOrderAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsDeleteOrderAsyncOnRepository()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var handler = new DeleteOrderCommandHandler(mockOrderRepository.Object);
            var command = new DeleteOrderCommand(Guid.NewGuid());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockOrderRepository.Verify(repo => repo.DeleteOrderAsync(command.OrderId), Times.Once);
        }

        // Test to verify that DeleteOrder does not throw an exception
        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(repo => repo.DeleteOrderAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception());
            var handler = new DeleteOrderCommandHandler(mockOrderRepository.Object);
            var command = new DeleteOrderCommand(Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}