/*using Application.Commands.Box.DeleteBox;
using Infrastructure.Repositories.BoxRepo;
using Moq;

namespace Tests.Application.Box.CommandHandlers
{
    public class DeleteBoxCommandHandlerTests
    {
        // Test to verify that DeleteBox calls DeleteBoxAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsDeleteBoxAsyncOnRepository()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            mockBoxRepository.Setup(repo => repo.DeleteBoxAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            var handler = new DeleteBoxCommandHandler(mockBoxRepository.Object);
            var command = new DeleteBoxCommand(Guid.NewGuid());

            // Act
            await handler.Handle(command, new CancellationToken());

            // Assert
            mockBoxRepository.Verify(repo => repo.DeleteBoxAsync(It.IsAny<Guid>()), Times.Once);
        }

        // Test to verify that DeleteBox throws an exception
        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            mockBoxRepository.Setup(repo => repo.DeleteBoxAsync(It.IsAny<Guid>())).Throws<Exception>();
            var handler = new DeleteBoxCommandHandler(mockBoxRepository.Object);
            var command = new DeleteBoxCommand(Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, new CancellationToken()));
        }
    }
}*/