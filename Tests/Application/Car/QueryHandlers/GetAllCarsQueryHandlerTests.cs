using Application.Queries.Car;
using Domain.Models.Car;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Application.Car.QueryHandlers
{
    public class GetAllCarsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidQuery_AccessesCarsOnDatabase()
        {
            // Arrange
            var data = new List<CarModel>
            {
                new CarModel { CarId = Guid.NewGuid(), Volume = 1000, Type = "Type1", Availability = "Available" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CarModel>>();
            mockSet.As<IQueryable<CarModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CarModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CarModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CarModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.As<IAsyncEnumerable<CarModel>>().Setup(d => d.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new TestAsyncEnumerator<CarModel>(data.GetEnumerator()));

            var mockContext = new Mock<IMSBDatabase>();
            mockContext.Setup(c => c.Cars).Returns(mockSet.Object);

            var handler = new GetAllCarsQueryHandler(mockContext.Object);
            var query = new GetAllCarsQuery();

            // Act
            var result = await handler.Handle(query);

            // Assert
            Assert.Equal(data.ToList(), result);
        }

        [Fact]
        public async Task Handle_GivenInvalidQuery_ThrowsException()
        {
            // Arrange
            var mockSet = new Mock<DbSet<CarModel>>();
            mockSet.As<IAsyncEnumerable<CarModel>>().Setup(m => m.GetAsyncEnumerator(default)).Throws<Exception>();

            var mockContext = new Mock<IMSBDatabase>();
            mockContext.Setup(c => c.Cars).Returns(mockSet.Object);

            var handler = new GetAllCarsQueryHandler(mockContext.Object);
            var query = new GetAllCarsQuery();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(query));
        }
    }

    // Using this class to mock the GetSyncEnumerator() method of DbSet<CarModel> so that ToListAsync() works correctly in the tests
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