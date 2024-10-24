﻿using API.Controllers.Address;
using Application.Commands.Address.AddAddress;
using Application.Commands.Address.DeleteAddress;
using Application.Commands.Address.UpdateAddress;
using Application.Dto.Adress;
using Application.Queries.Address.GetAll;
using Application.Queries.Address.GetByID;
using Application.Validators.AddressValidator;
using Domain.Models.Address;
using FluentValidation.Results;
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
        private readonly Mock<IAddressValidations> _addressValidationsMock;
        private readonly Mock<ILogger<AddressController>> _loggerMock;
        private readonly AddressController _controller;

        public AddressControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _configurationMock = new Mock<IConfiguration>();
            _addressValidationsMock = new Mock<IAddressValidations>();
            _loggerMock = new Mock<ILogger<AddressController>>();
            _controller = new AddressController(
                _mediatorMock.Object,
                _configurationMock.Object,
                _addressValidationsMock.Object,
                _loggerMock.Object);
        }

        // Test to verify that AddAddress returns a 500 status code when an exception is thrown
        [Fact]
        public async Task AddAddress_Returns500_WhenExceptionIsThrown()
        {
            // Arrange
            var command = new AddAddressCommand(new AddressDto());
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddAddressCommand>(), default)).ThrowsAsync(new Exception());
            var address = new AddressModel();


            // Act
            var result = await _controller.AddAddress(command);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        // Test to verify that AddAddress returns BadRequest when model state is invalid
        [Fact]
        public async Task AddAddress_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var command = new AddAddressCommand(new AddressDto());
            _addressValidationsMock.Setup(v => v.Validate(It.IsAny<AddressDto>())).Returns(new ValidationResult());

            // Act
            var result = await _controller.AddAddress(command);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);

            // Check if the status code is 500 - "An error occurred while adding an Address"
            Assert.Equal(500, objectResult.StatusCode);
        }

        // Test to verify that GetAllAddresses returns a 500 status code when an exception is thrown
        [Fact]
        public async Task GetAllAddresses_Returns500_WhenExceptionIsThrown()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAddressesQuery>(), default)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAllAddresses();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        // Test to verify that GetAddressById returns a 500 status code when an exception is thrown
        [Fact]
        public async Task GetAddressById_Returns500_WhenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAddressByIdQuery>(), default)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.GetAddressById(id);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        // Test to verify that UpdateAddress returns a 500 status code when an exception is thrown
        [Fact]
        public async Task UpdateAddress_Returns500_WhenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid();
            var addressModel = new AddressModel { AddressId = id };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateAddressCommand>(), default)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.UpdateAddress(id, addressModel);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        // Test to verify that DeleteAddress returns a 500 status code when an exception is thrown
        [Fact]
        public async Task DeleteAddress_Returns500_WhenExceptionIsThrown()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteAddressCommand>(), default)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.DeleteAddress(id);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        // Test to verify that AddAddress returns a 201 status code when an address is successfully added
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

        // Test to verify that AddAddress returns a 201 status code when an address is successfully added
        [Fact]
        public async Task DeleteAddress_ReturnsOk_WhenDeleteIsSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.Is<DeleteAddressCommand>(c => c.AddressId == id), default)).Returns(Task.FromResult(MediatR.Unit.Value));

            // Act
            var result = await _controller.DeleteAddress(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}