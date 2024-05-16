using AutoMapper;
using Domain.Interfaces;
using Domain.Models.Label;
using Domain.Models.Order;
using Infrastructure.Repositories.OrderRepo;
using Infrastructure.Repositories.WarehouseRepo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Order.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderModel>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<AddOrderCommandHandler> _logger;
        private readonly ILabelPrinterService _labelPrinterService;

        public AddOrderCommandHandler(IOrderRepository orderRepository, IWarehouseRepository warehouseRepository, IMapper mapper, IMediator mediator, ILogger<AddOrderCommandHandler> logger, ILabelPrinterService labelPrinterService)
        {
            _orderRepository = orderRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _labelPrinterService = labelPrinterService;
        }

        public async Task<OrderModel> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.WarehouseId);

            if (warehouse == null)
            {
                throw new Exception("Warehouse not found");
            }

            // Use AutoMapper to map AddOrderDto to OrderModel
            var orderToCreate = _mapper.Map<OrderModel>(request.NewOrder);
            orderToCreate.OrderId = Guid.NewGuid(); // Ensure OrderId is set to a new GUID

            var createdOrder = await _orderRepository.AddOrderAsync(orderToCreate);

            // Add label
            var label = new LabelModel
            {
                OrderNumber = createdOrder.OrderNumber.ToString(),
                OrderDate = createdOrder.OrderDate,
            };
            try
            {
                await _labelPrinterService.PrintLabelAsync(label);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to print label for order {createdOrder.OrderId}");
                throw;
            }

            return createdOrder;
        }

    }

}