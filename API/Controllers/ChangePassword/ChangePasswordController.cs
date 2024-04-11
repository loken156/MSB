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

        public ChangePasswordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
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
    }
}