using Application.Dto.Box;
using Application.Dto.Order;
using AutoMapper;
using Domain.Models.Box;

// This class defines a mapping profile for AutoMapper to map between BoxModel and BoxDto, as well as 
// between BoxModel and AddBoxToOrderDto. It inherits from AutoMapper's Profile class. In the constructor, 
// CreateMap method is used to define the mapping between BoxModel and BoxDto bidirectionally using ReverseMap, 
// indicating that the mapping is reversible. Additionally, another CreateMap call defines the mapping between 
// BoxModel and AddBoxToOrderDto bidirectionally. This mapping profile facilitates the conversion of BoxModel 
// instances to BoxDto instances, AddBoxToOrderDto instances, and vice versa.

namespace Application.MappingProfiles
{
    public class BoxMapping : Profile
    {
        public BoxMapping()
        {
            // Map BoxModel to BoxDto and reverse, handling BoxType for Size.
            CreateMap<BoxModel, BoxDto>()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.BoxType.Size)) // Map Size from BoxType
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.BoxType.Type)) // Map Type from BoxType
                .ForMember(dest => dest.BoxTypeId, opt => opt.MapFrom(src => src.BoxTypeId)) // Map BoxTypeId directly
                .ReverseMap();

            // Map between BoxModel and AddBoxToOrderDto
            CreateMap<BoxModel, AddBoxToOrderDto>().ReverseMap();
        }
    }
}