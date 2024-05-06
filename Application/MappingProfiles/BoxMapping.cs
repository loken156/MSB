using Application.Dto.Box;
using AutoMapper;
using Domain.Models.Box;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
    public class BoxMapping : Profile
    {
        public BoxMapping()
        {
            CreateMap<BoxModel,BoxDto>().ReverseMap();
        }
    }
}
