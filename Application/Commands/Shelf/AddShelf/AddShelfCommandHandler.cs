using AutoMapper;
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
        private readonly IMapper _mapper;

        public AddShelfCommandHandler(IShelfRepository shelfRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _shelfRepository = shelfRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<ShelfModel> Handle(AddShelfCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByNameAsync(request.WarehouseName);
            if (warehouse == null)
            {
                throw new Exception("Warehouse not found");
            }

            // Use AutoMapper to map AddShelfDto to ShelfModel
            ShelfModel shelfToCreate = _mapper.Map<ShelfModel>(request.NewShelf);
            shelfToCreate.ShelfId = Guid.NewGuid(); // Ensure ShelfId is set to a new GUID
            shelfToCreate.WarehouseId = warehouse.WarehouseId; // Correctly assign the WarehouseId

            var createdShelf = await _shelfRepository.AddShelfAsync(shelfToCreate);

            return createdShelf;
        }
    }
}