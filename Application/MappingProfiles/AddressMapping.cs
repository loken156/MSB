using Application.Dto.Adress;
using AutoMapper;
using Domain.Models.Address;

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