﻿using Domain.Models.Employee;
using Infrastructure.Repositories.EmployeeRepo;
using MediatR;

// This class handles the GetEmployeeByIdQuery, responsible for retrieving an employee by ID. 
// It depends on an IEmployeeRepository instance provided via its constructor for data access. 
// The Handle method asynchronously processes the query, attempting to retrieve the employee 
// with the specified ID from the repository. Any exceptions encountered during the process are 
// caught and rethrown with an appropriate message.

namespace Application.Queries.Employee.GetById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeModel>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeModel> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employeeId = request.Id;
                var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
                return employee;
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly
                throw new Exception("Error occurred while fetching employee by ID", ex);
            }
        }
    }
}