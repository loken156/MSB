using Application.Dto.TimeSlot;
using Domain.Models.TimeSlot;
using MediatR;

namespace Application.Commands.TimeSlot.UpdateTimeSlot
{
    public class UpdateTimeSlotCommand : IRequest<TimeSlotModel>
    {
        public TimeSlotDto TimeSlot { get; }

        public UpdateTimeSlotCommand(TimeSlotDto timeSlot)
        {
            TimeSlot = timeSlot;
        }
    }
}