using Application.Commands.Users.DeleteUser;
using Application.Commands.Users.UpdateUser;
using Application.Dto.UpdateUserInfo;
using Application.Queries.User.GetAll;
using Application.Queries.User.GetById;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers.UserController
{
    [AllowAnonymous]
    // Define the route and make this a controller for handling API requests related to user management
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Dependencies injected via constructor
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        private readonly IEmployeeService _employeeService;

        // Constructor to initialize the dependencies
        public UserController(IMediator mediator, ILogger<UserController> logger, IEmployeeService employeeService)
        {
            _mediator = mediator;
            _logger = logger;
            _employeeService = employeeService;
        }

        // Endpoint to get all users
        [HttpGet]
        [Route("Getallusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                // Create and send the GetAllUsersQuery using MediatR
                var users = await _mediator.Send(new GetAllUsersQuery());
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log error and return a server error status
                _logger.LogError(ex, "Error getting all users");
                return StatusCode(500, "Internal server error");
            }
        }

        // Endpoint to get a user by their ID
        [HttpGet]
        [Route("GetUserbyId")]
        public async Task<IActionResult> GetUserById(string UserId)
        {
            try
            {
                // Create and send the GetUserByIdQuery using MediatR
                var user = await _mediator.Send(new GetUserByIdQuery(UserId));
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log error and return a server error status
                _logger.LogError(ex, "Error getting user by id: {id}", UserId);
                return StatusCode(500, "Internal server error");
            }
        }

        // Endpoint to update a user's information
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserInfoDto updatedUserInfoDto, [FromQuery] string updatedUserId)
        {
            try
            {
                // Create and send the UpdateUserCommand using MediatR
                var command = new UpdateUserCommand(updatedUserInfoDto, updatedUserId);
                var result = await _mediator.Send(command);

                if (result == null)
                {
                    return NotFound("Usernotfound");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log error and return a server error status
                _logger.LogError(ex, "Error updating user with id: {id}", updatedUserId);
                return StatusCode(500, "An error occurred while updating the user");
            }
        }

        // Endpoint to delete a user by their ID
        [HttpDelete("DeleteUserby{id}")]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            try
            {
                // Create and send the DeleteUserCommand using MediatR
                var user = await _mediator.Send(new DeleteUserCommand(id));

                if (user == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error and return a server error status
                _logger.LogError(ex, "Error deleting user with id: {id}", id);
                return StatusCode(500, "An error occurred while deleting the user");
            }
        }

        // Endpoint to change a user's password
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            // Change the user's password using the employee service
            var result = await _employeeService.ChangePasswordAsync(userId, currentPassword, newPassword);
            if (result.Succeeded)
            {
                return Ok("Password changed successfully");
            }

            return BadRequest(result.Errors);
        }
    }
}