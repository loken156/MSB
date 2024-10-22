/*using Application.Commands.Box.UpdateBox;
using Application.Dto.Box;
using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using Moq;

namespace Tests.Application.Box.CommandHandlers
{
    public class UpdateBoxCommandHandlerTests
    {
        // Test to verify that UpdateBox calls UpdateBoxAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsUpdateBoxAsyncOnRepository()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            var boxDto = new BoxDto { BoxId = Guid.NewGuid(), Type = "Type1", TimesUsed = 1, Stock = 10, ImageUrl = "http://example.com", UserNotes = "Note", Size = "Large" };
            var boxModel = new BoxModel { BoxId = boxDto.BoxId, Type = boxDto.Type, TimesUsed = boxDto.TimesUsed, Stock = boxDto.Stock, ImageUrl = boxDto.ImageUrl, UserNotes = boxDto.UserNotes, Size = boxDto.Size };
            mockBoxRepository.Setup(repo => repo.UpdateBoxAsync(It.IsAny<BoxModel>())).ReturnsAsync(boxModel);
            var handler = new UpdateBoxCommandHandler(mockBoxRepository.Object);
            var command = new UpdateBoxCommand(boxDto);

            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            mockBoxRepository.Verify(repo => repo.UpdateBoxAsync(It.IsAny<BoxModel>()), Times.Once);
        }

        // Test to verify that UpdateBox returns updated box model
        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            mockBoxRepository.Setup(repo => repo.UpdateBoxAsync(It.IsAny<BoxModel>())).Throws<Exception>();
            var handler = new UpdateBoxCommandHandler(mockBoxRepository.Object);
            var boxDto = new BoxDto { BoxId = Guid.NewGuid(), Type = "Type1", TimesUsed = 1, Stock = 10, ImageUrl = "http://example.com", UserNotes = "Note", Size = "Large" };
            var command = new UpdateBoxCommand(boxDto);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, new CancellationToken()));
        }
    }
}*/