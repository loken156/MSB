using Application.Dto.AddShelf;
using Application.Dto.Shelf;
using AutoMapper;
using Domain.Models.Shelf;

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