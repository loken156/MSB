/*using Application.Commands.Box.AddBox;
using Application.Dto.Box;
using AutoMapper;
using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Application.Box.CommandHandlers
{
    public class AddBoxCommandHandlerTests
    {
        private readonly Mock<IBoxRepository> _mockBoxRepository;
        private readonly Mock<ILogger<AddBoxCommandHandler>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddBoxCommandHandler _handler;
        private readonly Mock<IMediator> _mockMediator;

        public AddBoxCommandHandlerTests()
        {
            _mockBoxRepository = new Mock<IBoxRepository>();
            _mockLogger = new Mock<ILogger<AddBoxCommandHandler>>();
            _mockMapper = new Mock<IMapper>();
            _mockMediator = new Mock<IMediator>();
            _handler = new AddBoxCommandHandler(_mockBoxRepository.Object, _mockLogger.Object, _mockMapper.Object);
        }

        // Test to verify that AddBox returns a BoxDto when box is added
        [Fact]
        public async Task Handle_GivenValidCommand_CreatesNewBoxWithCorrectProperties()
        {
            // Arrange
            var newBoxDto = new BoxDto { Type = "Type1", TimesUsed = 1, Stock = 10, ImageUrl = "http://example.com", UserNotes = "Note", Size = "Large" };
            var newBoxModel = new BoxModel(); // Assuming default values or constructor logic
            _mockMapper.Setup(m => m.Map<BoxModel>(It.IsAny<BoxDto>())).Returns(newBoxModel);
            _mockMapper.Setup(m => m.Map<BoxDto>(It.IsAny<BoxModel>())).Returns(newBoxDto);
            _mockBoxRepository.Setup(repo => repo.AddBoxAsync(It.IsAny<BoxModel>())).ReturnsAsync(newBoxModel);

            var command = new AddBoxCommand(newBoxDto);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            _mockMapper.Verify(m => m.Map<BoxModel>(It.IsAny<BoxDto>()), Times.Once);
            _mockMapper.Verify(m => m.Map<BoxDto>(It.IsAny<BoxModel>()), Times.Once);
            Assert.Equal(newBoxDto.Type, result.Type);
            Assert.Equal(newBoxDto.TimesUsed, result.TimesUsed);
            Assert.Equal(newBoxDto.Stock, result.Stock);
            Assert.Equal(newBoxDto.ImageUrl, result.ImageUrl);
            Assert.Equal(newBoxDto.UserNotes, result.UserNotes);
            Assert.Equal(newBoxDto.Size, result.Size);
        }

        // Test to verify that AddBox returns a BoxDto with non-empty ID
        [Fact]
        public async Task Handle_GivenValidCommand_CreatesNewBoxWithNonEmptyId()
        {
            // Arrange
            var newBoxDto = new BoxDto();
            var newBoxModel = new BoxModel { BoxId = Guid.NewGuid() }; // Ensure the BoxModel has a non-empty ID
            _mockMapper.Setup(m => m.Map<BoxModel>(It.IsAny<BoxDto>())).Returns(newBoxModel);
            _mockMapper.Setup(m => m.Map<BoxDto>(It.IsAny<BoxModel>())).Returns<BoxModel>(src => new BoxDto { BoxId = src.BoxId });
            _mockBoxRepository.Setup(repo => repo.AddBoxAsync(It.IsAny<BoxModel>())).ReturnsAsync(newBoxModel);

            var command = new AddBoxCommand(newBoxDto);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, result.BoxId);

        }
    }
}*/