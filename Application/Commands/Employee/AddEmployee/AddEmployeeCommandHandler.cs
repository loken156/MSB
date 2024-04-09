using Domain.Models.Employee;
using MediatR;

namespace Application.Commands.Employee.AddEmployee
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, EmployeeModel>
    {
        public Task<EmployeeModel> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
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
                return Task.FromResult(employeeToCreate);
            }
            catch (Exception ex)
            {
                var newException = new Exception($"An error occurred while adding a new employee: {ex.Message}", ex);
                throw newException;
            }
        }
    }
}
