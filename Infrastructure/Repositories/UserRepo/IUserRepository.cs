using Domain.Interfaces;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<ApplicationUser> AddUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string id);
        Task<IAppUser> GetUserByIdAsync(string id);

        Task<List<IAppUser>> GetAllUsersAsync();

        Task<ApplicationUser> GetByEmailAsync(string email);

        Task<bool> UpdatePasswordAsync(ApplicationUser user);



    }
}
