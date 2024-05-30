using Domain.Models.Order;
using Infrastructure.Repositories.OrderRepo;
using MediatR;

// This class resides in the Application layer and handles the command to update an order. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the order repository in the Infrastructure layer to retrieve 
// the existing order entity based on the provided OrderId. If the order is not found, it 
// throws an exception. Otherwise, it updates the properties of the order with the values 
// provided in the UpdateOrderCommand and then saves the updated order to the database.

namespace Application.Commands.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderModel>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderModel> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the existing order from the database
            var orderToUpdate = await _orderRepository.GetOrderByIdAsync(request.Order.OrderId);

            if (orderToUpdate == null)
            {
                // Handle the case where the order doesn't exist
                // This might involve throwing an exception, returning null, or something else
                throw new Exception($"Order with ID {request.Order.OrderId} not found");
            }

            // Update the properties of the order
            orderToUpdate.OrderDate = request.Order.OrderDate;
            orderToUpdate.TotalCost = request.Order.TotalCost;
            orderToUpdate.OrderStatus = request.Order.OrderStatus;
            orderToUpdate.UserId = request.Order.UserId;
            orderToUpdate.RepairNotes = request.Order.RepairNotes;

            // Save the updated order to the database
            return await _orderRepository.UpdateOrderAsync(orderToUpdate);
        }

    }
}