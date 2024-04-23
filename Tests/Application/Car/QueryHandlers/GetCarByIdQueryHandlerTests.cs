using Application.Queries.Car.GetById;
using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Application.Car.QueryHandlers
{
    public class GetCarByIdQueryHandlerTests
    {
        private readonly Mock<IMSBDatabase> _mockContext;
        private readonly Mock<ICarRepository> _mockCarRepository;
        private readonly GetCarByIdQueryHandler _handler;

        public GetCarByIdQueryHandlerTests()
        {
            _mockContext = new Mock<IMSBDatabase>();
            _mockCarRepository = new Mock<ICarRepository>();
            _handler = new GetCarByIdQueryHandler(_mockCarRepository.Object, _mockContext.Object);
        }

        [Fact]
        public async Task Handle_GivenValidId_ReturnsCar()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var car = new CarModel { CarId = carId };
            _mockCarRepository.Setup(m => m.GetCarById(carId)).ReturnsAsync(car);

            var mockSet = new Mock<DbSet<CarModel>>();
            mockSet.Setup(m => m.FindAsync(carId)).ReturnsAsync(car);
            _mockContext.Setup(m => m.Cars).Returns(mockSet.Object);

            // Act
            var result = await _handler.Handle(new GetCarByIdQuery(carId));

            // Assert
            Assert.Equal(car, result);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ThrowsException()
        {
            // Arrange
            var carId = Guid.NewGuid();
            _mockCarRepository.Setup(m => m.GetCarById(carId)).ReturnsAsync((CarModel?)null);

            var mockSet = new Mock<DbSet<CarModel>>();
            mockSet.Setup(m => m.FindAsync(carId)).ReturnsAsync((CarModel?)null);
            _mockContext.Setup(m => m.Cars).Returns(mockSet.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetCarByIdQuery(carId)));
        }
    }
}