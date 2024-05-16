using Application.Dto.Admin;
using Domain.Models.Admin;
using Infrastructure.Repositories.AdminRepo;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AdminController> _logger;
        private readonly IMediator _mediator;

        public AdminController(IAdminRepository adminRepository, UserManager<IdentityUser> userManager, ILogger<AdminController> logger, IMediator mediator)
        {
            _adminRepository = adminRepository;
            _userManager = userManager;
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminModel>>> GetAdmins()
        {
            try
            {
                var admins = await _adminRepository.GetAdminsAsync();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting admins");
                return StatusCode(500, "An error occurred while getting the admins");
            }



        }

        // GET: api/Admin/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminModel>> GetAdmin(Guid id)
        {
            try
            {
                var admin = await _adminRepository.GetAdminAsync(id);
                if (admin == null)
                {
                    return NotFound();
                }
                return Ok(admin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting admin with id: {Id}", id);
                return StatusCode(500, "An error occurred while getting the admin");
            }


        }

        // POST: api/Admin
        [HttpPost]
        public async Task<ActionResult<AdminDto>> CreateAdmin(AdminDto adminDto)
        {
            try
            {
                var admin = new AdminModel
                {
                    Id = adminDto.AdminId.ToString(),
                    UserName = adminDto.Email,
                    Email = adminDto.Email,
                    FirstName = adminDto.FirstName,
                    LastName = adminDto.LastName,
                    Role = "Admin",
                    // Permissions = adminDto.Permissions
                };

                var createdAdmin = await _adminRepository.CreateAdminAsync(admin);

                var user = await _userManager.FindByIdAsync(createdAdmin.Id);

                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }

                // Assign the "Admin" role to the new admin
                var result = await _userManager.AddToRoleAsync(user, "Admin");

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return CreatedAtAction(nameof(GetAdmin), new { id = createdAdmin.Id }, createdAdmin);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating admin with email: {EmployeeEmail}", adminDto.Email);
                return StatusCode(500, "An error occurred while creating the admin");
            }




        }

        // PUT: api/Admin/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(Guid id, AdminDto adminDto)
        {

            try
            {

                if (id != adminDto.AdminId)
                {
                    return BadRequest();
                }

                var admin = new AdminModel
                {
                    Id = adminDto.AdminId.ToString(),
                    UserName = adminDto.Email,
                    Email = adminDto.Email,
                    FirstName = adminDto.FirstName,
                    LastName = adminDto.LastName,
                    Role = "Admin",
                    // Permissions = adminDto.Permissions
                };

                var updatedAdmin = await _adminRepository.UpdateAdminAsync(id, admin);
                if (updatedAdmin == null)
                {
                    return NotFound();
                }
                return Ok("Update successful");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating admin with id: {Id}", id);
                return StatusCode(500, "An error occurred while updating the admin");
            }

        }

        // DELETE: api/Admin/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            try
            {
                var result = await _adminRepository.DeleteAdminAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deleting admin with id: {Id}", id);
                return StatusCode(500, "An error occurred while deleting the admin");
            }

        }
    }
}