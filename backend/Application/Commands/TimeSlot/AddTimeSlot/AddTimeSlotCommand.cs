using Application.Dto.TimeSlot;
using MediatR;

namespace Application.Commands.TimeSlot.AddTimeSlot
{
    public class AddTimeSlotCommand : IRequest<TimeSlotDto>
    {
        public TimeSlotDto NewTimeSlot { get; }

        public AddTimeSlotCommand(TimeSlotDto newTimeSlot)
        {
            NewTimeSlot = newTimeSlot;
        }
    }
}