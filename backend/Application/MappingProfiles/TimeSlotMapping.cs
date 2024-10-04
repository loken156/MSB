using Application.Dto.TimeSlot;
using AutoMapper;
using Domain.Models.TimeSlot;

namespace Application.MappingProfiles
{
    // Install AutoMapper.Extensions.Microsoft.DependencyInjection via NuGet

    public class TimeSlotMapping : Profile
    {
        public TimeSlotMapping()
        {
            // Correct usage of ReverseMap() method
            CreateMap<TimeSlotModel, TimeSlotDto>().ReverseMap();
        }
    }
}