using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using MediatR;

// This class handles the command to update an existing address. It uses MediatR for processing the command
// and interacts with the address repository to update the address in the data source, returning the updated address model.

namespace Application.Commands.Address.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, AddressModel>
    {
        private readonly IAddressRepository _addressRepository;

        public UpdateAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<AddressModel> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            return await _addressRepository.UpdateAddressAsync(request.Address);
        }
    }
}