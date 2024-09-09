using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using MediatR;
using Microsoft.Extensions.Logging;

// This class handles the GetAllEmployeesQuery, responsible for retrieving all employees. It 
// relies on an IEmployeeRepository instance provided via its constructor for data access and 
// optionally a ILogger for logging. The Handle method asynchronously processes the query, 
// attempting to retrieve all employees from the repository. Any exceptions encountered during 
// the process are caught, logged, and rethrown.

namespace Application.Queries.Employee.GetAll
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeModel>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<GetAllEmployeesQueryHandler> _logger;


        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, ILogger<GetAllEmployeesQueryHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<List<EmployeeModel>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesAsync();
                return employees.ToList();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error in GetAllEmployeesQueryHandler");
                throw;
            }
        }
    }
}