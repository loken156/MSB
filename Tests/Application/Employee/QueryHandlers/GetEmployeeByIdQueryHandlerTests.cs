using Application.Queries.Employee.GetById;
using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using Moq;

namespace Tests.Application.Employee.QueryHandlers
{
    public class GetEmployeeByIdQueryHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly GetEmployeeByIdQueryHandler _handler;

        public GetEmployeeByIdQueryHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _handler = new GetEmployeeByIdQueryHandler(_mockEmployeeRepository.Object);
        }

        [Fact]
        public async Task Handle_GivenValidId_ReturnsEmployeeDto()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new EmployeeModel
            {
                Id = employeeId.ToString(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Department = "IT",
                Position = "Developer",
                HireDate = DateTime.Now,
                WarehouseId = Guid.NewGuid()
            };
            _mockEmployeeRepository.Setup(m => m.GetEmployeeByIdAsync(employeeId)).ReturnsAsync(employee);

            // Act
            var result = await _handler.Handle(new GetEmployeeByIdQuery(employeeId), CancellationToken.None);

            // Assert
            Assert.Equal(employee.Id, result.Id.ToString());
            Assert.Equal(employee.FirstName, result.FirstName);
            Assert.Equal(employee.LastName, result.LastName);
            Assert.Equal(employee.Email, result.Email);
            Assert.Equal(employee.Department, result.Department);
            Assert.Equal(employee.Position, result.Position);
            Assert.Equal(employee.HireDate, result.HireDate);
            Assert.Equal(employee.WarehouseId, result.WarehouseId);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ThrowsException()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            _mockEmployeeRepository.Setup(m => m.GetEmployeeByIdAsync(employeeId)).ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetEmployeeByIdQuery(employeeId), CancellationToken.None));
        }
    }
}
