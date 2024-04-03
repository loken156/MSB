using Application.Dto.LogIn;
using Infrastructure.Entities;
using MediatR;

namespace Application.Commands.Users.LogIn
{
    public class UserLoginCommand : IRequest<ApplicationUser>
    {
        public LogInDto logInDtos { get; set; }

        public UserLoginCommand(LogInDto logInDto)
        {
            logInDtos = logInDto;
        }
    }

}

