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

        public IEnumerable<DriverModel> GetAllDrivers()
        {
            return _database.Drivers.ToList();
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
        public async Task AssignOrderToDriver(DriverModel driver, Guid orderId)
        {
            // Find the specific order
            var order = driver.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                // Handle the case when the order is not found
                // For example, you can throw an exception
                throw new Exception($"Order with ID {orderId} not found.");
            }

            // Perform the operation on the order
            // For example, if you want to update the order status, you can do:
            order.OrderStatus = "Assigned to driver";

            _database.Drivers.Update(driver);
            await _database.SaveChangesAsync();
        }

    }
}

