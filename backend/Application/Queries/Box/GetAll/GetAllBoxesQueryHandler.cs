using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using MediatR;

// This class implements the request handler interface IRequestHandler for GetAllBoxesQuery, which 
// retrieves all boxes. It takes an IBoxRepository instance via constructor injection. The Handle method 
// asynchronously processes the query, delegating the retrieval of all boxes to the corresponding method 
// in the box repository. The retrieved boxes are then returned.

namespace Application.Queries.Box.GetAll
{
    public class GetAllBoxesQueryHandler : IRequestHandler<GetAllBoxesQuery, IEnumerable<BoxModel>>
    {
        private readonly IBoxRepository _boxRepository;
        public GetAllBoxesQueryHandler(IBoxRepository boxRepository)
        {
            _boxRepository = boxRepository;
        }

        public async Task<IEnumerable<BoxModel>> Handle(GetAllBoxesQuery request, CancellationToken cancellationToken)
        {
            return await _boxRepository.GetAllBoxesAsync();
        }
    }
}