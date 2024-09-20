using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using MediatR;
using Microsoft.AspNetCore.Identity;

// This class resides in the Application layer and handles the command to add a new employee. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the employee repository in the Infrastructure layer to create and 
// persist the new employee entity. It constructs a new EmployeeModel object based on the data 
// provided in the AddEmployeeCommand, saves it to the database, and returns the created employee 
// model if successful. If an error occurs during the process, it throws an exception with appropriate 
// error message for error handling and logging purposes.

namespace Application.Commands.Employee.AddEmployee
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, EmployeeModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository, RoleManager<IdentityRole> roleManager)
        {
            _employeeRepository = employeeRepository;
            _roleManager = roleManager;
        }

        public async Task<EmployeeModel> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.NewEmployee.Roles == null || !request.NewEmployee.Roles.Any())
                {
                    throw new Exception("At least one role must be provided.");
                }

                var roles = new List<IdentityRole>();

                // Fetch the actual IdentityRole objects from the RoleManager
                foreach (var roleName in request.NewEmployee.Roles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role == null)
                    {
                        throw new Exception($"Role '{roleName}' not found.");
                    }
                    roles.Add(role); // Store the IdentityRole object
                }

                // Create the EmployeeModel with the Roles property as ICollection<IdentityRole>
                EmployeeModel employeeToCreate = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = request.NewEmployee.FirstName,
                    LastName = request.NewEmployee.LastName,
                    Email = request.NewEmployee.Email,
                    Department = request.NewEmployee.Department,
                    Position = request.NewEmployee.Position,
                    HireDate = request.NewEmployee.HireDate,
                    Roles = roles, // Assign the list of IdentityRole objects
                    WarehouseId = request.NewEmployee.WarehouseId,
                    UserName = request.NewEmployee.Email
                };

                // Save the new employee to the database
                var result = await _employeeRepository.CreateEmployeeAsync(employeeToCreate);

                if (result == null)
                {
                    throw new Exception("Failed to save the new employee to the database.");
                }

                return employeeToCreate;
            }
            catch (Exception ex)
            {
                var newException = new Exception($"An error occurred while adding a new employee: {ex.Message}", ex);
                throw newException;
            }
        }
    }
}
