using Application.Dto.AddShelf;
using Application.Dto.Shelf;
using AutoMapper;
using Domain.Models.Shelf;

// This class represents a mapping profile for AutoMapper, specifically for mapping between ShelfModel and 
// ShelfDto. It inherits from AutoMapper's Profile class. Within the constructor, CreateMap method is 
// utilized to define the mapping from ShelfModel to ShelfDto and vice versa using ReverseMap, indicating 
// bidirectional mapping. Additionally, another mapping is defined from ShelfModel to AddShelfDto and vice 
// versa, allowing for conversion between these types. This mapping profile streamlines the conversion of 
// ShelfModel instances to ShelfDto instances and vice versa, as well as between ShelfModel and AddShelfDto.

namespace Application.MappingProfiles
{
    public class ShelfMapping : Profile
    {
        public ShelfMapping()
        {
            CreateMap<ShelfModel, ShelfDto>().ReverseMap();
            CreateMap<ShelfModel, AddShelfDto>().ReverseMap();
        }

    }
}