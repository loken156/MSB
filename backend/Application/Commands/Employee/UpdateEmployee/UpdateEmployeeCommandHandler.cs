using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;

// This class resides in the Application layer and handles the command to update an employee. 
// It interacts with the employee repository in the Infrastructure layer to retrieve the 
// employee entity based on the provided EmployeeId. If the employee is not found, it throws 
// a KeyNotFoundException. Otherwise, it updates the employee's properties with the values 
// provided in the UpdateEmployeeDto of the command and then updates the employee in the 
// repository. Finally, it returns the updated employee model.

namespace Application.Commands.Employee.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeModel> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(command.EmployeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }

            employee.Email = command.UpdateEmployeeDto.Email ?? employee.Email;
            employee.FirstName = command.UpdateEmployeeDto.FirstName ?? employee.FirstName;
            employee.LastName = command.UpdateEmployeeDto.LastName ?? employee.LastName;
            await _employeeRepository.UpdateEmployeeAsync(command.EmployeeId, employee);
            return employee;
        }
    }
}