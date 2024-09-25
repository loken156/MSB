using Domain.Models.BoxType;
using Infrastructure.Repositories.BoxTypeRepo;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

// This class retrieves a box by its ID. It depends on an IBoxRepository instance injected via its 
// constructor. The Handle method asynchronously processes the query, fetching the box with the specified 
// ID from the repository and returning it.

namespace Application.Queries.BoxType.GetByID
{
    // Query class that encapsulates the request
    

    // Handler for the GetBoxTypeByIdQuery
    public class GetBoxTypeByIdQueryHandler : IRequestHandler<GetBoxTypeByIdQuery, BoxTypeModel>
    {
        private readonly IBoxTypeRepository _boxtypeRepository;

        public GetBoxTypeByIdQueryHandler(IBoxTypeRepository boxtypeRepository)
        {
            _boxtypeRepository = boxtypeRepository;
        }

        public async Task<BoxTypeModel> Handle(GetBoxTypeByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the box type by ID from the repository
            return await _boxtypeRepository.GetBoxTypeByIdAsync(request.BoxTypeId);
        }
    }
}