using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, MSB_Database dbContext)
        {
            // Predefined roles
            string[] predefinedRoles = { "Admin", "Employee", "WarehouseWorker", "Driver" };

            // Ensure predefined roles exist
            foreach (var role in predefinedRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Fetch roles from your database (assuming you have a Roles table with a Name column)
            var rolesFromDb = await dbContext.Roles.Select(r => r.Name).ToListAsync();

            // Ensure roles from the database exist
            foreach (var role in rolesFromDb)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}