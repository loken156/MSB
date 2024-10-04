using Domain.Models.TimeSlot;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.TimeSlot.GetAll
{
    public class GetAllTimeSlotsQuery : IRequest<IEnumerable<TimeSlotModel>>
    {
    }
}