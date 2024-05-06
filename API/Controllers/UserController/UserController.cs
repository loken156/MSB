using Application.Commands.Users.DeleteUser;
using Application.Commands.Users.UpdateUser;
using Application.Dto.UpdateUserInfo;
using Application.Queries.User.GetAll;
using Application.Queries.User.GetById;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        private readonly IEmployeeService _employeeService;

        public UserController(IMediator mediator, ILogger<UserController> logger, IEmployeeService employeeService)
        {
            _mediator = mediator;
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("Get all users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _mediator.Send(new GetAllUsersQuery());
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetUser by Id")]
        public async Task<IActionResult> GetUserById(string UserId)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery(UserId));
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by id: {id}", UserId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("Update User")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserInfoDto updatedUserInfoDto, [FromQuery] string updatedUserId)
        {
            try
            {
                var command = new UpdateUserCommand(updatedUserInfoDto, updatedUserId);
                var result = await _mediator.Send(command);

                if (result == null)
                {
                    return NotFound("User not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with id: {id}", updatedUserId);
                return StatusCode(500, "An error occurred while updating the user");
            }
        }

        [HttpDelete("Delete User by {id}")]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            try
            {
                var user = await _mediator.Send(new DeleteUserCommand(id));

                if (user == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with id: {id}", id);
                return StatusCode(500, "An error occurred while deleting the user");
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var result = await _employeeService.ChangePasswordAsync(userId, currentPassword, newPassword);
            if (result.Succeeded)
            {
                return Ok("Password changed successfully");
            }

            return BadRequest(result.Errors);
        }
    }
}