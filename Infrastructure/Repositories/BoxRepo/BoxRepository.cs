using Domain.Models.Box;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.BoxRepo
{
    public class BoxRepository : IBoxRepository
    {
        private readonly MSB_Database _database;
        public BoxRepository(MSB_Database mSB_database)
        {
            _database = mSB_database;
        }

        public async Task<BoxModel> AddBoxAsync(BoxModel box, Guid shelfId)
        {
            var shelf = await _database.Shelves.FindAsync(shelfId);
            if (shelf != null)
            {
                box.ShelfId = shelfId;
                _database.Boxes.AddAsync(box);
                await _database.SaveChangesAsync();
            }
            return box;
        }



        async Task IBoxRepository.DeleteBoxAsync(Guid boxId)
        {
            var box = await _database.Boxes.FindAsync(boxId);
            if (box != null)
            {
                _database.Boxes.Remove(box);
                await _database.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<BoxModel>> IBoxRepository.GetAllBoxesAsync()
        {
            return await _database.Boxes.ToListAsync();
        }

        async Task<BoxModel> IBoxRepository.GetBoxByIdAsync(Guid boxId)
        {
            return await _database.Boxes.FindAsync(boxId);
        }

        async Task<BoxModel> IBoxRepository.UpdateBoxAsync(BoxModel box)
        {
            _database.Boxes.Update(box);
            _database.SaveChangesAsync();

            return box;
        }
    }
}