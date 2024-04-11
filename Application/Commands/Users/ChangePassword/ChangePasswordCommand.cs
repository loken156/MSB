using MediatR;

namespace Application.Commands.Users.ChangePassword
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public ChangePasswordCommand(string userId, string currentPassword, string newPassword)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }
}