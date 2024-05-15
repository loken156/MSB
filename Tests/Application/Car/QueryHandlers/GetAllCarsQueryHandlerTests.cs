using Application.Queries.Car;
using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;
using Moq;

namespace Tests.Application.Car.QueryHandlers
{
    public class GetAllCarsQueryHandlerTests
    {
        private readonly Mock<ICarRepository> _mockCarRepository;
        private readonly GetAllCarsQueryHandler _handler;

        public GetAllCarsQueryHandlerTests()
        {
            _mockCarRepository = new Mock<ICarRepository>();
            _handler = new GetAllCarsQueryHandler(_mockCarRepository.Object);
        }

        [Fact]
        public async Task Handle_GivenValidQuery_AccessesCarsOnDatabase()
        {
            // Arrange
            var cars = new List<CarModel>
            {
                new CarModel { CarId = Guid.NewGuid(), Volume = 1000, Type = "Type1", Availability = "Available" }
            };
            _mockCarRepository.Setup(m => m.GetAllCars()).ReturnsAsync(cars);

            // Act
            var result = await _handler.Handle(new GetAllCarsQuery());

            // Assert
            Assert.Equal(cars, result);
        }

        [Fact]
        public async Task Handle_GivenInvalidQuery_ThrowsException()
        {
            // Arrange
            _mockCarRepository.Setup(m => m.GetAllCars()).Throws<Exception>();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetAllCarsQuery()));
        }
    }
}