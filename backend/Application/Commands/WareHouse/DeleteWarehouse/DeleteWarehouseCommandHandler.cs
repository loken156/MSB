using Infrastructure.Repositories.WarehouseRepo;
using MediatR;

// This class resides in the Application layer and handles the command to delete a warehouse. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the warehouse repository in the Infrastructure layer to delete 
// the warehouse entity from the database based on the provided WarehouseId. After deleting 
// the warehouse, it returns a Unit value to indicate the successful completion of the command. 

namespace Application.Commands.Warehouse.DeleteWarehouse
{
    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, Unit>
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public DeleteWarehouseCommandHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<Unit> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            await _warehouseRepository.DeleteWarehouseAsync(request.WarehouseId);
            return Unit.Value;
        }
    }
}