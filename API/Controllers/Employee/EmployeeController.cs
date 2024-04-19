using Application.Commands.Employee.AddEmployee;
using Application.Dto.Employee;
using Application.Validators.EmployeeValidator;
using Domain.Interfaces;
using Domain.Models.Employee;
using Infrastructure.Entities;
using Infrastructure.Repositories.EmployeeRepo;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IEmployeeService = Application.Services.Employee.IEmployeeService;

namespace API.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeValidations _employeeValidations;
        private readonly IEmployeeService _employeeService;
     

        public EmployeeController(IEmployeeRepository employeeRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, Mediator mediator,
            ILogger<EmployeeController> logger, EmployeeValidations validations, IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _mediator = mediator;
            _logger = logger;
            _employeeValidations = validations;
            _employeeService = employeeService;
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
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto, bool isAdmin)
        {
            _logger.LogInformation("Starting to create new employee: {EmployeeEmail}", employeeDto.Email);

            try
            {
                var command = new AddEmployeeCommand(employeeDto);
                var createdEmployee = await _mediator.Send(command);

                if (createdEmployee == null)
                {
                    _logger.LogWarning("Failed to create employee: {EmployeeEmail}", employeeDto.Email);
                    return BadRequest(new { Message = "Failed to create employee" });
                }

                if (isAdmin)
                {
                    await _employeeService.AssignRole(employeeDto.Email, "Admin");
                }

                _logger.LogInformation("Employee created successfully: {Id}", createdEmployee.Id);
                return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee: {EmployeeEmail}", employeeDto.Email);
                return StatusCode(500, new { Message = "Internal server error" });
            }
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