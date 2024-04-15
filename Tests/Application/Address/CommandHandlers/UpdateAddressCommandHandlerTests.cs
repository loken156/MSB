using Application.Commands.Address.UpdateAddress;
using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using Moq;

namespace Tests.Application.Address.CommandHandlers
{
    public class UpdateAddressCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_CallsUpdateAddressAsyncOnRepository()
        {
            // Arrange
            var mockAddressRepository = new Mock<IAddressRepository>();
            var handler = new UpdateAddressCommandHandler(mockAddressRepository.Object);
            var address = new AddressModel { StreetName = "Test Street" };
            var command = new UpdateAddressCommand(address);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockAddressRepository.Verify(repo => repo.UpdateAddressAsync(address), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsUpdatedAddressModel()
        {
            // Arrange
            var updatedAddress = new AddressModel { StreetName = "Updated Street" };
            var mockAddressRepository = new Mock<IAddressRepository>();
            mockAddressRepository.Setup(repo => repo.UpdateAddressAsync(It.IsAny<AddressModel>()))
                .ReturnsAsync(updatedAddress);
            var handler = new UpdateAddressCommandHandler(mockAddressRepository.Object);
            var address = new AddressModel { StreetName = "Test Street" };
            var command = new UpdateAddressCommand(address);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(updatedAddress, result);
        }
    }
}
