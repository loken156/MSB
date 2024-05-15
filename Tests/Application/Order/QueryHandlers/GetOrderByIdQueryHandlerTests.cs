using Application.Queries.Order.GetByID;
using Domain.Models.Order;
using Infrastructure.Repositories.OrderRepo;
using Moq;

namespace Tests.Application.Order.QueryHandlers
{
    public class GetOrderByIdQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly GetOrderByIdQueryHandler _handler;

        public GetOrderByIdQueryHandlerTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _handler = new GetOrderByIdQueryHandler(_mockOrderRepository.Object);
        }

        [Fact]
        public async Task Handle_GivenValidId_ReturnsOrderDto()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new OrderModel
            {
                OrderId = orderId,
                OrderNumber = 1,
                OrderDate = DateTime.Now,
                TotalCost = 100,
                UserId = "1",
                CarId = Guid.NewGuid(),
            };
            _mockOrderRepository.Setup(m => m.GetOrderByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _handler.Handle(new GetOrderByIdQuery(orderId), CancellationToken.None);

            // Assert
            Assert.Equal(order.OrderId, result.OrderId);
            Assert.Equal(order.OrderNumber, result.OrderNumber);
            Assert.Equal(order.OrderDate, result.OrderDate);
            Assert.Equal(order.TotalCost, result.TotalCost);
            Assert.Equal(order.UserId, result.UserId);
            Assert.Equal(order.CarId, result.CarId);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ThrowsException()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mockOrderRepository.Setup(m => m.GetOrderByIdAsync(orderId)).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetOrderByIdQuery(orderId), CancellationToken.None));
        }
    }
}