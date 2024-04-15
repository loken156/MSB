using Application.Commands.Box.AddBox;
using Application.Dto.Box;

namespace Tests.Application.Box.CommandHandlers
{
    public class AddBoxCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CreatesNewBoxWithCorrectProperties()
        {
            // Arrange
            var handler = new AddBoxCommandHandler();
            var newBox = new BoxDto { Type = "Type1", TimesUsed = 1, Stock = 10, ImageUrl = "http://example.com", UserNotes = "Note", Size = "Large" };
            var command = new AddBoxCommand(newBox, Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.Equal(newBox.Type, result.Type);
            Assert.Equal(newBox.TimesUsed, result.TimesUsed);
            Assert.Equal(newBox.Stock, result.Stock);
            Assert.Equal(newBox.ImageUrl, result.ImageUrl);
            Assert.Equal(newBox.UserNotes, result.UserNotes);
            Assert.Equal(newBox.Size, result.Size);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_CreatesNewBoxWithNonEmptyId()
        {
            // Arrange
            var handler = new AddBoxCommandHandler();
            var newBox = new BoxDto { Type = "Type1", TimesUsed = 1, Stock = 10, ImageUrl = "http://example.com", UserNotes = "Note", Size = "Large" };
            var command = new AddBoxCommand(newBox, Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, result.BoxId);
        }
    }
}
