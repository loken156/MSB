using Application.Queries.Box.GetAll;
using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using Moq;

namespace Tests.Application.Box.QueryHandlers
{
    public class GetAllBoxesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidQuery_CallsGetAllBoxesAsyncOnRepository()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            mockBoxRepository.Setup(repo => repo.GetAllBoxesAsync()).ReturnsAsync(new List<BoxModel>());
            var handler = new GetAllBoxesQueryHandler(mockBoxRepository.Object);
            var query = new GetAllBoxesQuery();

            // Act
            await handler.Handle(query, new CancellationToken());

            // Assert
            mockBoxRepository.Verify(repo => repo.GetAllBoxesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenInvalidQuery_ThrowsException()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            mockBoxRepository.Setup(repo => repo.GetAllBoxesAsync()).Throws<Exception>();
            var handler = new GetAllBoxesQueryHandler(mockBoxRepository.Object);
            var query = new GetAllBoxesQuery();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_GivenValidQuery_ReturnsCorrectData()
        {
            // Arrange
            var mockBoxRepository = new Mock<IBoxRepository>();
            var boxes = new List<BoxModel> { new BoxModel { BoxId = Guid.NewGuid(), Type = "Type1", TimesUsed = 1, Stock = 10, ImageUrl = "http://example.com", UserNotes = "Note", Size = "Large" } };
            mockBoxRepository.Setup(repo => repo.GetAllBoxesAsync()).ReturnsAsync(boxes);
            var handler = new GetAllBoxesQueryHandler(mockBoxRepository.Object);
            var query = new GetAllBoxesQuery();

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Equal(boxes, result);
        }
    }
}


