using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, MSB_Database dbContext)
        {
            // Step 1: Initialize predefined roles
            string[] predefinedRoles = { "Admin", "Employee", "WarehouseWorker", "Driver" };

            foreach (var role in predefinedRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Step 2: Synchronize roles from the database (from the custom Roles table)
            var rolesFromDb = await dbContext.Roles.Select(r => r.Name).ToListAsync();
            
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


/*using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, MSB_Database dbContext)
        {
            // Fetch roles from your database (assuming you have a Roles table with a Name column)
            var rolesFromDb = await dbContext.Roles.Select(r => r.Name).ToListAsync();

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




/* using Microsoft.AspNetCore.Identity;

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
} */