using Application.Commands.Address.AddAddress;
using Domain.Models.Address;

namespace Tests.Application.Address.CommandHandlers
{
    public class AddAddressCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsCorrectAddressModel()
        {
            // Arrange
            var handler = new AddAddressCommandHandler();
            var command = new AddAddressCommand(new AddressModel
            {
                StreetName = "Test Street",
                StreetNumber = "123",
                Apartment = "4B",
                ZipCode = "12345",
                Floor = "2",
                City = "Test City",
                State = "Test State",
                Country = "Test Country",
                Latitude = 12.34,
                Longitude = 56.78
            });


            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(command.NewAddress.StreetName, result.StreetName);
            Assert.Equal(command.NewAddress.StreetNumber, result.StreetNumber);
            Assert.Equal(command.NewAddress.Apartment, result.Apartment);
            Assert.Equal(command.NewAddress.ZipCode, result.ZipCode);
            Assert.Equal(command.NewAddress.Floor, result.Floor);
            Assert.Equal(command.NewAddress.City, result.City);
            Assert.Equal(command.NewAddress.State, result.State);
            Assert.Equal(command.NewAddress.Country, result.Country);
            Assert.Equal(command.NewAddress.Latitude, result.Latitude);
            Assert.Equal(command.NewAddress.Longitude, result.Longitude);
        }

        [Fact]
        public async Task Handle_GivenNullValues_ReturnsAddressModelWithEmptyStrings()
        {
            // Arrange
            var handler = new AddAddressCommandHandler();
            var command = new AddAddressCommand(new AddressModel
            {
                StreetName = null,
                StreetNumber = null,
                ZipCode = null
            });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(string.Empty, result.StreetName);
            Assert.Equal(string.Empty, result.StreetNumber);
            Assert.Equal(string.Empty, result.ZipCode);
        }
    }
}
