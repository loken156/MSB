using Application.Commands.Users.ChangePassword;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
        {
            // User not found, handle accordingly
            return false;
        }

        // Attempt to change the password
        var result = await _userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);

        // Return the result of the update attempt
        return result.Succeeded;
    }
}
