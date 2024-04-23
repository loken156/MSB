using Application.Dto.Register;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Registration
{
    public class RegistrationCommand : IRequest<IdentityResult>
    {
        public RegisterDto RegDto { get; set; }

        public RegistrationCommand(RegisterDto regDto)
        {
            RegDto = regDto;
        }

    }
}