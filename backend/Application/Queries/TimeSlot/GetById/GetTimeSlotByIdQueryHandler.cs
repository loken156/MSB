using Domain.Models.TimeSlot;
using Infrastructure.Repositories.TimeSlotRepo;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

// This class retrieves a time slot by its ID. It depends on an ITimeSlotRepository instance injected via its 
// constructor. The Handle method asynchronously processes the query, fetching the time slot with the specified 
// ID from the repository and returning it.

namespace Application.Queries.TimeSlot.GetByID
{
    // Handler for the GetTimeSlotByIdQuery
    public class GetTimeSlotByIdQueryHandler : IRequestHandler<GetTimeSlotByIdQuery, TimeSlotModel>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;

        public GetTimeSlotByIdQueryHandler(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<TimeSlotModel> Handle(GetTimeSlotByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the time slot by ID from the repository
            return await _timeSlotRepository.GetTimeSlotByIdAsync(request.TimeSlotId);
        }
    }
}