using Application.Commands.Address.DeleteAddress;
using Infrastructure.Repositories.AddressRepo;
using Moq;

namespace Tests.Application.Address.CommandHandlers
{
    public class DeleteAddressCommandHandlerTests
    {
        // Test to verify that DeleteAddress calls DeleteAddressAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsDeleteAddressAsyncOnRepository()
        {
            // Arrange
            var mockAddressRepository = new Mock<IAddressRepository>();
            var handler = new DeleteAddressCommandHandler(mockAddressRepository.Object);
            var command = new DeleteAddressCommand(Guid.NewGuid());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockAddressRepository.Verify(repo => repo.DeleteAddressAsync(command.AddressId), Times.Once);
        }

        // Test to verify that DeleteAddress does not throw an exception
        [Fact]
        public async Task Handle_GivenInvalidCommand_ThrowsException()
        {
            // Arrange
            var mockAddressRepository = new Mock<IAddressRepository>();
            mockAddressRepository.Setup(repo => repo.DeleteAddressAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception());
            var handler = new DeleteAddressCommandHandler(mockAddressRepository.Object);
            var command = new DeleteAddressCommand(Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}