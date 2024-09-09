using Application.Dto.Warehouse;
using AutoMapper;
using Domain.Models.Warehouse;

namespace Application.MappingProfiles
{
    public class WarehouseMapping : Profile
    {
        public WarehouseMapping()
        {
            CreateMap<WarehouseModel, WarehouseDto>().ReverseMap();
        }

    }
}