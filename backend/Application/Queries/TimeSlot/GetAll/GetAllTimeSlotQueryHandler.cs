using Domain.Models.TimeSlot;
using Infrastructure.Repositories.TimeSlotRepo;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.TimeSlot.GetAll
{
    // This class implements the request handler interface IRequestHandler for GetAllTimeSlotsQuery,
    // which retrieves all time slots. It takes an ITimeSlotRepository instance via constructor injection.
    // The Handle method asynchronously processes the query, delegating the retrieval of all time slots 
    // to the corresponding method in the time slot repository. The retrieved time slots are then returned.
    public class GetAllTimeSlotsQueryHandler : IRequestHandler<GetAllTimeSlotsQuery, IEnumerable<TimeSlotModel>>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;

        public GetAllTimeSlotsQueryHandler(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<IEnumerable<TimeSlotModel>> Handle(GetAllTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            return await _timeSlotRepository.GetAllTimeSlotsAsync();
        }
    }
}