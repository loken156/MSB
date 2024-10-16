﻿using Domain.Models.Car;
using Infrastructure.Database;
using Infrastructure.Repositories.CarRepo;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Repositories
{
    public class CarRepositoryTests
    {
        // Test to verify that AddCar adds car to database
        [Fact]
        public async Task AddCar_AddsCarToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var car = new CarModel { CarId = Guid.NewGuid(), Volume = 100, Type = "TestCar", Availability = "Available", Employee = "TestEmployee" };
            using (var context = new MSB_Database(options))
            {
                var carRepository = new CarRepository(context);

                // Act
                await carRepository.AddCar(car);

                // Assert
                Assert.Single(context.Cars);
            }
        }

        // Test to verify that GetAllCars returns all cars
        [Fact]
        public async Task GetAllCars_ReturnsAllCars()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var cars = new List<CarModel>
            {
                new CarModel { CarId = Guid.NewGuid(), Volume = 100, Type = "TestCar1", Availability = "Available", Employee = "TestEmployee1" },
                new CarModel { CarId = Guid.NewGuid(), Volume = 200, Type = "TestCar2", Availability = "Unavailable", Employee = "TestEmployee2" },
            };
            using (var context = new MSB_Database(options))
            {
                context.Cars.AddRange(cars);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var carRepository = new CarRepository(context);

                // Act
                var result = await carRepository.GetAllCars();

                // Assert
                Assert.Equal(cars.Count, result.Count());
            }
        }

        // Test to verify that GetCarById returns car when car exists
        [Fact]
        public async Task GetCarById_ReturnsCar_WhenCarExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var carId = Guid.NewGuid();
            var expectedCar = new CarModel { CarId = carId, Volume = 100, Type = "TestCar", Availability = "Available", Employee = "TestEmployee" };
            using (var context = new MSB_Database(options))
            {
                context.Cars.Add(expectedCar);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var carRepository = new CarRepository(context);

                // Act
                var result = await carRepository.GetCarById(carId);

                // Assert
                Assert.Equal(expectedCar.CarId, result?.CarId);
            }
        }

        // Test to verify that UpdateCar updates car in database
        [Fact]
        public async Task UpdateCar_UpdatesCarInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var carId = Guid.NewGuid();
            var originalCar = new CarModel { CarId = carId, Volume = 100, Type = "OriginalCar", Availability = "Available", Employee = "TestEmployee" };
            var updatedCar = new CarModel { CarId = carId, Volume = 200, Type = "UpdatedCar", Availability = "Unavailable", Employee = "TestEmployee" };
            using (var context = new MSB_Database(options))
            {
                context.Cars.Add(originalCar);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var carRepository = new CarRepository(context);

                // Act
                await carRepository.UpdateCar(updatedCar);

                // Assert
                Assert.Equal(updatedCar.CarId, context.Cars.Single().CarId);
                Assert.Equal(updatedCar.Type, context.Cars.Single().Type);
            }
        }

        // Test to verify that DeleteCar deletes car from database
        [Fact]
        public async Task DeleteCar_DeletesCarFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var carId = Guid.NewGuid();
            var car = new CarModel { CarId = carId, Volume = 100, Type = "TestCar", Availability = "Available", Employee = "TestEmployee" };
            using (var context = new MSB_Database(options))
            {
                context.Cars.Add(car);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var carRepository = new CarRepository(context);

                // Act
                await carRepository.DeleteCar(car);

                // Assert
                Assert.Empty(context.Cars);
            }
        }
    }
}