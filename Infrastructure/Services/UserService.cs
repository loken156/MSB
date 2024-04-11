using Domain.Interfaces;
using Domain.Models.Results;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IAppUser> FindByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user as IAppUser;
        }
        public async Task<PasswordChangeResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new PasswordChangeResult
                {
                    Succeeded = false,
                    Errors = new[] { "User not found" }
                };
            }

            var identityResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            return new PasswordChangeResult
            {
                Succeeded = identityResult.Succeeded,
                Errors = identityResult.Errors.Select(e => e.Description)
            };
        }
    }
}