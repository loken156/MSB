using Application.Queries.Car.GetById;
using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;
using Moq;

namespace Tests.Application.Car.QueryHandlers
{
    public class GetCarByIdQueryHandlerTests
    {
        private readonly Mock<ICarRepository> _mockCarRepository;
        private readonly GetCarByIdQueryHandler _handler;

        public GetCarByIdQueryHandlerTests()
        {
            _mockCarRepository = new Mock<ICarRepository>();
            _handler = new GetCarByIdQueryHandler(_mockCarRepository.Object);
        }

        [Fact]
        public async Task Handle_GivenValidId_ReturnsCar()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var car = new CarModel { CarId = carId };
            _mockCarRepository.Setup(m => m.GetCarById(carId)).ReturnsAsync(car);

            // Act
            var result = await _handler.Handle(new GetCarByIdQuery(carId), CancellationToken.None);

            // Assert
            Assert.Equal(car, result);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ThrowsException()
        {
            // Arrange
            var carId = Guid.NewGuid();
            _mockCarRepository.Setup(m => m.GetCarById(carId)).ReturnsAsync((CarModel?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetCarByIdQuery(carId), CancellationToken.None));
        }
    }
}