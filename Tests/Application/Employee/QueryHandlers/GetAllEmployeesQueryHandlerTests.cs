using Application.Queries.Employee.GetAll;
using Domain.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Application.Employee.QueryHandlers
{
    public class GetAllEmployeesQueryHandlerTests
    {
        private readonly Mock<IMSBDatabase> _mockDatabase;
        private readonly GetAllEmployeesQueryHandler _handler;

        public GetAllEmployeesQueryHandlerTests()
        {
            _mockDatabase = new Mock<IMSBDatabase>();
            _handler = new GetAllEmployeesQueryHandler(_mockDatabase.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<EmployeeModel>
            {
                new EmployeeModel { Id = Guid.NewGuid().ToString(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new EmployeeModel { Id = Guid.NewGuid().ToString(), FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<EmployeeModel>>();
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.Provider).Returns(employees.Provider);
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());
            mockSet.As<IAsyncEnumerable<EmployeeModel>>().Setup(d => d.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new TestAsyncEnumerator<EmployeeModel>(employees.GetEnumerator()));

            _mockDatabase.Setup(m => m.Employees).Returns(mockSet.Object);

            // Act
            var result = await _handler.Handle(new GetAllEmployeesQuery(), default);

            // Assert
            Assert.Equal(employees.Count(), result.Count);
        }

        [Fact]
        public async Task Handle_MapsEmployeeModelCorrectly()
        {
            // Arrange
            var employee = new EmployeeModel { Id = Guid.NewGuid().ToString(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var employees = new List<EmployeeModel> { employee }.AsQueryable();

            var mockSet = new Mock<DbSet<EmployeeModel>>();
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.Provider).Returns(employees.Provider);
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<EmployeeModel>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());
            mockSet.As<IAsyncEnumerable<EmployeeModel>>().Setup(d => d.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new TestAsyncEnumerator<EmployeeModel>(employees.GetEnumerator()));

            _mockDatabase.Setup(m => m.Employees).Returns(mockSet.Object);

            // Act
            var result = await _handler.Handle(new GetAllEmployeesQuery(), default);
            var employeeDto = result.First();

            // Assert
            Assert.Equal(employee.Id, employeeDto.Id);
            Assert.Equal(employee.FirstName, employeeDto.FirstName);
            Assert.Equal(employee.LastName, employeeDto.LastName);
            Assert.Equal(employee.Email, employeeDto.Email);
        }


        public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public T Current => _inner.Current;

            public ValueTask DisposeAsync()
            {
                _inner.Dispose();
                return new ValueTask();
            }

            public ValueTask<bool> MoveNextAsync()
            {
                return new ValueTask<bool>(_inner.MoveNext());
            }
        }
    }
}