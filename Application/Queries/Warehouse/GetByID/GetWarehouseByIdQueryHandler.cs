using Domain.Models.Warehouse;
using Infrastructure.Repositories.WarehouseRepo;
using MediatR;

// This class handles the GetWarehouseByIdQuery, responsible for retrieving a warehouse by its ID from the database.
// It depends on an IWarehouseRepository instance provided via its constructor for data access.
// The Handle method asynchronously processes the query, retrieving the warehouse with the specified ID from the repository.
// It returns a WarehouseModel representing the warehouse if found, otherwise null.

namespace Application.Queries.Warehouse.GetByID
{
    public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, WarehouseModel>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public GetWarehouseByIdQueryHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public async Task<WarehouseModel> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _warehouseRepository.GetWarehouseByIdAsync(request.WarehouseId);
        }
    }
}