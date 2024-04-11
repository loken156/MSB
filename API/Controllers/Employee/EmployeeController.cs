using Application.Dto.Employee;
using Domain.Models.Employee;
using Infrastructure.Entities;
using Infrastructure.Repositories.EmployeeRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeController(IEmployeeRepository employeeRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/Employee
        [HttpGet]
        [Route("Get All Employees")]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            return Ok(employees);
        }

        // GET: api/Employee/{id}
        [HttpGet("Get Employee By {id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployee(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        [Route("Add Employee")]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeDto employeeDto)
        {
            var employee = new EmployeeModel
            {
                Id = employeeDto.EmployeeId.ToString(),
                UserName = employeeDto.Email,
                Email = employeeDto.Email,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Role = "Employee",
                Department = employeeDto.Department,
                Position = employeeDto.Position,
                HireDate = employeeDto.HireDate,
                WarehouseId = employeeDto.WarehouseId
            };

            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);

            var user = await _userManager.FindByIdAsync(createdEmployee.Id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Assign the "Employee" role to the new employee
            var result = await _userManager.AddToRoleAsync(user, "Employee");

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
        }


        // PUT: api/Employee/{id}
        [HttpPut("Update Employee By {id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeModel employee)
        {
            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(id, employee);
            if (updatedEmployee == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Employee/{id}
        [HttpDelete("Delete Employee By {id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await _employeeRepository.DeleteEmployeeAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
