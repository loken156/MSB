using MediatR;

namespace Application.Commands.TimeSlot.DeleteTimeSlot
{
    public class DeleteTimeSlotCommand : IRequest<Unit>
    {
        public Guid TimeSlotId { get; set; }

        public DeleteTimeSlotCommand(Guid timeSlotId)
        {
            TimeSlotId = timeSlotId;
        }
    }
}