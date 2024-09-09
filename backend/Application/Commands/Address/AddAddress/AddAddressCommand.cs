using Application.Dto.Adress;
using Domain.Models.Address;
using MediatR;

namespace Application.Commands.Address.AddAddress
{
    public class AddAddressCommand : IRequest<AddressDto>
    {
        public AddressDto NewAddress { get; }
        public AddAddressCommand(AddressDto newAddress)
        {
            NewAddress = newAddress;
        }

    }
}