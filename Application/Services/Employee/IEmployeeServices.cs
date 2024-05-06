namespace Application.Services.Employee
{
    public interface IEmployeeServices
    {
        Task AssignRole(string employeeEmail, string roleName);
    }
}