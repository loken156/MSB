using Application.Dto.Adress;
using AutoMapper;
using Domain.Models.Address;

// This class defines a mapping profile for AutoMapper to map between AddressModel and AddressDto. 
// It inherits from AutoMapper's Profile class. In the constructor, CreateMap method is used to define 
// the mapping between AddressModel and AddressDto bidirectionally using ReverseMap. This mapping 
// profile facilitates the conversion of AddressModel instances to AddressDto instances and vice versa.

namespace Application.MappingProfiles
{
    public class AddressMapping : Profile
    {
        public AddressMapping()
        {
            CreateMap<AddressModel, AddressDto>().ReverseMap();
        }
    }



}