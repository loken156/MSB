using Application.Dto.Car;
using Application.Dto.Driver;
using Application.Dto.Employee;
using Application.Queries.Driver.GetById;
using Infrastructure.Repositories.DriverRepo;

public class GetDriverByIdQueryHandler
{
    private readonly IDriverRepository _driverRepository;

    public GetDriverByIdQueryHandler(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<DriverDetailDto> Handle(GetDriverByIdQuery query)
    {
        var driver = await _driverRepository.GetDriverByIdAsync(query.DriverId)
                         ?? throw new Exception($"No driver found with id {query.DriverId}");

        return new DriverDetailDto
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
                EmployeeId = Guid.Parse(driver.Id),
                Email = driver.Email,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                // Map other properties of EmployeeDto here
            },
            // Map other properties of DriverDetailDto here
        };
    }
}
