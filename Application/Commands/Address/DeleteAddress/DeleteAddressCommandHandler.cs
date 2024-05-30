using Infrastructure.Repositories.AddressRepo;
using MediatR;

// This class handles the command to delete an address. It uses MediatR for processing the command
// and interacts with the address repository to remove the specified address from the data source.

namespace Application.Commands.Address.DeleteAddress
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Unit>
    {
        private readonly IAddressRepository _addressRepository;

        public DeleteAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Unit> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            await _addressRepository.DeleteAddressAsync(request.AddressId);
            return Unit.Value;
        }
    }
}