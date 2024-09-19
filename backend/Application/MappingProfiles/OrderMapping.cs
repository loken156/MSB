using Application.Dto.Order;
using AutoMapper;
using Domain.Models.Order;

// This class represents a mapping profile for AutoMapper, specifically for mapping between OrderDto and 
// OrderModel. It inherits from AutoMapper's Profile class. Within the constructor, CreateMap method is 
// utilized to define the mapping from OrderDto to OrderModel and vice versa using ReverseMap, indicating 
// bidirectional mapping. This mapping profile streamlines the conversion of OrderDto instances to OrderModel 
// instances and vice versa.

namespace Application.MappingProfiles
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderDto, OrderModel>().ReverseMap();
            
            // New mapping for AddOrderDto to OrderModel
            CreateMap<AddOrderDto, OrderModel>()
                .ForMember(dest => dest.OrderId, opt => opt.Ignore()) // OrderId is typically generated
                .ForMember(dest => dest.Boxes, opt => opt.Ignore())   // Boxes might be added later
                .ForMember(dest => dest.Car, opt => opt.Ignore());    // Car might not always be provided
        }

    }
}