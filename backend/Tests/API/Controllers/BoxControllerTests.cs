﻿using API.Controllers.BoxController;
using Application.Commands.Box.AddBox;
using Application.Commands.Box.DeleteBox;
using Application.Commands.Box.UpdateBox;
using Application.Dto.Box;
using Application.Queries.Box.GetAll;
using Application.Queries.Box.GetByID;
using AutoMapper;
using Domain.Models.Box;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.API.Controllers
{
    public class BoxControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BoxController _controller;
        private readonly Mock<ILogger<BoxController>> _loggerMock;
        private readonly Mock<IValidator<BoxDto>> _validatorMock;
        private readonly Mock<IMapper> _mapperMock;

        public BoxControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<BoxController>>();
            _validatorMock = new Mock<IValidator<BoxDto>>();
            _mapperMock = new Mock<IMapper>();
            _controller = new BoxController(_mediatorMock.Object, _loggerMock.Object, _validatorMock.Object, _mapperMock.Object);

            // General setup for IMediator to return a BoxDto asynchronously, if needed
            _mediatorMock.Setup(m => m.Send(It.IsAny<IRequest<BoxDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BoxDto { /* Initialize properties as needed */ });
        }

        // Test to verify that AddBox returns CreatedAtAction when box is created
        [Fact]
        public async Task AddBox_ReturnsCreatedAtAction_WhenBoxIsCreated()
        {
            // Arrange
            var boxDto = new BoxDto { BoxId = Guid.NewGuid() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddBoxCommand>(), default)).ReturnsAsync(boxDto);
            _mapperMock.Setup(m => m.Map<BoxDto>(It.IsAny<BoxModel>())).Returns(boxDto);

            var validationResult = new ValidationResult();
            _validatorMock.Setup(v => v.Validate(boxDto)).Returns(validationResult);

            // Act
            var result = await _controller.AddBox(boxDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<BoxDto>(createdAtActionResult.Value);
            Assert.Equal(boxDto.BoxId, returnValue.BoxId);
        }

        // Test to verify that AddBox returns BadRequest when model state is invalid
        [Fact]
        public async Task GetAllBoxes_ReturnsOk_WhenBoxesExist()
        {
            // Arrange
            var boxes = new List<BoxModel> { new BoxModel(), new BoxModel() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBoxesQuery>(), default)).ReturnsAsync(boxes);

            // Act
            var result = await _controller.GetAllBoxes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<BoxDto>>(okResult.Value);
            Assert.Equal(boxes.Count, returnValue.Count());
        }

        // Test to verify that GetBoxById returns Ok when box exists
        [Fact]
        public async Task GetBoxById_ReturnsOk_WhenBoxExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var box = new BoxModel { BoxId = id };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBoxByIdQuery>(), default)).ReturnsAsync(box);

            // Act
            var result = await _controller.GetBoxById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<BoxDto>(okResult.Value);
            Assert.Equal(box.BoxId, returnValue.BoxId);
        }

        // Test to verify that GetBoxById returns NotFound when box does not exist
        [Fact]
        public async Task GetBoxById_ReturnsNotFound_WhenBoxDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBoxByIdQuery>(), default)).ReturnsAsync((BoxModel)null);

            // Act
            var result = await _controller.GetBoxById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        // Test to verify that UpdateBox returns NoContent when box is updated
        [Fact]
        public async Task UpdateBox_ReturnsNoContent_WhenBoxIsUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var boxDto = new BoxDto { BoxId = id };
            var box = new BoxModel { BoxId = id };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBoxCommand>(), default)).ReturnsAsync(box);

            // Act
            var result = await _controller.UpdateBox(id, boxDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        // Test to verify that DeleteBox returns NoContent when box is deleted
        [Fact]
        public async Task DeleteBox_ReturnsNoContent_WhenBoxIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBoxCommand>(), default)).Returns(Task.FromResult(MediatR.Unit.Value));

            // Act
            var result = await _controller.DeleteBox(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}