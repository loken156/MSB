using Application.Dto.Box;
using Application.Dto.Order;
using AutoMapper;
using Domain.Models.Box;

namespace Application.MappingProfiles
{
    public class BoxMapping : Profile
    {
        public BoxMapping()
        {
            CreateMap<BoxModel, BoxDto>().ReverseMap();
            CreateMap<BoxModel, AddBoxToOrderDto>().ReverseMap();
        }
    }
}