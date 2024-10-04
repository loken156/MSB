using Domain.Models.TimeSlot;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

// This class implements the ITimeSlotRepository interface and provides methods for interacting with the TimeSlotModel entities in the MSB_Database.
// The class includes methods to:
// - Add a new time slot asynchronously with AddTimeSlotAsync(TimeSlotModel timeSlot)
// - Delete a time slot by ID asynchronously with DeleteTimeSlotAsync(Guid timeSlotId)
// - Retrieve all time slots asynchronously with GetAllTimeSlotsAsync()
// - Retrieve a specific time slot by ID asynchronously with GetTimeSlotByIdAsync(Guid timeSlotId)
// - Update an existing time slot asynchronously with UpdateTimeSlotAsync(TimeSlotModel timeSlot)
// The class leverages Entity Framework Core for database operations and ensures changes are saved asynchronously to the database.

namespace Infrastructure.Repositories.TimeSlotRepo
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly MSB_Database _context;

        public TimeSlotRepository(MSB_Database context)
        {
            _context = context;
        }

        public async Task<TimeSlotModel> AddTimeSlotAsync(TimeSlotModel timeSlot)
        {
            // Assuming your DB context has a DbSet<TimeSlotModel>
            await _context.TimeSlots.AddAsync(timeSlot);
            await _context.SaveChangesAsync();
            return timeSlot;
        }

        public async Task<IEnumerable<TimeSlotModel>> GetAllTimeSlotsAsync()
        {
            return await _context.TimeSlots.ToListAsync();
        }

        public async Task<TimeSlotModel> UpdateTimeSlotAsync(TimeSlotModel timeSlot)
        {
            _context.TimeSlots.Update(timeSlot);
            await _context.SaveChangesAsync();
            return timeSlot;
        }

        public async Task DeleteTimeSlotAsync(Guid timeSlotId)
        {
            var timeSlot = await _context.TimeSlots.FindAsync(timeSlotId);
            if (timeSlot != null)
            {
                _context.TimeSlots.Remove(timeSlot);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TimeSlotModel> GetTimeSlotByIdAsync(Guid timeSlotId)
        {
            return await _context.TimeSlots.FindAsync(timeSlotId);
        }
    }
}
