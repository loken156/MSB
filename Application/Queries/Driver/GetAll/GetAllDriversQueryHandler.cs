﻿using Application.Dto.Car;
using Application.Dto.Driver;
using Application.Dto.Employee;
using Infrastructure.Repositories.DriverRepo;

namespace Application.Queries.Driver.GetAll
{
    public class GetAllDriversQueryHandler
    {
        private readonly IDriverRepository _driverRepository;

        public GetAllDriversQueryHandler(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<IEnumerable<DriverDetailDto>> Handle(GetAllDriversQuery query)
        {
            var drivers = await _driverRepository.GetAllDrivers();

            var driverDtos = drivers.Select(driver => new DriverDetailDto
            {
                DriverId = Guid.Parse(driver.Id),
                LicenseNumber = driver.LicenseNumber,
                Car = new CarDto
                {
                    CarId = driver.CurrentCarId,
                    // Map other properties of CarDto here
                },
                Employee = new EmployeeDto
                {

                    Email = driver.EmployeeEmail,
                    FirstName = driver.FirstName,
                    LastName = driver.LastName,

                },

            });

            return driverDtos;
        }
    }
}