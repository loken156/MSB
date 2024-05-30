using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

// This class implements the IEmployeeServices interface, providing functionality related to employee management.
// It depends on a UserManager<ApplicationUser> instance provided via its constructor for user management operations.
// The AssignRole method asynchronously assigns a role to an employee identified by their email.
// It retrieves the user with the specified email using the UserManager, then adds the specified role to the user.

namespace Application.Services.Employee
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task AssignRole(string employeeEmail, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(employeeEmail);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}