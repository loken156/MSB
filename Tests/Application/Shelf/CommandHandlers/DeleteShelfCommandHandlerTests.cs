using Application.Commands.Shelf.DeleteShelf;
using Infrastructure.Repositories.ShelfRepo;
using Moq;

namespace Tests.Application.Shelf.CommandHandlers
{
    public class DeleteShelfCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CallsDeleteShelfAsyncOnRepository()
        {
            // Arrange
            var mockShelfRepository = new Mock<IShelfRepository>();
            var handler = new DeleteShelfCommandHandler(mockShelfRepository.Object);
            var command = new DeleteShelfCommand(Guid.NewGuid());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockShelfRepository.Verify(repo => repo.DeleteShelfAsync(command.ShelfId), Times.Once);
        }
    }
}