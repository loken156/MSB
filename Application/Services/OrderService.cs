using Domain.Models.Order;
using Infrastructure.Repositories.OrderRepo;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

// This class represents an order service responsible for creating orders.
// It depends on an order repository for accessing order data.
// The CreateOrderAsync method creates a new order asynchronously.
// It first retrieves the highest order number from the database and increments it to generate a unique order number.
// The order number is constructed by combining the last two digits of the current year, the current month, and the incremented order number part.
// If a duplicate key exception occurs during the database operation, indicating a potential conflict with an existing order number,
// the method retries the operation until it succeeds.
// The IsDuplicateKeyException method checks if a given DbUpdateException is caused by a duplicate key violation,
// specifically checking for error code 1062 which corresponds to MySQL duplicate key exceptions.

namespace Application.Services
{

    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderModel> CreateOrderAsync(OrderModel newOrder)
        {
            while (true)
            {
                try
                {
                    // Get the highest order number
                    int highestOrderNumber = await _orderRepository.GetHighestOrderNumberAsync();

                    // Extract the order number part and increment it
                    int orderNumberPart = highestOrderNumber % 10000;
                    orderNumberPart++;

                    // Get the current year and month
                    var year = DateTime.Now.Year % 100; // Get the last two digits of the current year
                    var month = DateTime.Now.Month;

                    // Combine the year, month, and order number parts
                    newOrder.OrderNumber = year * 1000000 + month * 10000 + orderNumberPart;

                    // Save the new order
                    await _orderRepository.CreateOrderAsync(newOrder);

                    return newOrder;
                }
                catch (DbUpdateException ex) when (IsDuplicateKeyException(ex))
                {
                    // If a duplicate key exception occurred, retry the operation
                    continue;
                }
            }
        }

        private bool IsDuplicateKeyException(DbUpdateException ex)
        {
            var mysqlException = ex.InnerException as MySqlException;
            if (mysqlException != null)
            {
                // Error code 1062 is for duplicate key exceptions
                return mysqlException.Number == 1062;
            }

            return false;
        }


    }

}