using API.Controllers.Warehouse;
using Application.Commands.Warehouse.AddWarehouse;
using Application.Commands.Warehouse.DeleteWarehouse;
using Application.Commands.Warehouse.UpdateWarehouse;
using Application.Dto.AddWarehouse;
using Application.Dto.Warehouse;
using Application.Queries.Warehouse.GetAll;
using Application.Queries.Warehouse.GetByID;
using Domain.Models.Warehouse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.API.Controllers
{
    public class WarehouseControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly WarehouseController _controller;

        public WarehouseControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new WarehouseController(_mediatorMock.Object);
        }

        [Fact]
        public async Task AddWarehouse_ReturnsCreatedAtAction_WhenWarehouseIsCreated()
        {
            // Arrange
            var addWarehouseDto = new AddWarehouseDto { WarehouseName = "Test Warehouse", AddressId = 1, ShelfId = 1 };
            var command = new AddWarehouseCommand(addWarehouseDto);
            var warehouse = new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "Test Warehouse" }; // Removed AddressId and ShelfId
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(warehouse);

            // Act
            var result = await _controller.AddWarehouse(command);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<AddWarehouseDto>(createdAtActionResult.Value);
            Assert.Equal(addWarehouseDto.WarehouseName, returnValue.WarehouseName);
            Assert.Equal(addWarehouseDto.AddressId, returnValue.AddressId);
            Assert.Equal(addWarehouseDto.ShelfId, returnValue.ShelfId);
        }





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

        [Fact]
        public async Task GetWarehouseById_ReturnsNotFound_WhenWarehouseDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetWarehouseByIdQuery>(), default)).ReturnsAsync((WarehouseModel)null);

            // Act
            var result = await _controller.GetWarehouseById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

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
