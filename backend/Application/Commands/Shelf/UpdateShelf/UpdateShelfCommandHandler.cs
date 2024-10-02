using Domain.Models.Shelf;
using Infrastructure.Repositories.ShelfRepo;
using MediatR;

// This class resides in the Application layer and handles the command to update a shelf. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the shelf repository in the Infrastructure layer to retrieve the 
// shelf entity based on the provided ShelfId. It constructs a new ShelfModel object with the 
// updated properties provided in the UpdateShelfCommand. After updating the shelf in the database, 
// it returns the updated shelf model.

namespace Application.Commands.Shelf.UpdateShelf
{
    public class UpdateShelfCommandHandler : IRequestHandler<UpdateShelfCommand, ShelfModel>
    {
        private readonly IShelfRepository _shelfRepository;
        public UpdateShelfCommandHandler(IShelfRepository shelfRepository)
        {
            _shelfRepository = shelfRepository;
        }
        public async Task<ShelfModel> Handle(UpdateShelfCommand request, CancellationToken cancellationToken)
        {
            ShelfModel shelfToUpdate = new ShelfModel
            {
                ShelfId = request.UpdatedShelf.ShelfId,
                ShelfRows = request.UpdatedShelf.ShelfRows,
                ShelfColumn = request.UpdatedShelf.ShelfColumn,
                Occupancy = request.UpdatedShelf.Occupancy,
                WarehouseId = request.UpdatedShelf.WarehouseId,
                // BoxId = request.UpdatedShelf.BoxId, // Pending implementation
                // ItemId = request.UpdatedShelf.ItemId, // Pending implementation

            };

            return await _shelfRepository.UpdateShelfAsync(shelfToUpdate);
        }
    }
}