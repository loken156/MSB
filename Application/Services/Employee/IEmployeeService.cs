using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Employee
{
    public interface IEmployeeService
    {
        Task AssignRole(string employeeEmail, string roleName);
    }
}
