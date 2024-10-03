using Application.Commands.TimeSlot.UpdateTimeSlot;
using Domain.Models.TimeSlot;
using Infrastructure.Repositories.TimeSlotRepo;
using MediatR;

namespace Application.Commands.TimeSlot.UpdateTimeSlot
{
    public class UpdateTimeSlotCommandHandler : IRequestHandler<UpdateTimeSlotCommand, TimeSlotModel>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;

        public UpdateTimeSlotCommandHandler(ITimeSlotRepository timeSlotRepository)
        {
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<TimeSlotModel> Handle(UpdateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            // Create an instance of TimeSlotModel using the provided DTO data
            TimeSlotModel timeSlotToUpdate = new TimeSlotModel(
                request.TimeSlot.Id,   // Assuming TimeSlotId is part of the DTO
                request.TimeSlot.Date,
                request.TimeSlot.TimeSlot,
                request.TimeSlot.Occupancy
            );

            // Call the repository to update the time slot
            return await _timeSlotRepository.UpdateTimeSlotAsync(timeSlotToUpdate);
        }
    }
}