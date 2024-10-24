﻿/*using Domain.Models.Order;
using Infrastructure.Database;
using Infrastructure.Repositories.OrderRepo;
using Infrastructure.Services.Notification;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Infrastructure.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly Mock<INotificationService> _notificationServiceMock;

        public OrderRepositoryTests()
        {
            _notificationServiceMock = new Mock<INotificationService>();
        }

        // Test to verify that GetAllOrdersAsync returns all orders
        [Fact]
        public async Task GetAllOrdersAsync_ReturnsAllOrders()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var orders = new List<OrderModel>
            {
                new OrderModel { OrderId = Guid.NewGuid(), OrderNumber = 1, OrderDate = DateTime.Now, TotalCost = 100.0m, UserId = Guid.NewGuid().ToString(), CarId = Guid.NewGuid() },
                new OrderModel { OrderId = Guid.NewGuid(), OrderNumber = 2, OrderDate = DateTime.Now, TotalCost = 200.0m, UserId = Guid.NewGuid().ToString(), CarId = Guid.NewGuid() },
            };
            using (var context = new MSB_Database(options))
            {
                context.Orders.AddRange(orders);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var orderRepository = new OrderRepository(context, _notificationServiceMock.Object);

                // Act
                var result = await orderRepository.GetAllOrdersAsync();

                // Assert
                Assert.Equal(orders.Count, result.Count());
            }
        }

        // Test to verify that GetOrderByIdAsync returns order when order exists
        [Fact]
        public async Task GetOrderByIdAsync_ReturnsOrder_WhenOrderExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var orderId = Guid.NewGuid();
            var expectedOrder = new OrderModel { OrderId = orderId, OrderNumber = 1, OrderDate = DateTime.Now, TotalCost = 100.0m, UserId = Guid.NewGuid().ToString(), CarId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Orders.Add(expectedOrder);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var orderRepository = new OrderRepository(context, _notificationServiceMock.Object);

                // Act
                var result = await orderRepository.GetOrderByIdAsync(orderId);

                // Assert
                Assert.Equal(expectedOrder.OrderId, result.OrderId);
            }
        }

        // Test to verify that AddOrderAsync creates order in database
        [Fact]
        public async Task AddOrderAsync_CreatesOrderInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var order = new OrderModel { OrderId = Guid.NewGuid(), OrderNumber = 1, OrderDate = DateTime.Now, TotalCost = 100.0m, UserId = Guid.NewGuid().ToString(), CarId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                var orderRepository = new OrderRepository(context, _notificationServiceMock.Object);

                // Act
                var result = await orderRepository.AddOrderAsync(order);

                // Assert
                Assert.Equal(order.OrderId, result.OrderId);
                Assert.Single(context.Orders);
            }
        }

        // Test to verify that UpdateOrderAsync updates order in database
        [Fact]
        public async Task UpdateOrderAsync_UpdatesOrderInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var orderId = Guid.NewGuid();
            var originalOrder = new OrderModel { OrderId = orderId, OrderNumber = 1, OrderDate = DateTime.Now, TotalCost = 100.0m, UserId = Guid.NewGuid().ToString(), CarId = Guid.NewGuid() };
            var updatedOrder = new OrderModel { OrderId = orderId, OrderNumber = 2, OrderDate = DateTime.Now, TotalCost = 200.0m, UserId = Guid.NewGuid().ToString(), CarId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Orders.Add(originalOrder);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var orderRepository = new OrderRepository(context, _notificationServiceMock.Object);

                // Act
                var result = await orderRepository.UpdateOrderAsync(updatedOrder);

                // Assert
                Assert.Equal(updatedOrder.OrderId, result.OrderId);
                Assert.Equal(updatedOrder.OrderNumber, context.Orders.Single().OrderNumber);
            }
        }

        // Test to verify that DeleteOrderAsync deletes order from database
        [Fact]
        public async Task DeleteOrderAsync_DeletesOrderFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var orderId = Guid.NewGuid();
            var order = new OrderModel { OrderId = orderId, OrderNumber = 1, OrderDate = DateTime.Now, TotalCost = 100.0m, UserId = Guid.NewGuid().ToString(), CarId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var orderRepository = new OrderRepository(context, _notificationServiceMock.Object);

                // Act
                await orderRepository.DeleteOrderAsync(orderId);

                // Assert
                Assert.Empty(context.Orders);
            }
        }
    }
}*/