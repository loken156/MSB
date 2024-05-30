using Domain.Models.Employee;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

// This class implements the IEmployeeRepository interface and provides methods for managing EmployeeModel entities in the MSB_Database.
// The class includes methods to:
// - Retrieve all employees asynchronously with GetEmployeesAsync()
// - Retrieve a specific employee by ID asynchronously with GetEmployeeByIdAsync(Guid id)
// - Create a new employee asynchronously with CreateEmployeeAsync(EmployeeModel employee)
// - Update an existing employee asynchronously with UpdateEmployeeAsync(Guid id, EmployeeModel employee)
// - Delete an employee asynchronously with DeleteEmployeeAsync(Guid id)
// The class uses Entity Framework Core for database operations and ensures changes are saved asynchronously to the database.
// The UpdateEmployeeAsync method also handles updating only the properties of an existing employee that are provided in the input EmployeeModel.

namespace Infrastructure.Repositories.EmployeeRepo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MSB_Database _database;

        public EmployeeRepository(MSB_Database mSB_Database)
        {
            _database = mSB_Database;
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployeesAsync()
        {
            return await _database.Employees.ToListAsync();
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(Guid id)
        {
            return await _database.Employees.FindAsync(id.ToString());
        }

        public async Task<EmployeeModel> CreateEmployeeAsync(EmployeeModel employee)
        {
            _database.Employees.Add(employee);
            await _database.SaveChangesAsync();
            return employee;
        }

        public async Task<EmployeeModel> UpdateEmployeeAsync(Guid id, EmployeeModel employee)
        {
            var existingEmployee = await _database.Employees.FindAsync(id.ToString());

            if (existingEmployee == null)
            {
                throw new Exception($"No employee found with ID {id}");
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.UserName = employee.UserName;

            // Update the rest of the properties if needed

            await _database.SaveChangesAsync();

            return existingEmployee;
        }


        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var employee = await _database.Employees.FindAsync(id.ToString());

            if (employee == null)
            {
                return false;
            }

            _database.Employees.Remove(employee);
            await _database.SaveChangesAsync();

            return true;
        }
    }
}