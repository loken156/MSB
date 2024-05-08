using AutoMapper;
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
        private readonly IMapper _mapper;
        public AddOrderCommandHandler(IOrderRepository orderRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }
        public async Task<OrderModel> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.WarehouseId);
            if (warehouse == null)
            {
                throw new Exception("Warehouse not found");
            }

            // Use AutoMapper to map OrderDto to OrderModel
            var orderToCreate = _mapper.Map<OrderModel>(request.NewOrder);
            orderToCreate.OrderId = Guid.NewGuid(); // Ensure OrderId is set to a new GUID

            var createdOrder = await _orderRepository.AddOrderAsync(orderToCreate);

            return createdOrder;
        }
    }
}