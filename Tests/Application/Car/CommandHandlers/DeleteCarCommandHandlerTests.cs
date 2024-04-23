using Application.Commands.Car.DeleteCar;
using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;
using Moq;

namespace Tests.Application.Car.CommandHandlers
{
    public class DeleteCarCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CallsDeleteCarOnRepository()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var carModel = new CarModel { CarId = Guid.NewGuid(), Volume = 100, Type = "Sedan", Availability = "Available" };
            mockCarRepository.Setup(repo => repo.GetCarById(It.IsAny<Guid>())).ReturnsAsync(carModel);
            var handler = new DeleteCarCommandHandler(mockCarRepository.Object);
            var command = new DeleteCarCommand(carModel.CarId);

            // Act
            await handler.Handle(command);

            // Assert
            mockCarRepository.Verify(repo => repo.DeleteCar(It.IsAny<CarModel>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenNonExistentCar_DoesNotCallDeleteCarOnRepository()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(repo => repo.GetCarById(It.IsAny<Guid>())).ReturnsAsync((CarModel)null);
            var handler = new DeleteCarCommandHandler(mockCarRepository.Object);
            var command = new DeleteCarCommand(Guid.NewGuid());

            // Act
            await handler.Handle(command);

            // Assert
            mockCarRepository.Verify(repo => repo.DeleteCar(It.IsAny<CarModel>()), Times.Never);
        }

        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(repo => repo.GetCarById(It.IsAny<Guid>())).Throws<Exception>();
            var handler = new DeleteCarCommandHandler(mockCarRepository.Object);
            var command = new DeleteCarCommand(Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command));
        }
    }
}