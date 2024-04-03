using Infrastructure.Entities;

namespace Infrastructure.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<ApplicationUser> AddUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(Guid id);
        Task<ApplicationUser> GetUserByIdAsync(Guid id);

        Task<List<ApplicationUser>> GetAllUsersAsync();

        Task<ApplicationUser> GetByEmailAsync(string email);

        Task<bool> UpdatePasswordAsync(ApplicationUser user);



    }
}
