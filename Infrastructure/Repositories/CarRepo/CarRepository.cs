using Domain.Models.Car;
using Domain.Models.Employee;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

// This class implements the ICarRepository interface and provides methods for managing CarModel entities in the MSB_Database.
// The class includes methods to:
// - Add a new car asynchronously with AddCar(CarModel carModel)
// - Retrieve all cars asynchronously with GetAllCars()
// - Retrieve a specific car by ID asynchronously with GetCarById(Guid carId)
// - Update an existing car asynchronously with UpdateCar(CarModel car)
// - Delete a car asynchronously with DeleteCar(CarModel car)
// - Assign a driver to a car asynchronously with AssignDriverToCar(CarModel car, EmployeeModel driver)
// - Remove a driver from a car asynchronously with RemoveDriverFromCar(CarModel car)
// The class uses Entity Framework Core for database operations and ensures changes are saved asynchronously to the database.

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
            return await _database.Cars.ToListAsync();
        }

        public async Task<CarModel?> GetCarById(Guid carId)
        {
            return await _database.Cars.FirstOrDefaultAsync(c => c.CarId == carId);
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