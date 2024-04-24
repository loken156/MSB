using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using MediatR;

namespace Application.Commands.Address.AddAddress
{
    public class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, AddressModel>
    {
        private readonly IAddressRepository _addressRepository;

        public AddAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<AddressModel> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            AddressModel addressToCreate = new()
            {
                AddressId = Guid.NewGuid(),
                StreetName = request.NewAddress.StreetName ?? string.Empty,
                StreetNumber = request.NewAddress.StreetNumber ?? string.Empty,
                Apartment = request.NewAddress.Apartment,
                ZipCode = request.NewAddress.ZipCode ?? string.Empty,
                Floor = request.NewAddress.Floor,
                // Additional geographic information
                City = request.NewAddress.City,
                State = request.NewAddress.State,
                Country = request.NewAddress.Country,
                Latitude = request.NewAddress.Latitude,
                Longitude = request.NewAddress.Longitude,
            };

            // Asynchronously save the new address using the repository
            await _addressRepository.AddAddressAsync(addressToCreate);

            return addressToCreate;
        }
    }

}