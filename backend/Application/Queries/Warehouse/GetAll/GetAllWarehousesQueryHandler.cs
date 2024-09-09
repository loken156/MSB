using Domain.Models.Warehouse;
using Infrastructure.Repositories.WarehouseRepo;
using MediatR;

// This class handles the GetAllWarehousesQuery, responsible for retrieving all warehouses from the database.
// It depends on an IWarehouseRepository instance provided via its constructor for data access.
// The Handle method asynchronously processes the query, retrieving all warehouses from the repository.
// It returns an IEnumerable<WarehouseModel> containing all warehouses.

namespace Application.Queries.Warehouse.GetAll
{
    public class GetAllWarehousesQueryHandler : IRequestHandler<GetAllWarehousesQuery, IEnumerable<WarehouseModel>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public GetAllWarehousesQueryHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public async Task<IEnumerable<WarehouseModel>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            return await _warehouseRepository.GetAllWarehousesAsync();
        }
    }
}