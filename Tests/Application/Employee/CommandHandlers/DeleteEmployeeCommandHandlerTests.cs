using Application.Commands.Employee.DeleteEmployee;
using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using Moq;

namespace Tests.Application.Employee.CommandHandlers
{
    public class DeleteEmployeeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CallsGetEmployeeByIdAsyncOnRepository()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var handler = new DeleteEmployeeCommandHandler(mockEmployeeRepository.Object);
            var command = new DeleteEmployeeCommand(Guid.NewGuid());

            // Set up GetEmployeeByIdAsync to return a non-null value
            mockEmployeeRepository.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EmployeeModel());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockEmployeeRepository.Verify(repo => repo.GetEmployeeByIdAsync(command.EmployeeId), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_CallsDeleteEmployeeAsyncOnRepository()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var handler = new DeleteEmployeeCommandHandler(mockEmployeeRepository.Object);
            var command = new DeleteEmployeeCommand(Guid.NewGuid());

            // Set up GetEmployeeByIdAsync to return a non-null value
            mockEmployeeRepository.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EmployeeModel());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockEmployeeRepository.Verify(repo => repo.DeleteEmployeeAsync(command.EmployeeId), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsDeletedEmployeeModel()
        {
            // Arrange
            var deletedEmployee = new EmployeeModel { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(deletedEmployee);
            var handler = new DeleteEmployeeCommandHandler(mockEmployeeRepository.Object);
            var command = new DeleteEmployeeCommand(Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(deletedEmployee, result);
        }

        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((EmployeeModel)null);
            var handler = new DeleteEmployeeCommandHandler(mockEmployeeRepository.Object);
            var command = new DeleteEmployeeCommand(Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }

    }
}