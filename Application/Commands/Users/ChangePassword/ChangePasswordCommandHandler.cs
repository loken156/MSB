using Application.Commands.Users.ChangePassword;
using Domain.Interfaces;
using MediatR;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IUserService _userService;

    public ChangePasswordCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userService.FindByIdAsync(command.UserId);
        if (user == null)
        {
            // User not found, handle accordingly
            return false;
        }

        // Attempt to change the password
        var result = await _userService.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);

        // Return the result of the update attempt
        return result;
    }
}