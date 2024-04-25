using Application.Queries.Employee.GetAll;
using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Application.Employee.QueryHandlers
{
    public class GetAllEmployeesQueryHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly GetAllEmployeesQueryHandler _handler;
        private readonly Mock<ILogger<GetAllEmployeesQueryHandler>> _logger;

        public GetAllEmployeesQueryHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _logger = new Mock<ILogger<GetAllEmployeesQueryHandler>>();
            _handler = new GetAllEmployeesQueryHandler(_mockEmployeeRepository.Object, _logger.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<EmployeeModel>
            {
                new EmployeeModel { Id = Guid.NewGuid().ToString(), FirstName = "John", LastName = "Doe", EmployeeEmail = "john.doe@example.com" },
                new EmployeeModel { Id = Guid.NewGuid().ToString(), FirstName = "Jane", LastName = "Doe", EmployeeEmail = "jane.doe@example.com" }
            };
            _mockEmployeeRepository.Setup(m => m.GetEmployeesAsync()).ReturnsAsync(employees);

            // Act
            var result = await _handler.Handle(new GetAllEmployeesQuery(), default);

            // Assert
            Assert.Equal(employees.Count, result.Count);
        }

        [Fact]
        public async Task Handle_MapsEmployeeModelCorrectly()
        {
            // Arrange
            var employee = new EmployeeModel { Id = Guid.NewGuid().ToString(), FirstName = "John", LastName = "Doe", EmployeeEmail = "john.doe@example.com" };
            var employees = new List<EmployeeModel> { employee };
            _mockEmployeeRepository.Setup(m => m.GetEmployeesAsync()).ReturnsAsync(employees);

            // Act
            var result = await _handler.Handle(new GetAllEmployeesQuery(), default);
            var employeeDto = result.First();

            // Assert
            Assert.Equal(employee.Id, employeeDto.Id);
            Assert.Equal(employee.FirstName, employeeDto.FirstName);
            Assert.Equal(employee.LastName, employeeDto.LastName);
            Assert.Equal(employee.EmployeeEmail, employeeDto.EmployeeEmail);
        }
    }
}