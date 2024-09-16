using Application.Commands.Admin.Add;
using Application.Dto.Admin;
using Domain.Models.Admin;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminMaker : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminMaker(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("create-admin")]
        public async Task<ActionResult> CreateAdmin([FromBody] AdminDto dto)
        {
            try
            {
                // Validate the DTO if necessary (you can integrate FluentValidation)

                // Create a new ApplicationUser for Identity
                var applicationUser = new ApplicationUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };

                // Use Identity's UserManager to create the user and hash the password
                var result = await _userManager.CreateAsync(applicationUser, dto.Password);

                // Check if user creation was successful
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                // Create the admin in the domain using MediatR command
                var admin = await CreateAdmin(applicationUser);

                return Ok(admin); // return the created admin details
            }
            catch (Exception ex)
            {
                // Handle errors appropriately
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task<AdminModel> CreateAdmin(ApplicationUser applicationUser)
        {
            // Use MediatR to handle admin creation logic
            var command = new AddAdminCommand
            {
                AdminId = Guid.NewGuid(),
                Email = applicationUser.Email,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName
                // Add other necessary fields
            };

            var result = await _mediator.Send(command);
            return result;
        }
    }
}