using Domain.Interfaces;
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


        public async Task<bool> ChangePasswordAsync(IAppUser user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync((ApplicationUser)user, currentPassword, newPassword);
            return result.Succeeded;
        }
    }
}
