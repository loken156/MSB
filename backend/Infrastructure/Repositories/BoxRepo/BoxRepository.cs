using Domain.Models.Box;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

// This class implements the IBoxRepository interface and provides methods for interacting with the BoxModel entities in the MSB_Database.
// The class includes methods to:
// - Add a new box asynchronously with AddBoxAsync(BoxModel box)
// - Delete a box by ID asynchronously with DeleteBoxAsync(Guid boxId)
// - Retrieve all boxes asynchronously with GetAllBoxesAsync()
// - Retrieve a specific box by ID asynchronously with GetBoxByIdAsync(Guid boxId)
// - Update an existing box asynchronously with UpdateBoxAsync(BoxModel box)
// The class leverages Entity Framework Core for database operations and ensures changes are saved asynchronously to the database.

namespace Infrastructure.Repositories.BoxRepo
{
    public class BoxRepository : IBoxRepository
    {
        private readonly MSB_Database _database;
        public BoxRepository(MSB_Database mSB_database)
        {
            _database = mSB_database;
        }

        public async Task<BoxModel> AddBoxAsync(BoxModel box)
        {
            await _database.Boxes.AddAsync(box);
            await _database.SaveChangesAsync();
            return box;
        }

        public async Task DeleteBoxAsync(BoxModel box)
        {
            _database.Boxes.Remove(box);
            await _database.SaveChangesAsync();  // Save the changes after deletion
        }

        public async Task<IEnumerable<BoxModel>> GetAllBoxesAsync()
        {
            return await _database.Boxes.ToListAsync();
        }

        public async Task<BoxModel> GetBoxByIdAsync(Guid boxId)
        {
            return await _database.Boxes.FindAsync(boxId);
        }

        public async Task<BoxModel> UpdateBoxAsync(BoxModel box)
        {
            _database.Boxes.Update(box);
            await _database.SaveChangesAsync();
            return box;
        }
    }

}