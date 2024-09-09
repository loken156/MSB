using Microsoft.AspNetCore.Identity;

// This class provides a method to initialize roles in the application using the RoleManager.
// It ensures that predefined roles ("Admin", "Employee", "WarehouseWorker", "Driver") are created if they do not already exist.

namespace Infrastructure.Identity
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Employee", "WarehouseWorker", "Driver" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}