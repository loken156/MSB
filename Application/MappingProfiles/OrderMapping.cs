using Application.Dto.Order;
using AutoMapper;
using Domain.Models.Order;

namespace Application.MappingProfiles
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderDto, OrderModel>().ReverseMap();
        }

    }
}
