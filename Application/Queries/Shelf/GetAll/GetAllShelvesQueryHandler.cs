using Domain.Models.Shelf;
using Infrastructure.Repositories.ShelfRepo;
using MediatR;

// This class handles the GetAllShelvesQuery, responsible for retrieving all shelves.
// It depends on an IShelfRepository instance provided via its constructor for data access.
// The Handle method asynchronously processes the query, retrieving all shelves from the repository.

namespace Application.Queries.Shelf.GetAll
{
    public class GetAllShelvesQueryHandler : IRequestHandler<GetAllShelvesQuery, IEnumerable<ShelfModel>>
    {
        private readonly IShelfRepository _shelfRepository;
        public GetAllShelvesQueryHandler(IShelfRepository shelfRepository)
        {
            _shelfRepository = shelfRepository;
        }
        public async Task<IEnumerable<ShelfModel>> Handle(GetAllShelvesQuery request, CancellationToken cancellationToken)
        {
            return await _shelfRepository.GetAllAsync();
        }
    }
}