namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<IAppUser> FindByIdAsync(string userId);
        Task<bool> ChangePasswordAsync(IAppUser user, string currentPassword, string newPassword);

    }
}
