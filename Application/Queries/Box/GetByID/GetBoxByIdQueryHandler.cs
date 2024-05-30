using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;

// This class retrieves a box by its ID. It depends on an IBoxRepository instance injected via its 
// constructor. The Handle method asynchronously processes the query, fetching the box with the specified 
// ID from the repository and returning it. 

namespace Application.Queries.Box.GetByID
{
    public class GetBoxByIdQueryHandler
    {
        private readonly IBoxRepository _boxRepository;

        public GetBoxByIdQueryHandler(IBoxRepository boxRepository)
        {
            _boxRepository = boxRepository;
        }

        public async Task<BoxModel> Handle(GetBoxByIdQuery request, CancellationToken cancellationToken)
        {
            return await _boxRepository.GetBoxByIdAsync(request.BoxId);
        }
    }
}