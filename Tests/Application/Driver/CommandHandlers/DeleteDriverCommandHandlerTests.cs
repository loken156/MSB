using Application.Commands.Driver.DeleteDriver;
using Infrastructure.Repositories.DriverRepo;
using Moq;

namespace Tests.Application.Driver.CommandHandlers
{
    public class DeleteDriverCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CallsDeleteDriverOnRepository()
        {
            // Arrange
            var mockDriverRepository = new Mock<IDriverRepository>();
            var handler = new DeleteDriverCommandHandler(mockDriverRepository.Object);
            var command = new DeleteDriverCommand { DriverId = Guid.NewGuid() };

            // Act
            await handler.Handle(command);

            // Assert
            mockDriverRepository.Verify(repo => repo.DeleteDriver(command.DriverId), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_PassesCorrectDriverIdToRepository()
        {
            // Arrange
            var mockDriverRepository = new Mock<IDriverRepository>();
            var handler = new DeleteDriverCommandHandler(mockDriverRepository.Object);
            var command = new DeleteDriverCommand { DriverId = Guid.NewGuid() };

            // Act
            await handler.Handle(command);

            // Assert
            mockDriverRepository.Verify(repo => repo.DeleteDriver(command.DriverId), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockDriverRepository = new Mock<IDriverRepository>();
            mockDriverRepository.Setup(repo => repo.DeleteDriver(It.IsAny<Guid>())).Throws<Exception>();
            var handler = new DeleteDriverCommandHandler(mockDriverRepository.Object);
            var command = new DeleteDriverCommand { DriverId = Guid.NewGuid() };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command));
        }
    }
}

