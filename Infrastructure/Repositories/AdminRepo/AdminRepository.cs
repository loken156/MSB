using Domain.Models.Admin;
using Infrastructure.Database;
using Infrastructure.Repositories.AdminRepo;
using Microsoft.EntityFrameworkCore;

public class AdminRepository : IAdminRepository
{
    private readonly MSB_Database _database;

    public AdminRepository(MSB_Database database)
    {
        _database = database;
    }

    public async Task<IEnumerable<AdminModel>> GetAdminsAsync()
    {
        return await _database.Admins.ToListAsync();
    }

    public async Task<AdminModel> GetAdminAsync(Guid id)
    {
        return await _database.Admins.FindAsync(id.ToString());
    }

    public async Task<AdminModel> CreateAdminAsync(AdminModel admin)
    {
        _database.Admins.Add(admin);
        await _database.SaveChangesAsync();
        return admin;
    }

    public async Task<AdminModel> UpdateAdminAsync(Guid id, AdminModel admin)
    {
        _database.Entry(admin).State = EntityState.Modified;
        await _database.SaveChangesAsync();
        return admin;
    }

    public async Task<bool> DeleteAdminAsync(Guid id)
    {
        var admin = await _database.Admins.FindAsync(id.ToString());
        if (admin == null)
        {
            return false;
        }

        _database.Admins.Remove(admin);
        await _database.SaveChangesAsync();
        return true;
    }
}