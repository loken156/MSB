using Application.Commands.TimeSlot.AddTimeSlot;
using Application.Dto.TimeSlot;
using AutoMapper;
using Domain.Models.TimeSlot;
using Infrastructure.Repositories.TimeSlotRepo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.TimeSlot.AddTimeSlot
{
    // This class handles the command to add a new time slot.
    // It uses MediatR for processing the command, 
    // AutoMapper for mapping between the DTO and the domain model, 
    // and ILogger for logging errors. 
    // The handler interacts with the time slot repository to save the new time slot 
    // and returns the created time slot as a DTO.
    
    public class AddTimeSlotCommandHandler : IRequestHandler<AddTimeSlotCommand, TimeSlotDto>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly ILogger<AddTimeSlotCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AddTimeSlotCommandHandler(ITimeSlotRepository timeSlotRepository, ILogger<AddTimeSlotCommandHandler> logger, IMapper mapper)
        {
            _timeSlotRepository = timeSlotRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TimeSlotDto> Handle(AddTimeSlotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map the DTO to the domain model
                var timeSlotModel = _mapper.Map<TimeSlotModel>(request.NewTimeSlot);
                
                // Add the time slot to the repository
                await _timeSlotRepository.AddTimeSlotAsync(timeSlotModel);
                
                // Map the domain model back to the DTO for the response
                var timeSlotDto = _mapper.Map<TimeSlotDto>(timeSlotModel);
                
                return timeSlotDto;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding time slot");
                throw;
            }
        }
    }
}
