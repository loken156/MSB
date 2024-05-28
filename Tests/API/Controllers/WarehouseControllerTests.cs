using API.Controllers.Warehouse;
using Application.Commands.Warehouse.AddWarehouse;
using Application.Commands.Warehouse.DeleteWarehouse;
using Application.Commands.Warehouse.UpdateWarehouse;
using Application.Dto.AddWarehouse;
using Application.Dto.Warehouse;
using Application.Queries.Warehouse.GetAll;
using Application.Queries.Warehouse.GetByID;
using Domain.Models.Shelf;
using Domain.Models.Warehouse;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.API.Controllers
{
    public class WarehouseControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly WarehouseController _controller;
        private readonly Mock<ILogger<WarehouseController>> _loggerMock;
        private readonly Mock<IValidator<AddWarehouseDto>> _validationsMock;

        public WarehouseControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<WarehouseController>>();
            _validationsMock = new Mock<IValidator<AddWarehouseDto>>();
            _controller = new WarehouseController(_mediatorMock.Object, _loggerMock.Object, _validationsMock.Object);
        }

        // Test to verify that AddWarehouse returns CreatedAtActionResult when warehouse is created
        [Fact]
        public async Task AddWarehouse_ReturnsCreatedAtActionResult_WhenWarehouseIsSuccessfullyAdded()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<WarehouseController>>();
            var validatorMock = new Mock<IValidator<AddWarehouseDto>>();
            var warehouseController = new WarehouseController(mediatorMock.Object, loggerMock.Object, validatorMock.Object);

            var newWarehouseDto = new AddWarehouseDto
            {
                WarehouseName = "Test Warehouse",
                AddressId = Guid.NewGuid()
            };

            var addWarehouseCommand = new AddWarehouseCommand(newWarehouseDto);

            var validationResult = new ValidationResult();

            validatorMock.Setup(v => v.Validate(newWarehouseDto)).Returns(validationResult);

            var warehouseModel = new WarehouseModel
            {
                WarehouseId = Guid.NewGuid(),
                WarehouseName = "Test Warehouse",
                AddressId = newWarehouseDto.AddressId,
                Shelves = new List<ShelfModel>()
            };

            mediatorMock.Setup(m => m.Send(addWarehouseCommand, It.IsAny<CancellationToken>())).ReturnsAsync(warehouseModel);

            // Act
            var result = await warehouseController.AddWarehouse(addWarehouseCommand);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedWarehouseDto = Assert.IsType<WarehouseDto>(createdAtActionResult.Value);
            Assert.Equal(warehouseModel.WarehouseId, returnedWarehouseDto.WarehouseId);
            Assert.Equal(warehouseModel.WarehouseName, returnedWarehouseDto.WarehouseName);
            Assert.Equal(warehouseModel.AddressId, returnedWarehouseDto.AddressId);
            Assert.Equal(warehouseModel.Shelves.Select(shelf => shelf.ShelfId), returnedWarehouseDto.ShelfIds);
        }

        // Test to verify that AddWarehouse returns BadRequest when model state is invalid
        [Fact]
        public async Task GetAllWarehouses_ReturnsOk_WhenWarehousesExist()
        {
            // Arrange
            var warehouses = new List<WarehouseModel> { new WarehouseModel(), new WarehouseModel() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllWarehousesQuery>(), default)).ReturnsAsync(warehouses);

            // Act
            var result = await _controller.GetAllWarehouses();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<WarehouseDto>>(okResult.Value);
            Assert.Equal(warehouses.Count, returnValue.Count());
        }

        // Test to verify that GetWarehouseById returns Ok when warehouse exists
        [Fact]
        public async Task GetWarehouseById_ReturnsOk_WhenWarehouseExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var warehouse = new WarehouseModel { WarehouseId = id };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetWarehouseByIdQuery>(), default)).ReturnsAsync(warehouse);

            // Act
            var result = await _controller.GetWarehouseById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<WarehouseDto>(okResult.Value);
            Assert.Equal(warehouse.WarehouseId, returnValue.WarehouseId);
        }

        // Test to verify that GetWarehouseById returns NotFound when warehouse does not exist
        [Fact]
        public async Task GetWarehouseById_ReturnsNotFound_WhenWarehouseDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetWarehouseByIdQuery>(), default)).Returns(Task.FromResult<WarehouseModel>(null));


            // Act
            var result = await _controller.GetWarehouseById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Test to verify that UpdateWarehouse returns Ok when warehouse is updated
        [Fact]
        public async Task UpdateWarehouse_ReturnsOk_WhenWarehouseIsUpdated()
        {
            // Arrange
            var warehouseDto = new WarehouseDto { WarehouseId = Guid.NewGuid(), WarehouseName = "Test Warehouse" };
            var command = new UpdateWarehouseCommand(warehouseDto);
            var warehouse = new WarehouseModel { WarehouseId = warehouseDto.WarehouseId };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(warehouse);

            // Act
            var result = await _controller.UpdateWarehouse(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<WarehouseDto>(okResult.Value);
            Assert.Equal(warehouse.WarehouseId, returnValue.WarehouseId);
        }

        // Test to verify that UpdateWarehouse returns BadRequest when model state is invalid
        [Fact]
        public async Task DeleteWarehouse_ReturnsNoContent_WhenWarehouseIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteWarehouseCommand>(), default)).Returns(Task.FromResult(MediatR.Unit.Value));

            // Act
            var result = await _controller.DeleteWarehouse(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}