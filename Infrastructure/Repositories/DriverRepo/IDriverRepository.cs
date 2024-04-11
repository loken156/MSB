using Domain.Models;
using Domain.Models.Driver;

namespace Infrastructure.Repositories.DriverRepo
{
    public interface IDriverRepository
    {
        Task<IEnumerable<DriverModel>> GetAllDrivers();
        void AddDriver(DriverModel driver);
        void DeleteDriver(Guid id);
        Task<DriverModel> GetDriverByIdAsync(Guid driverId);
        Task UpdateDriver(DriverModel driver);
        Task AssignOrderToDriver(DriverModel driver, Guid orderId, TimeSlot pickupTimeSlot);
    }
}

