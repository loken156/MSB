using Application.Dto.Adress;
using AutoMapper;
using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using MediatR;

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
            var addressmodel = _mapper.Map<AddressModel>(request.NewAddress); 
            
            await _addressRepository.AddAddressAsync(addressmodel);

            var addressDto = _mapper.Map<AddressDto>(addressmodel);

            return addressDto;

        }
    }

}