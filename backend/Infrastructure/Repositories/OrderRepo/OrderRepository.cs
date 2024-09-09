﻿using Domain.Models.Notification;
using Domain.Models.Order;
using Infrastructure.Database;
using Infrastructure.Services.Notification;
using Microsoft.EntityFrameworkCore;

// This class implements the IOrderRepository interface and provides methods for managing OrderModel entities in the MSB_Database.
// The class includes methods to:
// - Retrieve all orders asynchronously with GetAllOrdersAsync()
// - Retrieve a specific order by ID asynchronously with GetOrderByIdAsync(Guid orderId)
// - Create a new order asynchronously with AddOrderAsync(OrderModel order) and CreateOrderAsync(OrderModel newOrder)
// - Update an existing order asynchronously with UpdateOrderAsync(OrderModel order)
// - Delete an order asynchronously with DeleteOrderAsync(Guid orderId)
// - Retrieve the highest order number asynchronously with GetHighestOrderNumberAsync()
// The class also integrates with the INotificationService to send notifications when an order's status is updated to "Delivered".
// The UpdateOrderAsync method ensures notifications are sent to the user when their order is delivered to the warehouse.
// Entity Framework Core is used for database operations, ensuring asynchronous save changes to the database.

namespace Infrastructure.Repositories.OrderRepo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MSB_Database _database;
        private readonly INotificationService _notificationService;
        public OrderRepository(MSB_Database mSB_Database, INotificationService notificationService)
        {
            _database = mSB_Database;
            _notificationService = notificationService;
        }

        public async Task<OrderModel> AddOrderAsync(OrderModel order)
        {
            await _database.Orders.AddAsync(order);
            await _database.SaveChangesAsync();

            return order;
        }

        public async Task<IEnumerable<OrderModel>> GetAllOrdersAsync()
        {
            return await _database.Orders.ToListAsync();
        }

        public async Task<OrderModel> GetOrderByIdAsync(Guid orderId)
        {
            return await _database.Orders.FindAsync(orderId);
        }

        public async Task<OrderModel> UpdateOrderAsync(OrderModel order)
        {
            _database.Orders.Update(order);
            await _database.SaveChangesAsync();

            if (order.OrderStatus == "Delivered")
            {
                // Send notification to user that order has been delivered to warehouse
                var notification = new NotificationModel
                {
                    UserId = order.UserId,
                    Message = "Your order has been delivered to the warehouse."
                };
                await _notificationService.SendNotification(notification);
            }

            return order;
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var order = await _database.Orders.FindAsync(orderId);
            if (order != null)
            {
                _database.Orders.Remove(order);
                await _database.SaveChangesAsync();
            }
        }

        public async Task<int> GetHighestOrderNumberAsync()
        {
            if (await _database.Orders.AnyAsync())
            {
                return await _database.Orders.MaxAsync(o => o.OrderNumber);
            }
            else
            {
                return 0;
            }
        }

        public async Task CreateOrderAsync(OrderModel newOrder)
        {
            await _database.Orders.AddAsync(newOrder);
            await _database.SaveChangesAsync();
        }
    }
}