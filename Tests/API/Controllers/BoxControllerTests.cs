using API.Controllers.BoxController;
using Application.Commands.Box.AddBox;
using Application.Commands.Box.DeleteBox;
using Application.Commands.Box.UpdateBox;
using Application.Dto.Box;
using Application.Queries.Box.GetAll;
using Application.Queries.Box.GetByID;
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

        public BoxControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<BoxController>>();
            _validatorMock = new Mock<IValidator<BoxDto>>();
            _controller = new BoxController(_mediatorMock.Object, _loggerMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task AddBox_ReturnsCreatedAtAction_WhenBoxIsCreated()
        {
            // Arrange
            var boxDto = new BoxDto { BoxId = Guid.NewGuid() };
            var shelfId = Guid.NewGuid();
            var command = new AddBoxCommand(boxDto, shelfId);
            var box = new BoxModel { BoxId = boxDto.BoxId };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(box);

            var validationResult = new ValidationResult();
            _validatorMock.Setup(v => v.Validate(boxDto)).Returns(validationResult);

            // Act
            var result = await _controller.AddBox(command);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<BoxModel>(createdAtActionResult.Value);
            Assert.Equal(box.BoxId, returnValue.BoxId);
        }

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