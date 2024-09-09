using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using MediatR;

// This class implements the request handler interface IRequestHandler for GetAllAddressesQuery, which 
// retrieves all addresses. It takes an IAddressRepository instance via constructor injection. The 
// Handle method asynchronously processes the query, delegating the retrieval of all addresses to the 
// corresponding method in the address repository. The retrieved addresses are then returned.

namespace Application.Queries.Address.GetAll
{
    public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, IEnumerable<AddressModel>>
    {
        private readonly IAddressRepository _addressRepository;
        public GetAllAddressesQueryHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<IEnumerable<AddressModel>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            return await _addressRepository.GetAllAddressesAsync();
        }
    }
}