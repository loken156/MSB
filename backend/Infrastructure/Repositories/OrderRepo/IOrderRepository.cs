using Domain.Models.Order;

namespace Infrastructure.Repositories.OrderRepo
{
    public interface IOrderRepository
    {
        Task<OrderModel> AddOrderAsync(OrderModel order);  // Adds a new order to the database
        Task<IEnumerable<OrderModel>> GetAllOrdersAsync();  // Retrieves all orders
        Task<OrderModel> GetOrderByIdAsync(Guid orderId);  // Retrieves an order by its ID
        Task<OrderModel> UpdateOrderAsync(OrderModel order);  // Updates an existing order
        Task DeleteOrderAsync(Guid orderId);  // Deletes an order by its ID
        Task<int> GetHighestOrderNumberAsync();  // Retrieves the highest order number
    }
}
