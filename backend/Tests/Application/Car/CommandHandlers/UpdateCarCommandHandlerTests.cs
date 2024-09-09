using Application.Commands.Car.UpdateCar;
using Application.Dto.Car;
using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;
using Moq;

namespace Tests.Application.Car.CommandHandlers
{
    public class UpdateCarCommandHandlerTests
    {
        // Test to verify that UpdateCar calls UpdateCar on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsUpdateCarOnRepository()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var carModel = new CarModel { CarId = Guid.NewGuid(), Volume = 100, Type = "Sedan", Availability = "Available" };
            mockCarRepository.Setup(repo => repo.GetCarById(It.IsAny<Guid>())).ReturnsAsync(carModel);
            var handler = new UpdateCarCommandHandler(mockCarRepository.Object);
            var updatedCar = new CarDto { CarId = carModel.CarId, Volume = 200, Type = "SUV", Availability = "Not available" };
            var command = new UpdateCarCommand(carModel.CarId, updatedCar);

            // Act
            await handler.Handle(command);

            // Assert
            mockCarRepository.Verify(repo => repo.UpdateCar(It.IsAny<CarModel>()), Times.Once);
        }

        // Test to verify that UpdateCar passes correct car to repository
        [Fact]
        public async Task Handle_GivenNonExistentCar_DoesNotCallUpdateCarOnRepository()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(repo => repo.GetCarById(It.IsAny<Guid>())).ReturnsAsync((CarModel)null);
            var handler = new UpdateCarCommandHandler(mockCarRepository.Object);
            var updatedCar = new CarDto { CarId = Guid.NewGuid(), Volume = 200, Type = "SUV", Availability = "Not available" };
            var command = new UpdateCarCommand(Guid.NewGuid(), updatedCar);

            // Act
            await handler.Handle(command);

            // Assert
            mockCarRepository.Verify(repo => repo.UpdateCar(It.IsAny<CarModel>()), Times.Never);
        }

        // Test to verify that UpdateCar throws an exception
        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(repo => repo.GetCarById(It.IsAny<Guid>())).Throws<Exception>();
            var handler = new UpdateCarCommandHandler(mockCarRepository.Object);
            var updatedCar = new CarDto { CarId = Guid.NewGuid(), Volume = 200, Type = "SUV", Availability = "Not available" };
            var command = new UpdateCarCommand(Guid.NewGuid(), updatedCar);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command));
        }
    }
}