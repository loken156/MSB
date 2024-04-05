using Application.Dto.Driver;
using Application.Dto.Employee;
using Infrastructure.Repositories.DriverRepo;

namespace Application.Queries.Driver.GetById
{
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
                DriverId = driver.DriverId,
                EmployeeId = driver.EmployeeId,
                Employee = new EmployeeDto
                {
                    EmployeeId = driver.Employee.EmployeeId,
                    Email = driver.Employee.Email,
                    Password = driver.Employee.Password,
                    FirstName = driver.Employee.FirstName,
                    LastName = driver.Employee.LastName,
                    Roles = driver.Employee.Roles
                }
            };
        }


    }
}
