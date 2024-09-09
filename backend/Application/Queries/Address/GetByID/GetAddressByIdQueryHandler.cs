using Application.Queries.Address.GetByID;
using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using MediatR;

// This class implements the request handler interface IRequestHandler for GetAddressByIdQuery, which 
// retrieves an address by its ID. It takes an IAddressRepository instance via constructor injection. The 
// Handle method asynchronously processes the query, delegating the retrieval of the address with the 
// specified ID to the corresponding method in the address repository. The retrieved address is then returned.

public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, AddressModel>
{
    private readonly IAddressRepository _addressRepository;

    public GetAddressByIdQueryHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<AddressModel> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        return await _addressRepository.GetAddressByIdAsync(request.AddressId);
    }
}