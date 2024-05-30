using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using MediatR;

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
        public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeModel> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                EmployeeModel employeeToCreate = new()
                {
                    FirstName = request.NewEmployee.FirstName,
                    LastName = request.NewEmployee.LastName,
                    Email = request.NewEmployee.Email,
                    // Initialize other properties as needed
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