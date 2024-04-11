using Domain.Models.Driver;
using System;

namespace Infrastructure.Repositories.DriverRepo
{
    public interface IDriverRepository
    {
        IEnumerable<DriverModel> GetAllDrivers();
        void AddDriver(DriverModel driver);
        void DeleteDriver(Guid id);
        Task<DriverModel> GetDriverByIdAsync(Guid driverId);
        Task UpdateDriver(DriverModel driver);
    }
}