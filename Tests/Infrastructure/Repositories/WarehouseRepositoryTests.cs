using Domain.Models.Warehouse;
using Infrastructure.Database;
using Infrastructure.Repositories.WarehouseRepo;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure.Repositories
{
    public class WarehouseRepositoryTests
    {
        [Fact]
        public async Task GetAllWarehousesAsync_ReturnsAllWarehouses()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var warehouses = new List<WarehouseModel>
            {
                new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "TestWarehouse1" },
                new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "TestWarehouse2" },
            };
            using (var context = new MSB_Database(options))
            {
                context.Warehouses.AddRange(warehouses);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var warehouseRepository = new WarehouseRepository(context);

                // Act
                var result = await warehouseRepository.GetAllWarehousesAsync();

                // Assert
                Assert.Equal(warehouses.Count, result.Count());
            }
        }

        [Fact]
        public async Task GetWarehouseByIdAsync_ReturnsWarehouse_WhenWarehouseExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var warehouseId = Guid.NewGuid();
            var expectedWarehouse = new WarehouseModel { WarehouseId = warehouseId, WarehouseName = "TestWarehouse" };
            using (var context = new MSB_Database(options))
            {
                context.Warehouses.Add(expectedWarehouse);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var warehouseRepository = new WarehouseRepository(context);

                // Act
                var result = await warehouseRepository.GetWarehouseByIdAsync(warehouseId);

                // Assert
                Assert.Equal(expectedWarehouse.WarehouseId, result.WarehouseId);
            }
        }

        [Fact]
        public async Task AddWarehouseAsync_AddsWarehouseToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var warehouse = new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "TestWarehouse" };
            using (var context = new MSB_Database(options))
            {
                var warehouseRepository = new WarehouseRepository(context);

                // Act
                var result = await warehouseRepository.AddWarehouseAsync(warehouse);

                // Assert
                Assert.Equal(warehouse.WarehouseId, result.WarehouseId);
                Assert.Single(context.Warehouses);
            }
        }

        [Fact]
        public async Task UpdateWarehouseAsync_UpdatesWarehouseInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var warehouseId = Guid.NewGuid();
            var originalWarehouse = new WarehouseModel { WarehouseId = warehouseId, WarehouseName = "OriginalWarehouse" };
            var updatedWarehouse = new WarehouseModel { WarehouseId = warehouseId, WarehouseName = "UpdatedWarehouse" };
            using (var context = new MSB_Database(options))
            {
                context.Warehouses.Add(originalWarehouse);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var warehouseRepository = new WarehouseRepository(context);

                // Act
                var result = await warehouseRepository.UpdateWarehouseAsync(updatedWarehouse);

                // Assert
                Assert.Equal(updatedWarehouse.WarehouseId, result.WarehouseId);
                Assert.Equal(updatedWarehouse.WarehouseName, context.Warehouses.Single().WarehouseName);
            }
        }

        [Fact]
        public async Task DeleteWarehouseAsync_DeletesWarehouseFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MSB_Database>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var warehouseId = Guid.NewGuid();
            var warehouse = new WarehouseModel { WarehouseId = warehouseId, WarehouseName = "TestWarehouse" };
            using (var context = new MSB_Database(options))
            {
                context.Warehouses.Add(warehouse);
                await context.SaveChangesAsync();
            }
            using (var context = new MSB_Database(options))
            {
                var warehouseRepository = new WarehouseRepository(context);

                // Act
                var result = await warehouseRepository.DeleteWarehouseAsync(warehouseId);

                // Assert
                Assert.True(result != null);
                Assert.Empty(context.Warehouses);
            }
        }
    }
}
