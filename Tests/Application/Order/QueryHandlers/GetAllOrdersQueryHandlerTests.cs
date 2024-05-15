using Application.Queries.Order.GetAll;
using Domain.Models.Order;
using Infrastructure.Repositories.OrderRepo;
using Moq;

namespace Tests.Application.Order.QueryHandlers
{
    public class GetAllOrdersQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly GetAllOrdersQueryHandler _handler;

        public GetAllOrdersQueryHandlerTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _handler = new GetAllOrdersQueryHandler(_mockOrderRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllOrders()
        {
            // Arrange
            var orders = new List<OrderModel>
            {
                new OrderModel { OrderId = Guid.NewGuid(), OrderNumber = 1, OrderDate = DateTime.Now, TotalCost = 100, UserId = "1", CarId = Guid.NewGuid() },
                new OrderModel { OrderId = Guid.NewGuid(), OrderNumber = 2, OrderDate = DateTime.Now, TotalCost = 200, UserId = "2", CarId = Guid.NewGuid() }
            };
            _mockOrderRepository.Setup(m => m.GetAllOrdersAsync()).ReturnsAsync(orders);

            // Act
            var result = await _handler.Handle(new GetAllOrdersQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(orders.Count, result.Count());
        }

        [Fact]
        public async Task Handle_MapsOrderModelCorrectly()
        {
            // Arrange
            var order = new OrderModel { OrderId = Guid.NewGuid(), OrderNumber = 1, OrderDate = DateTime.Now, TotalCost = 100, UserId = "1", CarId = Guid.NewGuid() };
            var orders = new List<OrderModel> { order };
            _mockOrderRepository.Setup(m => m.GetAllOrdersAsync()).ReturnsAsync(orders);

            // Act
            var result = await _handler.Handle(new GetAllOrdersQuery(), CancellationToken.None);
            var retrievedOrder = result.First();

            // Assert
            Assert.Equal(order.OrderId, retrievedOrder.OrderId);
            Assert.Equal(order.OrderNumber, retrievedOrder.OrderNumber);
            Assert.Equal(order.OrderDate, retrievedOrder.OrderDate);
            Assert.Equal(order.TotalCost, retrievedOrder.TotalCost);
            Assert.Equal(order.UserId, retrievedOrder.UserId);
            Assert.Equal(order.CarId, retrievedOrder.CarId);

        }
    }
}
