using Infrastructure.Database; 
using Domain.Models.TimeSlot;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TimeSlotService
    {
        private readonly MSB_Database _context;

        public TimeSlotService(MSB_Database context)
        {
            _context = context;
        }

        public async Task PopulateTimeSlotsAsync()
        {
            var currentDate = DateTime.Today;
            var daysToPopulate = 7;

            for (int i = 0; i < daysToPopulate; i++)
            {
                var dateToCheck = currentDate.AddDays(i);
                // Check if TimeSlots for this date already exist
                var existingTimeSlots = await _context.TimeSlots
                    .Where(ts => ts.Date.Date == dateToCheck)
                    .ToListAsync();

                if (!existingTimeSlots.Any())
                {
                    // If no existing TimeSlots, add them
                    var timeSlotsToAdd = new[]
                    {
                        new TimeSlotModel { Date = dateToCheck, TimeSlot = "11-14", Occupancy = 3 },
                        new TimeSlotModel { Date = dateToCheck, TimeSlot = "14-17", Occupancy = 3 },
                        new TimeSlotModel { Date = dateToCheck, TimeSlot = "17-20", Occupancy = 3 }
                    };

                    await _context.TimeSlots.AddRangeAsync(timeSlotsToAdd);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}