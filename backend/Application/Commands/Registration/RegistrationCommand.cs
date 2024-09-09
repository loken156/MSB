using Application.Dto.Register;
using MediatR;
using Microsoft.AspNetCore.Identity;

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