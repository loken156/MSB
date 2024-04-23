using Domain.Models.Results;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<IAppUser> FindByIdAsync(string userId);
        Task<PasswordChangeResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}