using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeService(UserManager<ApplicationUser> userManager)
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
