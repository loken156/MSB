using Domain.Models.TimeSlot;
using MediatR;

namespace Application.Queries.TimeSlot.GetByID
{
    public class GetTimeSlotByIdQuery : IRequest<TimeSlotModel>
    {
        public Guid TimeSlotId { get; } // Assuming TimeSlotId is of type Guid

        public GetTimeSlotByIdQuery(Guid timeSlotId)
        {
            TimeSlotId = timeSlotId;
        }
    }
}