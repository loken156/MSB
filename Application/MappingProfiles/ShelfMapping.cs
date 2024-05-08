using Application.Dto.Shelf;
using Domain.Models.Shelf;
using Org.BouncyCastle.Asn1.Esf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.MappingProfiles
{
    public class ShelfMapping : Profile
    {
        public ShelfMapping()
        {
            CreateMap<ShelfModel, ShelfDto>().ReverseMap();
        }

    }
}
