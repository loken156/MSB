using API.Controllers.Warehouse;
using Application.Commands.Warehouse.AddWarehouse;
using Application.Commands.Warehouse.DeleteWarehouse;
using Application.Commands.Warehouse.UpdateWarehouse;
using Application.Dto.AddWarehouse;
using Application.Dto.Warehouse;
using Application.Queries.Warehouse.GetAll;
using Application.Queries.Warehouse.GetByID;
using AutoMapper;
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
        private readonly Mock<IMapper> _mapperMock;

        public WarehouseControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<WarehouseController>>();
            _validationsMock = new Mock<IValidator<AddWarehouseDto>>();
            _mapperMock = new Mock<IMapper>();
            _controller = new WarehouseController(_mediatorMock.Object, _loggerMock.Object, _validationsMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void AddWarehouse_ReturnsCreatedAtActionResult_WhenWarehouseIsSuccessfullyAdded()
        {
            // Arrange
            var newWarehouseDto = new AddWarehouseDto
            {
                WarehouseName = "Test Warehouse",
                AddressId = System.Guid.NewGuid()
            };

            var addWarehouseCommand = new AddWarehouseCommand(newWarehouseDto);

            var validationResult = new ValidationResult();

            _validationsMock.Setup(v => v.Validate(newWarehouseDto)).Returns(validationResult);

            var warehouseModel = new WarehouseModel
            {
                WarehouseId = System.Guid.NewGuid(),
                WarehouseName = "Test Warehouse",
                AddressId = newWarehouseDto.AddressId,
                Shelves = new List<ShelfModel>()
            };

            _mediatorMock.Setup(m => m.Send(addWarehouseCommand, It.IsAny<CancellationToken>())).ReturnsAsync(warehouseModel);

            var warehouseDto = new WarehouseDto
            {
                WarehouseId = warehouseModel.WarehouseId,
                WarehouseName = warehouseModel.WarehouseName,
                AddressId = warehouseModel.AddressId,
                ShelfIds = warehouseModel.Shelves.Select(shelf => shelf.ShelfId).ToList()
            };

            _mapperMock.Setup(m => m.Map<WarehouseDto>(warehouseModel)).Returns(warehouseDto);

            // Act
            var result = await _controller.AddWarehouse(addWarehouseCommand);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedWarehouseDto = Assert.IsType<WarehouseDto>(createdAtActionResult.Value);
            Assert.Equal(warehouseDto, returnedWarehouseDto);
        }

        [Fact]
        public async void GetAllWarehouses_ReturnsOk_WhenWarehousesExist()
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

        [Fact]
        public async void GetWarehouseById_ReturnsOk_WhenWarehouseExists()
        {
            // Arrange
            var id = System.Guid.NewGuid();
            var warehouse = new WarehouseModel { WarehouseId = id };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetWarehouseByIdQuery>(), default)).ReturnsAsync(warehouse);

            var warehouseDto = new WarehouseDto { WarehouseId = id };
            _mapperMock.Setup(m => m.Map<WarehouseDto>(warehouse)).Returns(warehouseDto);

            // Act
            var result = await _controller.GetWarehouseById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<WarehouseDto>(okResult.Value);
            Assert.Equal(warehouseDto, returnValue);
        }

        [Fact]
        public async void GetWarehouseById_ReturnsNotFound_WhenWarehouseDoesNotExist()
        {
            // Arrange
            var id = System.Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetWarehouseByIdQuery>(), default)).ReturnsAsync((WarehouseModel)null);

            // Act
            var result = await _controller.GetWarehouseById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void UpdateWarehouse_ReturnsOk_WhenWarehouseIsUpdated()
        {
            // Arrange
            var warehouseDto = new WarehouseDto { WarehouseId = System.Guid.NewGuid(), WarehouseName = "Test Warehouse" };
            var command = new UpdateWarehouseCommand(warehouseDto);
            var warehouse = new WarehouseModel { WarehouseId = warehouseDto.WarehouseId };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(warehouse);

            _mapperMock.Setup(m => m.Map<WarehouseDto>(warehouse)).Returns(warehouseDto);

            // Act
            var result = await _controller.UpdateWarehouse(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<WarehouseDto>(okResult.Value);
            Assert.Equal(warehouseDto, returnValue);
        }

        [Fact]
        public async void DeleteWarehouse_ReturnsNoContent_WhenWarehouseIsDeleted()
        {
            // Arrange
            var id = System.Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteWarehouseCommand>(), default)).ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.DeleteWarehouse(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}