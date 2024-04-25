using API.Controllers.Address;
using Application.Commands.Address.AddAddress;
using Application.Commands.Address.DeleteAddress;
using Application.Commands.Address.UpdateAddress;
using Application.Dto.Adress;
using Application.Queries.Address.GetAll;
using Application.Queries.Address.GetByID;
using Application.Validators.AddressValidator;
using Domain.Models.Address;
using FluentValidation.Results;
using Infrastructure.Repositories.OrderRepo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.API.Controllers
{
    public class AddressControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<AddressRepository> _addressRepositoryMock;
        private readonly Mock<AddressValidations> _addressValidationsMock;
        private readonly Mock<ILogger<AddressController>> _loggerMock;
        private readonly AddressController _controller;

        public AddressControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _configurationMock = new Mock<IConfiguration>();
            _addressRepositoryMock = new Mock<AddressRepository>();
            _addressValidationsMock = new Mock<AddressValidations>();
            _loggerMock = new Mock<ILogger<AddressController>>();
            _controller = new AddressController(
                _mediatorMock.Object,
                _configurationMock.Object,
                _addressRepositoryMock.Object,
                _addressValidationsMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task AddAddress_ReturnsCreatedAtAction_WhenModelStateIsValid()
        {
            // Arrange
            var command = new AddAddressCommand(new AddressModel());
            var address = new AddressModel { AddressId = Guid.NewGuid() };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(address);
            _addressValidationsMock.Setup(v => v.Validate(It.IsAny<AddressDto>())).Returns(new ValidationResult());

            // Act
            var result = await _controller.AddAddress(command);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<AddressModel>(createdAtActionResult.Value);
            Assert.Equal(address, returnValue);
        }

        [Fact]
        public async Task AddAddress_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var command = new AddAddressCommand(new AddressModel());
            _addressValidationsMock.Setup(v => v.Validate(It.IsAny<AddressDto>())).Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure("TestProperty", "TestError") }));

            // Act
            var result = await _controller.AddAddress(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllAddresses_ReturnsOk_WhenAddressesExist()
        {
            // Arrange
            var addresses = new List<AddressModel> { new AddressModel(), new AddressModel() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAddressesQuery>(), default)).ReturnsAsync(addresses);

            // Act
            var result = await _controller.GetAllAddresses();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<AddressModel>>(okResult.Value);
            Assert.Equal(addresses, returnValue);
        }

        [Fact]
        public async Task GetAddressById_ReturnsOk_WhenAddressExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var address = new AddressModel { AddressId = id };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAddressByIdQuery>(), default)).ReturnsAsync(address);

            // Act
            var result = await _controller.GetAddressById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<AddressModel>(okResult.Value);
            Assert.Equal(address, returnValue);
        }

        [Fact]
        public async Task GetAddressById_ReturnsNotFound_WhenAddressDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAddressByIdQuery>(), default)).Returns(Task.FromResult<AddressModel>(null));

            // Act
            var result = await _controller.GetAddressById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateAddress_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            var address = new AddressModel { AddressId = id };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateAddressCommand>(), default)).ReturnsAsync(address);

            // Act
            var result = await _controller.UpdateAddress(id, address);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateAddress_ReturnsBadRequest_WhenIdDoesNotMatchAddressId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var address = new AddressModel { AddressId = Guid.NewGuid() };

            // Act
            var result = await _controller.UpdateAddress(id, address);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteAddress_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAddressCommand>(), default)).Returns(Task.FromResult(MediatR.Unit.Value));

            // Act
            var result = await _controller.DeleteAddress(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}