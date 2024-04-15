using Domain.Models.Employee;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Employee.GetAll
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeModel>>
    {
        private readonly IMSBDatabase _database;

        public GetAllEmployeesQueryHandler(IMSBDatabase database)
        {
            _database = database;
        }

        public async Task<List<EmployeeModel>> Handle(GetAllEmployeesQuery query, CancellationToken cancellationToken)
        {
            return await _database.Employees.ToListAsync();
        }
    }
}

