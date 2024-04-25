using Application.Commands.Box.AddBox;
using Application.Dto.Box;
using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Application.Box.CommandHandlers
{
    public class AddBoxCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CreatesNewBoxWithCorrectProperties()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            mockBoxRepository.Setup(repo => repo.AddBoxAsync(It.IsAny<BoxModel>(), It.IsAny<Guid>()))
                .ReturnsAsync((BoxModel box, Guid id) => box);

            var mockLogger = new Mock<ILogger<AddBoxCommandHandler>>();

            var handler = new AddBoxCommandHandler(mockBoxRepository.Object, mockLogger.Object);
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
            var mockBoxRepository = new Mock<IBoxRepository>();
            mockBoxRepository.Setup(repo => repo.AddBoxAsync(It.IsAny<BoxModel>(), It.IsAny<Guid>()))
                .ReturnsAsync((BoxModel box, Guid id) => box);

            var mockLogger = new Mock<ILogger<AddBoxCommandHandler>>();

            var handler = new AddBoxCommandHandler(mockBoxRepository.Object, mockLogger.Object);
            var newBox = new BoxDto { Type = "Type1", TimesUsed = 1, Stock = 10, ImageUrl = "http://example.com", UserNotes = "Note", Size = "Large" };
            var command = new AddBoxCommand(newBox, Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, result.BoxId);
        }
    }
}