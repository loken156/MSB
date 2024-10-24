﻿using Domain.Models.Employee;

namespace Infrastructure.Repositories.EmployeeRepo
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeModel>> GetEmployeesAsync();
        Task<EmployeeModel> GetEmployeeByIdAsync(Guid id);
        Task<EmployeeModel> CreateEmployeeAsync(EmployeeModel employee);
        Task<EmployeeModel> UpdateEmployeeAsync(Guid id, EmployeeModel employee);
        Task<bool> DeleteEmployeeAsync(Guid id);
    }
}