using Application.Commands.Box.AddBox;
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
        private readonly IMediator _mediator;

        public AddOrderCommandHandler(IOrderRepository orderRepository, IWarehouseRepository warehouseRepository, IMapper mapper, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _mediator = mediator;
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

            // Process each BoxDto in the request
            foreach (var boxDto in request.NewOrder.Boxes)
            {
                // Ensure each box is associated with the newly created order
                boxDto.OrderId = createdOrder.OrderId;

                // Create an AddBoxCommand for each box
                var addBoxCommand = new AddBoxCommand(boxDto);
                await _mediator.Send(addBoxCommand, cancellationToken);
            }

            return createdOrder;
        }
    }

}