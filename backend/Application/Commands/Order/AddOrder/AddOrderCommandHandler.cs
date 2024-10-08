using Application.Dto.Order;
using AutoMapper;
using Domain.Models.Box;
using Domain.Models.Order;
using Infrastructure.Repositories.BoxRepo;
using Infrastructure.Repositories.OrderRepo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Order.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBoxRepository _boxRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddOrderCommandHandler> _logger;

        public AddOrderCommandHandler(IOrderRepository orderRepository, IBoxRepository boxRepository, IMapper mapper, ILogger<AddOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _boxRepository = boxRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderDto> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map the AddOrderDto (from request) to the OrderModel (domain model)
                var orderModel = _mapper.Map<OrderModel>(request.NewOrder);

                // Check if there are boxes included in the request
                if (request.NewOrder.Boxes != null && request.NewOrder.Boxes.Count > 0)
                {
                    // Map each BoxDto to a BoxModel and associate it with the order
                    var boxModels = _mapper.Map<List<BoxModel>>(request.NewOrder.Boxes);
                    
                    foreach (var box in boxModels)
                    {
                        box.OrderId = orderModel.OrderId; // Link the box to the order
                        await _boxRepository.AddBoxAsync(box); // Persist each box to the repository
                    }

                    // Associate the boxes with the order
                    orderModel.Boxes = boxModels;
                }

                // Add the order to the repository
                await _orderRepository.AddOrderAsync(orderModel);

                // Map the result back to OrderDto (return type)
                var orderDto = _mapper.Map<OrderDto>(orderModel);
                return orderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding order.");
                throw;
            }
        }
    }
}
