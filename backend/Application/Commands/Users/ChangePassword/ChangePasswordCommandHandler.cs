using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

// This class resides in the Application layer and handles the command to change a user's password. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the UserManager provided by ASP.NET Core Identity to retrieve the 
// user based on the provided UserId. If the user is not found, it returns false. Otherwise, it attempts 
// to change the user's password using the ChangePasswordAsync method of the UserManager. The method returns 
// true if the password change is successful; otherwise, it returns false.

namespace Application.Commands.Users.ChangePassword
{

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
}