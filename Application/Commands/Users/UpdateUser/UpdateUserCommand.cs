using Application.Dto.UpdateUserInfo;
using Infrastructure.Entities;
using MediatR;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<ApplicationUser>
    {
        public UpdateUserInfoDto UpdateUserInfoDto { get; }
        public string UserId { get; }

        public string UpdatePassword { get; set; }

        public UpdateUserCommand(UpdateUserInfoDto userInfoDto, string userId)
        {
            UpdateUserInfoDto = userInfoDto;
            UserId = userId;
        }
    }
}