using Domain.Models.Shelf;
using Infrastructure.Repositories.ShelfRepo;
using MediatR;

// This class handles the GetShelfByIdQuery, responsible for retrieving a shelf by its ID.
// It depends on an IShelfRepository instance provided via its constructor for data access.
// The Handle method asynchronously processes the query, retrieving the shelf with the specified ID from the repository.

namespace Application.Queries.Shelf.GetByID
{
    public class GetShelfByIdQueryHandler : IRequestHandler<GetShelfByIdQuery, ShelfModel>
    {
        private readonly IShelfRepository _shelfRepository;

        public GetShelfByIdQueryHandler(IShelfRepository shelfRepository)
        {
            _shelfRepository = shelfRepository;
        }

        public async Task<ShelfModel> Handle(GetShelfByIdQuery request, CancellationToken cancellationToken)
        {
            var shelf = await _shelfRepository.GetShelfWithBoxesAsync(request.ShelfId);
            if (shelf == null)
            {
                throw new KeyNotFoundException($"Shelf with ID {request.ShelfId} was not found.");
            }
            return shelf;
        }
    }

}