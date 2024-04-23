using Domain.Models.Employee;
using Infrastructure.Database;
using Infrastructure.Repositories.EmployeeRepo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

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