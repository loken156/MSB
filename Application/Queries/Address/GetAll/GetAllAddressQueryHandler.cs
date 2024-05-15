using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using MediatR;

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