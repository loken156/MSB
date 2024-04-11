using Domain.Models.Car;
using Domain.Models.Driver;

namespace Infrastructure.Repositories.CarRepo
{
    public interface ICarRepository
    {
        Task<IEnumerable<CarModel>> GetAllCars();
        Task<CarModel?> GetCarById(Guid carId);
        Task UpdateCar(CarModel car);
        Task DeleteCar(CarModel car);
        Task AddCar(CarModel carModel);
        Task AssignDriverToCar(CarModel car, DriverModel driver);
        Task RemoveDriverFromCar(CarModel car);
    }
}