﻿using Application.Commands.Employee.UpdateEmployee;
using Application.Dto.Employee;
using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using Moq;

namespace Tests.Application.Employee.CommandHandlers
{
    public class UpdateEmployeeCommandHandlerTests
    {
        // Test to verify that UpdateEmployee calls GetEmployeeByIdAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsGetEmployeeByIdAsyncOnRepository()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var handler = new UpdateEmployeeCommandHandler(mockEmployeeRepository.Object);
            var employeeId = Guid.NewGuid();
            var employeeDto = new EmployeeDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var command = new UpdateEmployeeCommand(employeeDto, employeeId);

            // Set up GetEmployeeByIdAsync to return a non-null value
            mockEmployeeRepository.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EmployeeModel());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockEmployeeRepository.Verify(repo => repo.GetEmployeeByIdAsync(employeeId), Times.Once);
        }

        // Test to verify that UpdateEmployee calls UpdateEmployeeAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsUpdateEmployeeAsyncOnRepository()
        {
            // Arrange
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var handler = new UpdateEmployeeCommandHandler(mockEmployeeRepository.Object);
            var employeeId = Guid.NewGuid();
            var employeeDto = new EmployeeDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var command = new UpdateEmployeeCommand(employeeDto, employeeId);

            // Set up GetEmployeeByIdAsync to return a non-null value
            mockEmployeeRepository.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new EmployeeModel());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockEmployeeRepository.Verify(repo => repo.UpdateEmployeeAsync(employeeId, It.IsAny<EmployeeModel>()), Times.Once);
        }

        // Test to verify that UpdateEmployee returns updated employee model
        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsUpdatedEmployeeModel()
        {
            // Arrange
            var updatedEmployee = new EmployeeModel { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(repo => repo.GetEmployeeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(updatedEmployee);
            var employeeId = Guid.NewGuid();
            var employeeDto = new EmployeeDto { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var handler = new UpdateEmployeeCommandHandler(mockEmployeeRepository.Object);
            var command = new UpdateEmployeeCommand(employeeDto, employeeId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(updatedEmployee, result);
        }
    }
}