using Application.Dto.Adress;
using AutoMapper;
using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using MediatR;

// This class handles the command to add a new address. It uses MediatR for processing the command 
// and AutoMapper for converting between DTOs and domain models. The handler interacts with the 
// address repository to save the new address and returns the created address as a DTO.


namespace Application.Commands.Address.AddAddress
{
    public class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, AddressDto>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public AddAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<AddressDto> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            var addressModel = _mapper.Map<AddressModel>(request.NewAddress);

            await _addressRepository.AddAddressAsync(addressModel);

            var addressDto = _mapper.Map<AddressDto>(addressModel);

            return addressDto;

        }
    }

}