using Domain.Models.Warehouse;
using Infrastructure.Repositories.WarehouseRepo;
using MediatR;

// This class resides in the Application layer and handles the command to update a warehouse. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the warehouse repository in the Infrastructure layer to update 
// the warehouse entity in the database. It constructs a new WarehouseModel object with the 
// properties provided in the UpdateWarehouseCommand, including WarehouseId and WarehouseName. 
// Other properties like AddressId and ShelfId are commented out indicating they are pending 
// implementation. After updating the warehouse in the database, it returns the updated 
// WarehouseModel entity.

namespace Application.Commands.Warehouse.UpdateWarehouse
{
    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, WarehouseModel>
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public UpdateWarehouseCommandHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<WarehouseModel> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            WarehouseModel warehouseToUpdate = new WarehouseModel
            {
                WarehouseId = request.Warehouse.WarehouseId,
                WarehouseName = request.Warehouse.WarehouseName,
                // AddressId = request.Warehouse.AddressId, // Comment until implemented
                // ShelfId = request.Warehouse.ShelfId // Comment until implemented


            };

            return await _warehouseRepository.UpdateWarehouseAsync(warehouseToUpdate);
        }
    }
}