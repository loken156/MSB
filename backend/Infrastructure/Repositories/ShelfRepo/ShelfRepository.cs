using Domain.Models.Shelf;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ShelfRepo
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly MSB_Database _msbDatabase;

        public ShelfRepository(MSB_Database msbDatabase)
        {
            _msbDatabase = msbDatabase;
        }

        // Add Shelf and initialize available slots
        public async Task<ShelfModel> AddShelfAsync(ShelfModel shelfToCreate)
        {
            // Initialize available slots based on shelf capacity
            shelfToCreate.InitializeAvailableSlots();
            _msbDatabase.Shelves.Add(shelfToCreate);
            await _msbDatabase.SaveChangesAsync();
            return shelfToCreate;
        }

        // Delete a Shelf
        public async Task DeleteShelfAsync(Guid shelfId)
        {
            var shelf = await _msbDatabase.Shelves.FindAsync(shelfId);
            if (shelf != null)
            {
                _msbDatabase.Shelves.Remove(shelf);
                await _msbDatabase.SaveChangesAsync();
            }
        }

        // Get all Shelves, including Boxes and their BoxType
        public async Task<IEnumerable<ShelfModel>> GetAllAsync()
        {
            return await _msbDatabase.Shelves
                .Include(s => s.Boxes) // Load related Boxes
                .ThenInclude(b => b.BoxType) // Load BoxType for each Box
                .ToListAsync();
        }

        // Get a specific shelf along with its boxes and their types
        public async Task<ShelfModel> GetShelfWithBoxesAsync(Guid shelfId)
        {
            var shelf = await _msbDatabase.Shelves
                .Include(s => s.Boxes)  // Load related Boxes
                .ThenInclude(b => b.BoxType) // Load BoxType for each Box
                .FirstOrDefaultAsync(s => s.ShelfId == shelfId);

            if (shelf == null)
            {
                throw new InvalidOperationException("Shelf not found");
            }

            // Recalculate available slots based on the current state of the shelf
            UpdateAvailableSlots(shelf);

            return shelf;
        }

        // Update Shelf, including recalculating available slots
        public async Task<ShelfModel> UpdateShelfAsync(ShelfModel shelfToUpdate)
        {
            // Recalculate available slots before updating
            UpdateAvailableSlots(shelfToUpdate);

            _msbDatabase.Entry(shelfToUpdate).State = EntityState.Modified;
            await _msbDatabase.SaveChangesAsync();
            return shelfToUpdate;
        }

        // Recalculate available slots based on the boxes currently on the shelf
        private void UpdateAvailableSlots(ShelfModel shelf)
        {
            // Reset the available slots to capacity
            shelf.AvailableLargeSlots = shelf.LargeBoxCapacity;
            shelf.AvailableMediumSlots = shelf.MediumBoxCapacity;
            shelf.AvailableSmallSlots = shelf.SmallBoxCapacity;

            // Decrease available slots based on the number of boxes currently in the shelf
            foreach (var box in shelf.Boxes)
            {
                var boxSize = box.BoxType.Size;
                if (boxSize == "Large")
                {
                    shelf.AvailableLargeSlots--;
                }
                else if (boxSize == "Medium")
                {
                    shelf.AvailableMediumSlots--;
                }
                else if (boxSize == "Small")
                {
                    shelf.AvailableSmallSlots--;
                }
            }
        }
    }
}
