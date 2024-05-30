using Domain.Models.Shelf;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

// This class implements the IShelfRepository interface and provides methods for managing ShelfModel entities in the MSB_Database.
// The class includes methods to:
// - Retrieve all shelves asynchronously with GetAllAsync()
// - Retrieve a specific shelf by ID asynchronously with GetShelfByIdAsync(Guid shelfId)
// - Create a new shelf asynchronously with AddShelfAsync(ShelfModel shelfToCreate)
// - Update an existing shelf asynchronously with UpdateShelfAsync(ShelfModel shelfToUpdate)
// - Delete a shelf asynchronously with DeleteShelfAsync(Guid shelfId)
// Entity Framework Core is used for database operations, ensuring asynchronous save changes to the database.

namespace Infrastructure.Repositories.ShelfRepo
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly MSB_Database _msbDatabase;
        public ShelfRepository(MSB_Database msbDatabase)
        {
            _msbDatabase = msbDatabase;
        }

        public async Task<ShelfModel> AddShelfAsync(ShelfModel shelfToCreate)
        {
            _msbDatabase.Shelves.Add(shelfToCreate);
            await _msbDatabase.SaveChangesAsync();
            return shelfToCreate;
        }

        public async Task DeleteShelfAsync(Guid shelfId)
        {
            var shelf = await _msbDatabase.Shelves.FindAsync(shelfId);
            if (shelf != null)
            {
                _msbDatabase.Shelves.Remove(shelf);
                await _msbDatabase.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ShelfModel>> GetAllAsync()
        {
            return await _msbDatabase.Shelves.ToListAsync();
        }

        public async Task<ShelfModel> GetShelfByIdAsync(Guid shelfId)
        {
            return await _msbDatabase.Shelves.FindAsync(shelfId);
        }

        public async Task<ShelfModel> UpdateShelfAsync(ShelfModel shelfToUpdate)
        {
            _msbDatabase.Entry(shelfToUpdate).State = EntityState.Modified;
            await _msbDatabase.SaveChangesAsync();
            return shelfToUpdate;
        }
    }
}