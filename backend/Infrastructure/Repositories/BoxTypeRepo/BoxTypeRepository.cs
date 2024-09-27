using Domain.Models.BoxType;
using Infrastructure.Database;
using Infrastructure.Repositories.BoxTypeRepo;
using Microsoft.EntityFrameworkCore;

// This class implements the IBoxRepository interface and provides methods for interacting with the BoxModel entities in the MSB_Database.
// The class includes methods to:
// - Add a new box asynchronously with AddBoxAsync(BoxModel box)
// - Delete a box by ID asynchronously with DeleteBoxAsync(Guid boxId)
// - Retrieve all boxes asynchronously with GetAllBoxesAsync()
// - Retrieve a specific box by ID asynchronously with GetBoxByIdAsync(Guid boxId)
// - Update an existing box asynchronously with UpdateBoxAsync(BoxModel box)
// The class leverages Entity Framework Core for database operations and ensures changes are saved asynchronously to the database.

namespace Infrastructure.Repositories.BoxTypeRepo
{
    public class BoxTypeRepository : IBoxTypeRepository
    {
        private readonly MSB_Database _context;

        public BoxTypeRepository(MSB_Database context)
        {
            _context = context;
        }

        public async Task<BoxTypeModel> AddBoxTypeAsync(BoxTypeModel boxType)
        {
            // Assuming your DB context has a DbSet<BoxTypeModel>
            await _context.BoxTypes.AddAsync(boxType);
            await _context.SaveChangesAsync();
            return boxType;
        }

        public async Task<IEnumerable<BoxTypeModel>> GetAllBoxTypesAsync()
        {
            return await _context.BoxTypes.ToListAsync();
        }

        public async Task<BoxTypeModel> UpdateBoxTypeAsync(BoxTypeModel box)
        {
            _context.BoxTypes.Update(box);
            await _context.SaveChangesAsync();
            return box;
        }

        public async Task DeleteBoxTypeAsync(int boxTypeId)
        {
            var boxType = await _context.BoxTypes.FindAsync(boxTypeId);
            if (boxType != null)
            {
                _context.BoxTypes.Remove(boxType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<BoxTypeModel> GetBoxTypeByIdAsync(int boxTypeId)
        {
            return await _context.BoxTypes.FindAsync(boxTypeId);
        }
        
        public async Task<List<string>> GetAllBoxSizesAsync()
        {
            return await _context.BoxTypes
                .Select(bt => bt.Size)   // Select the size property
                .Distinct()              // Ensure distinct sizes
                .ToListAsync();          // Convert to a list
        }
    }
}