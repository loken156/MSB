﻿using Application.Commands.Warehouse.DeleteWarehouse;
using Infrastructure.Repositories.WarehouseRepo;
using MediatR;
using Moq;

namespace Tests.Application.Warehouse.CommandHandlers
{
    public class DeleteWarehouseCommandHandlerTests
    {
        // Test to verify that DeleteWarehouse calls DeleteWarehouseAsync on repository
        [Fact]
        public async Task Handle_GivenValidCommand_CallsDeleteWarehouseAsyncOnRepository()
        {
            // Arrange
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            var handler = new DeleteWarehouseCommandHandler(mockWarehouseRepository.Object);
            var command = new DeleteWarehouseCommand(Guid.NewGuid());

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockWarehouseRepository.Verify(repo => repo.DeleteWarehouseAsync(command.WarehouseId), Times.Once);
        }

        // Test to verify that DeleteWarehouse returns Unit value
        [Fact]
        public async Task Handle_GivenValidCommand_ReturnsUnitValue()
        {
            // Arrange
            var mockWarehouseRepository = new Mock<IWarehouseRepository>();
            var handler = new DeleteWarehouseCommandHandler(mockWarehouseRepository.Object);
            var command = new DeleteWarehouseCommand(Guid.NewGuid());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
        }
    }
}