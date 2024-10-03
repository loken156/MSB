using Domain.Models.TimeSlot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TimeSlotRepo
{
    public interface ITimeSlotRepository
    {
        Task<TimeSlotModel> AddTimeSlotAsync(TimeSlotModel timeSlot);
        Task<IEnumerable<TimeSlotModel>> GetAllTimeSlotsAsync();
        Task<TimeSlotModel> UpdateTimeSlotAsync(TimeSlotModel timeSlot);
        Task DeleteTimeSlotAsync(Guid timeSlotId);
        Task<TimeSlotModel> GetTimeSlotByIdAsync(Guid timeSlotId);
    }
}