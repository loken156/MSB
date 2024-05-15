using Application.Commands.Address.AddAddress;
using Application.Dto.Adress;
using AutoMapper;
using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using Moq;

namespace Tests.Application.Address.CommandHandlers
{
    public class AddAddressCommandHandlerTests
    {
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AddAddressCommandHandler _handler;

        public AddAddressCommandHandlerTests()
        {
            _addressRepositoryMock = new Mock<IAddressRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new AddAddressCommandHandler(_addressRepositoryMock.Object, _mapperMock.Object);

            // Setup default behaviors for mapper
            _mapperMock.Setup(m => m.Map<AddressModel>(It.IsAny<AddressDto>())).Returns(new AddressModel());
            _mapperMock.Setup(m => m.Map<AddressDto>(It.IsAny<AddressModel>())).Returns(new AddressDto());
        }

        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsCorrectAddressDto()
        {
            // Arrange
            var newAddressDto = new AddressDto
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
            };

            var command = new AddAddressCommand(newAddressDto);

            _mapperMock.Setup(m => m.Map<AddressDto>(It.IsAny<AddressModel>())).Returns(newAddressDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(newAddressDto.StreetName, result.StreetName);
            Assert.Equal(newAddressDto.StreetNumber, result.StreetNumber);
            Assert.Equal(newAddressDto.Apartment, result.Apartment);
            Assert.Equal(newAddressDto.ZipCode, result.ZipCode);
            Assert.Equal(newAddressDto.Floor, result.Floor);
            Assert.Equal(newAddressDto.City, result.City);
            Assert.Equal(newAddressDto.State, result.State);
            Assert.Equal(newAddressDto.Country, result.Country);
            Assert.Equal(newAddressDto.Latitude, result.Latitude);
            Assert.Equal(newAddressDto.Longitude, result.Longitude);
        }

        [Fact]
        public async Task Handle_GivenNullValues_ReturnsAddressDtoWithEmptyStrings()
        {
            // Arrange
            var newAddressDto = new AddressDto
            {
                StreetName = string.Empty,
                StreetNumber = string.Empty,
                ZipCode = string.Empty
            };

            var command = new AddAddressCommand(newAddressDto);

            _mapperMock.Setup(m => m.Map<AddressDto>(It.IsAny<AddressModel>())).Returns(newAddressDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(string.Empty, result.StreetName);
            Assert.Equal(string.Empty, result.StreetNumber);
            Assert.Equal(string.Empty, result.ZipCode);
        }
    }
}