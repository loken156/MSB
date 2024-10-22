/*using Application.Queries.Box.GetByID;
using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using Moq;

namespace Tests.Application.Box.QueryHandlers
{
    public class GetBoxByIdQueryHandlerTests
    {
        // Test to verify that GetBoxById calls GetBoxByIdAsync on repository
        [Fact]
        public async Task Handle_GivenValidQuery_CallsGetBoxByIdAsyncOnRepository()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            var box = new BoxModel { BoxId = Guid.NewGuid(), Type = "Type1", TimesUsed = 1, Stock = 10, ImageUrl = "http://example.com", UserNotes = "Note", Size = "Large" };
            mockBoxRepository.Setup(repo => repo.GetBoxByIdAsync(It.IsAny<Guid>())).ReturnsAsync(box);
            var handler = new GetBoxByIdQueryHandler(mockBoxRepository.Object);
            var query = new GetBoxByIdQuery(box.BoxId);

            // Act
            await handler.Handle(query, new CancellationToken());

            // Assert
            mockBoxRepository.Verify(repo => repo.GetBoxByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        // Test to verify that GetBoxById throws an exception
        [Fact]
        public async Task Handle_GivenInvalidQuery_ThrowsException()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            mockBoxRepository.Setup(repo => repo.GetBoxByIdAsync(It.IsAny<Guid>())).Throws<Exception>();
            var handler = new GetBoxByIdQueryHandler(mockBoxRepository.Object);
            var query = new GetBoxByIdQuery(Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, new CancellationToken()));
        }

        // Test to verify that GetBoxById returns correct data
        [Fact]
        public async Task Handle_GivenValidQuery_ReturnsCorrectData()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            var box = new BoxModel { BoxId = Guid.NewGuid(), Type = "Type1", TimesUsed = 1, Stock = 10, ImageUrl = "http://example.com", UserNotes = "Note", Size = "Large" };
            mockBoxRepository.Setup(repo => repo.GetBoxByIdAsync(It.IsAny<Guid>())).ReturnsAsync(box);
            var handler = new GetBoxByIdQueryHandler(mockBoxRepository.Object);
            var query = new GetBoxByIdQuery(box.BoxId);

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Equal(box, result);
        }
    }
}*/