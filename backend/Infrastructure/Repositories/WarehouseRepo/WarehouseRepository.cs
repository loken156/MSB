using Domain.Models.Warehouse;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

// This class implements the IWarehouseRepository interface and provides methods for managing WarehouseModel entities in the MSB_Database.
// The class includes methods to:
// - Add a new warehouse asynchronously with AddWarehouseAsync(WarehouseModel warehouse)
// - Delete a warehouse asynchronously with DeleteWarehouseAsync(Guid id)
// - Check if a warehouse exists asynchronously with ExistWarehouseAsync(Guid warehouseId)
// - Retrieve all warehouses asynchronously with GetAllWarehousesAsync()
// - Retrieve a warehouse by ID asynchronously with GetWarehouseByIdAsync(Guid id)
// - Retrieve a warehouse by name asynchronously with GetWarehouseByNameAsync(string warehouseName)
// - Update a warehouse asynchronously with UpdateWarehouseAsync(WarehouseModel warehouse)
// Entity Framework Core is used for database operations, ensuring asynchronous save changes to the database.

namespace Infrastructure.Repositories.WarehouseRepo
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly MSB_Database _database;
        public WarehouseRepository(MSB_Database mSB_Database)
        {
            _database = mSB_Database;
        }

        public async Task<WarehouseModel> AddWarehouseAsync(WarehouseModel warehouse)
        {
            await _database.Warehouses.AddAsync(warehouse);
            await _database.SaveChangesAsync();

            return await Task.FromResult(warehouse);
        }
        public async Task<WarehouseModel> DeleteWarehouseAsync(Guid id)
        {
            var warehouse = await _database.Warehouses.FindAsync(id);
            if (warehouse != null)
            {
                _database.Warehouses.Remove(warehouse);
                await _database.SaveChangesAsync();
            }
            return warehouse;
        }

        public async Task<bool> ExistWarehouseAsync(Guid warehouseId)
        {
            return await _database.Warehouses.AnyAsync(w => w.WarehouseId == warehouseId);
        }

        public async Task<IEnumerable<WarehouseModel>> GetAllWarehousesAsync()
        {
            return await _database.Warehouses.Include(w => w.Address).ToListAsync();
        }

        public async Task<WarehouseModel> GetWarehouseByIdAsync(Guid id)
        {
            return await _database.Warehouses.Include(w => w.Address).FirstOrDefaultAsync(w => w.WarehouseId == id);
        }

        public async Task<WarehouseModel?> GetWarehouseByNameAsync(string warehouseName)
        {
            return await _database.Warehouses.FirstOrDefaultAsync(w => w.WarehouseName == warehouseName);
        }

        public async Task<WarehouseModel> UpdateWarehouseAsync(WarehouseModel warehouse)
        {
            _database.Entry(warehouse).State = EntityState.Modified;
            await _database.SaveChangesAsync();
            return warehouse;
        }
    }
}