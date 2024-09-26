using Domain.Models.BoxType;
using Infrastructure.Repositories.BoxTypeRepo;
using MediatR;

// This class implements the request handler interface IRequestHandler for GetAllBoxesQuery, which 
// retrieves all boxes. It takes an IBoxRepository instance via constructor injection. The Handle method 
// asynchronously processes the query, delegating the retrieval of all boxes to the corresponding method 
// in the box repository. The retrieved boxes are then returned.

namespace Application.Queries.BoxType.GetAll
{
    public class GetAllBoxTypesQueryHandler : IRequestHandler<GetAllBoxTypesQuery, IEnumerable<BoxTypeModel>>
    {
        private readonly IBoxTypeRepository _boxtypeRepository;
        public GetAllBoxTypesQueryHandler(IBoxTypeRepository boxtypeRepository)
        {
            _boxtypeRepository = boxtypeRepository;
        }

        public async Task<IEnumerable<BoxTypeModel>> Handle(GetAllBoxTypesQuery request, CancellationToken cancellationToken)
        {
            return await _boxtypeRepository.GetAllBoxTypesAsync();
        }
    }
}