using Application.Commands.Users.ChangePassword;
using Application.Dto.ChangePassword;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ChangePassword
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChangePasswordController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ChangePasswordController> _logger;

        public ChangePasswordController(IMediator mediator, Logger<ChangePasswordController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                var command = new ChangePasswordCommand(changePasswordDto.UserId, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
                var result = await _mediator.Send(command);
                if (result)
                {
                    return Ok(new { Message = "Password changed successfully" });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to change password" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while changing password");
                return StatusCode(500, new { Message = "Internal server error" });

            }



        }
    }
}