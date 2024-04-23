using Application.Commands.Driver.UpdateDriver;
using Domain.Models.Driver;
using Infrastructure.Repositories.DriverRepo;
using Moq;

namespace Tests.Application.Driver.CommandHandlers
{
    public class UpdateDriverCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CallsUpdateDriverOnRepository()
        {
            // Arrange
            var mockDriverRepository = new Mock<IDriverRepository>();
            var handler = new UpdateDriverCommandHandler(mockDriverRepository.Object);
            var command = new UpdateDriverCommand { UpdatedDriver = new DriverModel { Id = Guid.NewGuid().ToString() } };

            // Act
            await handler.Handle(command);

            // Assert
            mockDriverRepository.Verify(repo => repo.UpdateDriver(command.UpdatedDriver), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_PassesCorrectDriverToRepository()
        {
            // Arrange
            var mockDriverRepository = new Mock<IDriverRepository>();
            var handler = new UpdateDriverCommandHandler(mockDriverRepository.Object);
            var command = new UpdateDriverCommand { UpdatedDriver = new DriverModel { Id = Guid.NewGuid().ToString() } };

            // Act
            await handler.Handle(command);

            // Assert
            mockDriverRepository.Verify(repo => repo.UpdateDriver(It.Is<DriverModel>(d => d.Id == command.UpdatedDriver.Id)), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockDriverRepository = new Mock<IDriverRepository>();
            mockDriverRepository.Setup(repo => repo.UpdateDriver(It.IsAny<DriverModel>())).Throws<Exception>();
            var handler = new UpdateDriverCommandHandler(mockDriverRepository.Object);
            var command = new UpdateDriverCommand { UpdatedDriver = new DriverModel { Id = Guid.NewGuid().ToString() } };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command));
        }
    }
}