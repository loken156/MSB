using Domain.Models.Admin;

namespace Infrastructure.Repositories.AdminRepo
{
    public interface IAdminRepository
    {
        Task<IEnumerable<AdminModel>> GetAdminsAsync();
        Task<AdminModel?> GetAdminAsync(Guid id);
        Task<AdminModel> CreateAdminAsync(AdminModel admin);
        Task<AdminModel> UpdateAdminAsync(Guid id, AdminModel admin);
        Task<bool> DeleteAdminAsync(Guid id);
    }
}