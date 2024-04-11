using Domain.Models.Employee;
using MediatR;
using System;

namespace Application.Queries.Employee.GetAll
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeModel>>
    {

    }
}