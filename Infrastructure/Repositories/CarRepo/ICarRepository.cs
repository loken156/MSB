﻿using Domain.Models.Car;
using System;

namespace Infrastructure.Repositories.CarRepo
{
    public interface ICarRepository
    {
        Task<IEnumerable<CarModel>> GetAllCars();
        Task<CarModel> GetCarById(Guid carId);
        Task UpdateCar(CarModel car);
        Task DeleteCar(CarModel car);
        Task AddCar(CarModel carModel);
    }
}