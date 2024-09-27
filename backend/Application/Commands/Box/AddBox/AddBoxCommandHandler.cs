using Application.Dto.Box;
using AutoMapper;
using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using Infrastructure.Repositories.BoxTypeRepo;
using Infrastructure.Repositories.OrderRepo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Box.AddBox
{
    public class AddBoxCommandHandler : IRequestHandler<AddBoxCommand, BoxDto>
    {
        private readonly IBoxRepository _boxRepository;
        private readonly IBoxTypeRepository _boxTypeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<AddBoxCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AddBoxCommandHandler(
            IBoxRepository boxRepository, 
            IBoxTypeRepository boxTypeRepository, 
            IOrderRepository orderRepository, 
            ILogger<AddBoxCommandHandler> logger, 
            IMapper mapper)
        {
            _boxRepository = boxRepository;
            _boxTypeRepository = boxTypeRepository;
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BoxDto> Handle(AddBoxCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the BoxType from the database using BoxTypeId
                var boxType = await _boxTypeRepository.GetBoxTypeByIdAsync(request.NewBox.BoxTypeId);
                if (boxType == null)
                {
                    throw new Exception($"BoxType with ID {request.NewBox.BoxTypeId} not found.");
                }

                // Map the incoming BoxDto to the domain model (BoxModel)
                var boxModel = _mapper.Map<BoxModel>(request.NewBox);

                // Assign the BoxType details from the database
                boxModel.BoxType = boxType;

                // Optionally link the box to an order if OrderId is provided
                if (request.NewBox.OrderId.HasValue)
                {
                    var order = await _orderRepository.GetOrderByIdAsync(request.NewBox.OrderId.Value);
                    if (order == null)
                    {
                        throw new Exception($"Order with ID {request.NewBox.OrderId} not found.");
                    }

                    // Add the box to the order and update the order
                    order.Boxes.Add(boxModel);
                    await _orderRepository.UpdateOrderAsync(order);
                }

                // Save the box to the database
                await _boxRepository.AddBoxAsync(boxModel);

                // Map the result back to BoxDto and return
                var boxDto = _mapper.Map<BoxDto>(boxModel);
                return boxDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding box.");
                throw;
            }
        }
    }
}
