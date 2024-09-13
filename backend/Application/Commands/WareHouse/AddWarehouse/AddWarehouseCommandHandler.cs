using Domain.Models.Shelf;
using Domain.Models.Warehouse;
using Infrastructure.Repositories.WarehouseRepo;
using MediatR;

// This class resides in the Application layer and handles the command to add a new warehouse. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the warehouse repository in the Infrastructure layer to add a new 
// warehouse entity to the database. It constructs a new WarehouseModel object with the properties 
// provided in the AddWarehouseCommand, including WarehouseId, WarehouseName, AddressId, and an 
// empty list of shelves. After adding the warehouse to the database, it returns the created 
// WarehouseModel entity.

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
                //Shelves = null
            };

            var createdWarehouse = await _warehouseRepository.AddWarehouseAsync(warehouseToCreate);

            return createdWarehouse;
        }
    }
}