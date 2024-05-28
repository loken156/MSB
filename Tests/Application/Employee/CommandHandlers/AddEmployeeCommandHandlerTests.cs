using Application.Commands.Employee.AddEmployee;
using Application.Dto.Employee;
using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using Moq;

namespace Tests.Application.Employee.CommandHandlers
{
    public class AddEmployeeCommandHandlerTests
    {
        // Test to verify that AddEmployee calls CreateEmployeeAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsCreateEmployeeAsyncOnRepository()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var handler = new AddEmployeeCommandHandler(mockEmployeeRepository.Object);
            var command = new AddEmployeeCommand(new EmployeeDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" });

            // Set up CreateEmployeeAsync to return a non-null value
            mockEmployeeRepository.Setup(repo => repo.CreateEmployeeAsync(It.IsAny<EmployeeModel>()))
                .ReturnsAsync(new EmployeeModel());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockEmployeeRepository.Verify(repo => repo.CreateEmployeeAsync(It.Is<EmployeeModel>(model => model.Email == command.NewEmployee.Email)), Times.Once);
        }

        // Test to verify that AddEmployee returns created employee model
        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsCreatedEmployeeModel()
        {
            // Arrange
            var createdEmployee = new EmployeeModel { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.CreateEmployeeAsync(It.IsAny<EmployeeModel>()))
                .ReturnsAsync(createdEmployee);
            var handler = new AddEmployeeCommandHandler(mockEmployeeRepository.Object);
            var command = new AddEmployeeCommand(new EmployeeDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(createdEmployee.FirstName, result.FirstName);
            Assert.Equal(createdEmployee.LastName, result.LastName);
            Assert.Equal(createdEmployee.Email, result.Email);
        }
    }
}