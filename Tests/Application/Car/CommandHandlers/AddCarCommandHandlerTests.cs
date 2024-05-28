using Application.Commands.Car.AddCar;
using Application.Dto.Car;
using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;
using Moq;

namespace Tests.Application.Car.CommandHandlers
{
    public class AddCarCommandHandlerTests
    {
        // Test to verify that AddCar calls AddCar on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsAddCarOnRepository()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var handler = new AddCarCommandHandler(mockCarRepository.Object);
            var carDto = new CarDto { CarId = Guid.NewGuid(), Volume = 100, Type = "Sedan", Availability = "Available" };
            var command = new AddCarCommand(carDto);

            // Act
            await handler.Handle(command);

            // Assert
            mockCarRepository.Verify(repo => repo.AddCar(It.IsAny<CarModel>()), Times.Once);
        }

        // Test to verify that AddCar passes correct car to repository
        [Fact]
        public async Task Handle_GivenValidCommand_PassesCorrectCarToRepository()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            var handler = new AddCarCommandHandler(mockCarRepository.Object);
            var carDto = new CarDto { CarId = Guid.NewGuid(), Volume = 100, Type = "Sedan", Availability = "Available" };
            var command = new AddCarCommand(carDto);

            // Act
            await handler.Handle(command);

            // Assert
            mockCarRepository.Verify(repo => repo.AddCar(It.Is<CarModel>(c => c.CarId == command.Car.CarId && c.Volume == command.Car.Volume && c.Type == command.Car.Type && c.Availability == command.Car.Availability)), Times.Once);
        }

        // Test to verify that AddCar returns added car model
        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockCarRepository = new Mock<ICarRepository>();
            mockCarRepository.Setup(repo => repo.AddCar(It.IsAny<CarModel>())).Throws<Exception>();
            var handler = new AddCarCommandHandler(mockCarRepository.Object);
            var carDto = new CarDto { CarId = Guid.NewGuid(), Volume = 100, Type = "Sedan", Availability = "Available" };
            var command = new AddCarCommand(carDto);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command));
        }
    }
}