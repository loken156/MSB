using Domain.Models.Shelf;
using Infrastructure.Repositories.ShelfRepo;
using Infrastructure.Repositories.WarehouseRepo;
using MediatR;

namespace Application.Commands.Shelf.AddShelf
{
    public class AddShelfCommandHandler : IRequestHandler<AddShelfCommand, ShelfModel>
    {
        private readonly IShelfRepository _shelfRepository;
        private readonly IWarehouseRepository _warehouseRepository;

        public AddShelfCommandHandler(IShelfRepository shelfRepository, IWarehouseRepository warehouseRepository)
        {
            _shelfRepository = shelfRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<ShelfModel> Handle(AddShelfCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.WarehouseId);

            if (warehouse == null)
            {
                throw new Exception("Warehouse not found");
            }

            request.NewShelf.WarehouseId = request.WarehouseId;

            ShelfModel shelfToCreate = new()
            {
                ShelfId = Guid.NewGuid(),
                ShelfRow = request.NewShelf.ShelfRow,
                ShelfColumn = request.NewShelf.ShelfColumn,
                Occupancy = request.NewShelf.Occupancy,
                WarehouseId = request.NewShelf.WarehouseId,
            };

            var createdShelf = await _shelfRepository.AddShelfAsync(shelfToCreate);

            return createdShelf;
        }
    }
}