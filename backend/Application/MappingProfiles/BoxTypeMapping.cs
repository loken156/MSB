using Application.Dto.Box;
using Application.Dto.BoxType;
using AutoMapper;
using Domain.Models.BoxType;

namespace Application.MappingProfiles
{
    // Install AutoMapper.Extensions.Microsoft.DependencyInjection via NuGet

    public class BoxTypeMapping : Profile
    {
        public BoxTypeMapping()
        {
            // Correct usage of ReverseMap() method
            CreateMap<BoxTypeModel, BoxTypeDto>().ReverseMap();
            CreateMap<BoxTypeModel, BoxDto>().ReverseMap();
        }
    }
}