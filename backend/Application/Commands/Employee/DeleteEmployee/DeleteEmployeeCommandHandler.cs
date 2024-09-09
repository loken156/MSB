using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using MediatR;

// This class resides in the Application layer and handles the command to delete an employee. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the employee repository in the Infrastructure layer to retrieve and 
// delete the employee entity based on the provided EmployeeId. It first retrieves the employee 
// to be deleted from the repository and checks if it exists. If not found, it throws an InvalidOperationException. 
// Otherwise, it proceeds to delete the employee from the database and returns the deleted employee model. 
// If an error occurs during the process, it throws an exception with an appropriate error message 
// for error handling and logging purposes.

namespace Application.Commands.Employee.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, EmployeeModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeModel> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                EmployeeModel employeeToDelete = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);

                if (employeeToDelete == null)
                {
                    throw new InvalidOperationException("No Employee with the given id was found");
                }

                await _employeeRepository.DeleteEmployeeAsync(request.EmployeeId);

                return employeeToDelete;
            }
            catch (Exception ex)
            {
                var newException = new Exception($"An Error occurred when deleteting employee with id {request.EmployeeId}", ex);
                throw newException;
            }
        }
    }
}