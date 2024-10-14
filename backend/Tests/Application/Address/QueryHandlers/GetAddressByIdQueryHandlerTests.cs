using Application.Queries.Address.GetByID;
using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using Moq;

namespace Tests.Application.Address.QueryHandlers
{
    public class GetAddressByIdQueryHandlerTests
    {
        // Test to verify that GetAddressById calls GetAddressByIdAsync on repository
        [Fact]
        public async Task Handle_GivenValidQuery_CallsGetAddressByIdAsyncOnRepository()
        {
            // Arrange
            var mockAddressRepository = new Mock<IAddressRepository>();
            var address = new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Street1", City = "City1", UnitNumber="#01-02" , Country = "Country1", ZipCode = "ZipCode1" };
            mockAddressRepository.Setup(repo => repo.GetAddressByIdAsync(It.IsAny<Guid>())).ReturnsAsync(address);
            var handler = new GetAddressByIdQueryHandler(mockAddressRepository.Object);
            var query = new GetAddressByIdQuery(address.AddressId);

            // Act
            await handler.Handle(query, new CancellationToken());

            // Assert
            mockAddressRepository.Verify(repo => repo.GetAddressByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        // Test to verify that GetAddressById throws an exception
        [Fact]
        public async Task Handle_GivenInvalidQuery_ThrowsException()
        {
            // Arrange
            var mockAddressRepository = new Mock<IAddressRepository>();
            mockAddressRepository.Setup(repo => repo.GetAddressByIdAsync(It.IsAny<Guid>())).Throws<Exception>();
            var handler = new GetAddressByIdQueryHandler(mockAddressRepository.Object);
            var query = new GetAddressByIdQuery(Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_GivenValidQuery_ReturnsCorrectData()
        {
            // Arrange
            var mockAddressRepository = new Mock<IAddressRepository>();
            var address = new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Street1", UnitNumber="#01-02" , City = "City1", Country = "Country1", ZipCode = "ZipCode1" };
            mockAddressRepository.Setup(repo => repo.GetAddressByIdAsync(It.IsAny<Guid>())).ReturnsAsync(address);
            var handler = new GetAddressByIdQueryHandler(mockAddressRepository.Object);
            var query = new GetAddressByIdQuery(address.AddressId);

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Equal(address, result);
        }
    }
}