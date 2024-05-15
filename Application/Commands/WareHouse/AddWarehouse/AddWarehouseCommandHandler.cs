using Domain.Models.Shelf;
using Domain.Models.Warehouse;
using Infrastructure.Repositories.WarehouseRepo;
using MediatR;

namespace Application.Commands.Warehouse.AddWarehouse
{
    public class AddWarehouseCommandHandler : IRequestHandler<AddWarehouseCommand, WarehouseModel>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public AddWarehouseCommandHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public async Task<WarehouseModel> Handle(AddWarehouseCommand request, CancellationToken cancellationToken)
        {
            WarehouseModel warehouseToCreate = new()
            {
                WarehouseId = Guid.NewGuid(),
                WarehouseName = request.NewWarehouse.WarehouseName,
                AddressId = request.NewWarehouse.AddressId,
                Shelves = new List<ShelfModel>()
            };

            var createdWarehouse = await _warehouseRepository.AddWarehouseAsync(warehouseToCreate);

            return createdWarehouse;
        }
    }
}