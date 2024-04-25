using Domain.Models.Car;
using Domain.Models.Employee;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.CarRepo
{
    public class CarRepository : ICarRepository
    {
        private readonly MSB_Database _database;

        public CarRepository(MSB_Database database)
        {
            _database = database;
        }

        public async Task AddCar(CarModel carModel)
        {
            await _database.Cars.AddAsync(carModel);
            await _database.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarModel>> GetAllCars()
        {
            return await _database.Cars.Include(c => c.Employee).ToListAsync();
        }

        public async Task<CarModel?> GetCarById(Guid carId)
        {
            return await _database.Cars.Include(c => c.Employee).FirstOrDefaultAsync(c => c.CarId == carId);
        }


        public async Task UpdateCar(CarModel car)
        {
            _database.Cars.Update(car);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteCar(CarModel car)
        {
            _database.Cars.Remove(car);
            await _database.SaveChangesAsync();
        }

        public async Task AssignDriverToCar(CarModel car, EmployeeModel driver)
        {
            car.DriverId = Guid.Parse(driver.Id);
            _database.Cars.Update(car);
            await _database.SaveChangesAsync();
        }

        public async Task RemoveDriverFromCar(CarModel car)
        {
            car.DriverId = null;
            _database.Cars.Update(car);
            await _database.SaveChangesAsync();
        }

    }

}