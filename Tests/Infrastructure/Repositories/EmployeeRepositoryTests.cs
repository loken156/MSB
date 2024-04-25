using Domain.Models.Employee;
using Infrastructure.Database;
using Infrastructure.Repositories.EmployeeRepo;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Repositories
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        public async Task GetEmployeesAsync_ReturnsAllEmployees()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var employees = new List<EmployeeModel>
            {
                new EmployeeModel { Id = Guid.NewGuid().ToString(), UserName = "TestEmployee1", EmployeeEmail = "test1@example.com", FirstName = "Test", LastName = "Employee", Role = "Employee", Department = "Sales", Position = "Manager", HireDate = DateTime.Now, WarehouseId = Guid.NewGuid() },
                new EmployeeModel { Id = Guid.NewGuid().ToString(), UserName = "TestEmployee2", EmployeeEmail = "test2@example.com", FirstName = "Test", LastName = "Employee", Role = "Employee", Department = "Marketing", Position = "Executive", HireDate = DateTime.Now, WarehouseId = Guid.NewGuid() },
            };
            using (var context = new MSB_Database(options))
            {
                context.Employees.AddRange(employees);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var employeeRepository = new EmployeeRepository(context);

                // Act
                var result = await employeeRepository.GetEmployeesAsync();

                // Assert
                Assert.Equal(employees.Count, result.Count());
            }
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsEmployee_WhenEmployeeExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var employeeId = Guid.NewGuid().ToString();
            var expectedEmployee = new EmployeeModel { Id = employeeId, UserName = "TestEmployee", EmployeeEmail = "test@example.com", FirstName = "Test", LastName = "Employee", Role = "Employee", Department = "Sales", Position = "Manager", HireDate = DateTime.Now, WarehouseId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Employees.Add(expectedEmployee);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var employeeRepository = new EmployeeRepository(context);

                // Act
                var result = await employeeRepository.GetEmployeeByIdAsync(Guid.Parse(employeeId));

                // Assert
                Assert.Equal(expectedEmployee.Id, result.Id);
            }
        }

        [Fact]
        public async Task CreateEmployeeAsync_CreatesEmployeeInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var employee = new EmployeeModel { Id = Guid.NewGuid().ToString(), UserName = "TestEmployee", EmployeeEmail = "test@example.com", FirstName = "Test", LastName = "Employee", Role = "Employee", Department = "Sales", Position = "Manager", HireDate = DateTime.Now, WarehouseId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                var employeeRepository = new EmployeeRepository(context);

                // Act
                var result = await employeeRepository.CreateEmployeeAsync(employee);

                // Assert
                Assert.Equal(employee.Id, result.Id);
                Assert.Single(context.Employees);
            }
        }

        [Fact]
        public async Task UpdateEmployeeAsync_UpdatesEmployeeInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var employeeId = Guid.NewGuid().ToString();
            var originalEmployee = new EmployeeModel { Id = employeeId, UserName = "OriginalEmployee", EmployeeEmail = "original@example.com", FirstName = "Test", LastName = "Employee", Role = "Employee", Department = "Sales", Position = "Manager", HireDate = DateTime.Now, WarehouseId = Guid.NewGuid() };
            var updatedEmployee = new EmployeeModel { Id = employeeId, UserName = "UpdatedEmployee", EmployeeEmail = "updated@example.com", FirstName = "Test", LastName = "Employee", Role = "Employee", Department = "Marketing", Position = "Executive", HireDate = DateTime.Now, WarehouseId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Employees.Add(originalEmployee);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var employeeRepository = new EmployeeRepository(context);

                // Act
                var result = await employeeRepository.UpdateEmployeeAsync(Guid.Parse(employeeId), updatedEmployee);

                // Assert
                Assert.Equal(updatedEmployee.Id, result.Id);
                Assert.Equal(updatedEmployee.UserName, context.Employees.Single().UserName);
            }
        }

        [Fact]
        public async Task DeleteEmployeeAsync_DeletesEmployeeFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var employeeId = Guid.NewGuid().ToString();
            var employee = new EmployeeModel { Id = employeeId, UserName = "TestEmployee", EmployeeEmail = "test@example.com", FirstName = "Test", LastName = "Employee", Role = "Employee", Department = "Sales", Position = "Manager", HireDate = DateTime.Now, WarehouseId = Guid.NewGuid() };
            using (var context = new MSB_Database(options))
            {
                context.Employees.Add(employee);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var employeeRepository = new EmployeeRepository(context);

                // Act
                var result = await employeeRepository.DeleteEmployeeAsync(Guid.Parse(employeeId));

                // Assert
                Assert.True(result);
                Assert.Empty(context.Employees);
            }
        }
    }
}
