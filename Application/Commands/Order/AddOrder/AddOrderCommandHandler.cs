using Domain.Models.Order;
using Infrastructure.Repositories.OrderRepo;
using Infrastructure.Repositories.WarehouseRepo;
using MediatR;

namespace Application.Commands.Order.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderModel>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        public AddOrderCommandHandler(IOrderRepository orderRepository, IWarehouseRepository warehouseRepository)
        {
            _orderRepository = orderRepository;
            _warehouseRepository = warehouseRepository;
        }
        public async Task<OrderModel> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {

            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.WarehouseId);
            if (warehouse == null)
            {
                throw new Exception("Warehouse not found");
            }

            request.NewOrder.WarehouseId = request.WarehouseId;

            OrderModel orderToCreate = new()
            {
                OrderId = Guid.NewGuid(),
                OrderDate = request.NewOrder.OrderDate,
                TotalCost = request.NewOrder.TotalCost,
                OrderStatus = request.NewOrder.OrderStatus ?? string.Empty,
                UserId = request.NewOrder.UserId,
                // User = request.NewOrder.User, // You'll need to map from UserDto to UserModel
                // CarId = request.NewOrder.CarId, // Uncomment if you want to set CarId
                // RepairId = request.NewOrder.RepairId, // Uncomment if you want to set RepairId
                WarehouseId = request.NewOrder.WarehouseId,
                // AdressId = request.NewOrder.AdressId,
                // Address = request.NewOrder.Address, // You'll need to map from AddressDto to AddressModel
                RepairNotes = request.NewOrder.RepairNotes ?? "No notes"
            };
            var createdOrder = await _orderRepository.AddOrderAsync(orderToCreate);

            return createdOrder;
        }
    }
}