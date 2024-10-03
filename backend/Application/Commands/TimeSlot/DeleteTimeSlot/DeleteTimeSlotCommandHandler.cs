using Application.Commands.TimeSlot.DeleteTimeSlot;
using Infrastructure.Repositories.TimeSlotRepo;
using MediatR;

// This class handles the command to delete a time slot. 
// It utilizes MediatR for processing the command and 
// interacts with the time slot repository to delete the specified time slot from the data source.

namespace Application.Commands.TimeSlot.DeleteTimeSlot
{
    public class DeleteTimeSlotCommandHandler : IRequestHandler<DeleteTimeSlotCommand, Unit>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;

        public DeleteTimeSlotCommandHandler(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<Unit> Handle(DeleteTimeSlotCommand request, CancellationToken cancellationToken)
        {
            await _timeSlotRepository.DeleteTimeSlotAsync(request.TimeSlotId);
            return Unit.Value;
        }
    }
}