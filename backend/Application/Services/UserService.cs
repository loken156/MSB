using Application.Dto.UpdateUserInfo;
using Infrastructure.Entities;

namespace Application.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task UpdateUserAsync(ApplicationUser user, UpdateUserInfoDto updateUserDto)
    {
        // Check if the email is being updated
        if (!string.IsNullOrEmpty(updateUserDto.Email) && user.Email != updateUserDto.Email)
        {
            // Update Email
            user.Email = updateUserDto.Email;

            // Sync UserName with the new Email
            user.UserName = updateUserDto.Email;

            // Normalize the UserName and update NormalizedUserName
            user.NormalizedUserName = _userManager.NormalizeName(updateUserDto.Email);
        }

        // Update the user in the database
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to update the user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
