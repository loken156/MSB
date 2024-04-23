using Application.Commands.Driver.AddDriver;
using Domain.Models.Driver;
using Infrastructure.Repositories.DriverRepo;
using Moq;

namespace Tests.Application.Driver.CommandHandlers
{
    public class AddDriverCommandHandlerTests
    {
        [Fact]
        public void Handle_GivenValidCommand_CallsAddDriverOnRepository()
        {
            // Arrange
            var mockDriverRepository = new Mock<IDriverRepository>();
            var handler = new AddDriverCommandHandler(mockDriverRepository.Object);
            var command = new AddDriverCommand { DriverId = Guid.NewGuid() };

            // Act
            handler.Handle(command);

            // Assert
            mockDriverRepository.Verify(repo => repo.AddDriver(It.IsAny<DriverModel>()), Times.Once);
        }

        [Fact]
        public void Handle_GivenValidCommand_PassesCorrectDriverIdToRepository()
        {
            // Arrange
            var mockDriverRepository = new Mock<IDriverRepository>();
            var handler = new AddDriverCommandHandler(mockDriverRepository.Object);
            var command = new AddDriverCommand { DriverId = Guid.NewGuid() };

            // Act
            handler.Handle(command);

            // Assert
            mockDriverRepository.Verify(repo => repo.AddDriver(It.Is<DriverModel>(d => d.Id == command.DriverId.ToString())), Times.Once);
        }

        [Fact]
        public void Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockDriverRepository = new Mock<IDriverRepository>();
            mockDriverRepository.Setup(repo => repo.AddDriver(It.IsAny<DriverModel>())).Throws<Exception>();
            var handler = new AddDriverCommandHandler(mockDriverRepository.Object);
            var command = new AddDriverCommand { DriverId = Guid.NewGuid() };

            // Act & Assert
            Assert.Throws<Exception>(() => handler.Handle(command));
        }
    }
}