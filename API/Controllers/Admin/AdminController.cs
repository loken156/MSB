using Application.Dto.Admin;
using Domain.Models.Admin;
using Infrastructure.Repositories.AdminRepo;
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

        public AdminController(IAdminRepository adminRepository, UserManager<IdentityUser> userManager)
        {
            _adminRepository = adminRepository;
            _userManager = userManager;
        }

        // GET: api/Admin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminModel>>> GetAdmins()
        {
            var admins = await _adminRepository.GetAdminsAsync();
            return Ok(admins);
        }

        // GET: api/Admin/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminModel>> GetAdmin(Guid id)
        {
            var admin = await _adminRepository.GetAdminAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        // POST: api/Admin
        [HttpPost]
        public async Task<ActionResult<AdminDto>> CreateAdmin(AdminDto adminDto)
        {
            var admin = new AdminModel
            {
                Id = adminDto.AdminId.ToString(),
                UserName = adminDto.Email,
                Email = adminDto.Email,
                FirstName = adminDto.FirstName,
                LastName = adminDto.LastName,
                Role = "Admin",
                Permissions = adminDto.Permissions
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

        // PUT: api/Admin/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(Guid id, AdminDto adminDto)
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
                Permissions = adminDto.Permissions
            };

            var updatedAdmin = await _adminRepository.UpdateAdminAsync(id, admin);
            if (updatedAdmin == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Admin/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            var result = await _adminRepository.DeleteAdminAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
