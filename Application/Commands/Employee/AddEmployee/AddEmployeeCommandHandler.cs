using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using MediatR;

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
                await _employeeRepository.CreateEmployeeAsync(employeeToCreate);

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
