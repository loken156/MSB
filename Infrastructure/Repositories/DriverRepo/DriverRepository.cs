using Domain.Models;
using Domain.Models.Driver;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.DriverRepo
{
    public class DriverRepository : IDriverRepository
    {
        private readonly MSB_Database _database;

        public DriverRepository(MSB_Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<DriverModel>> GetAllDrivers()
        {
            return await _database.Drivers.ToListAsync();
        }


        public async Task<DriverModel> GetDriverByIdAsync(Guid id)
        {
            return await _database.Drivers.FirstOrDefaultAsync(d => d.Id == id.ToString());
        }

        public void AddDriver(DriverModel driver)
        {
            _database.Drivers.Add(driver);
            _database.SaveChanges();
        }

        public async Task UpdateDriver(DriverModel driver)
        {
            _database.Drivers.Update(driver);
            await _database.SaveChangesAsync();
        }

        public void DeleteDriver(Guid id)
        {
            var driver = _database.Drivers.Find(id);
            if (driver == null)
                return;

            _database.Drivers.Remove(driver);
            _database.SaveChanges();
        }
        public async Task AssignOrderToDriver(DriverModel driver, Guid orderId, TimeSlot pickupTimeSlot)
        {
            // Find the specific order
            var order = driver.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                // Handle the case when the order is not found
                throw new Exception($"Order with ID {orderId} not found.");
            }

            // Perform the operation on the order
            order.OrderStatus = "Assigned to driver";

            // Find the time slot that includes the pickup time and remove it
            var timeSlot = driver.Availability.FirstOrDefault(slot => slot.StartTime <= pickupTimeSlot.StartTime && slot.EndTime >= pickupTimeSlot.EndTime);
            if (timeSlot != null)
            {
                driver.Availability.Remove(timeSlot);
            }
            else
            {
                // Handle the case when no matching time slot is found
                throw new Exception($"No available time slot found for the pickup time.");
            }

            _database.Drivers.Update(driver);
            await _database.SaveChangesAsync();
        }


    }
}

