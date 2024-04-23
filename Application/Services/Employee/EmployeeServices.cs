using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

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