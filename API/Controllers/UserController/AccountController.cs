using Application.Dto.LogIn;
using Application.Dto.Register;
using FluentValidation;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LogInDto> _loginValidator;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, IValidator<RegisterDto> registerValidator, IValidator<LoginDto> loginValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var validationResult = _registerValidator.Validate(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("User registered: {Email}", model.Email);
                return Ok(new { Message = "Registration successful" });
            }

            _logger.LogWarning("Registration failed for user: {Email}", model.Email);
            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogInDto model)
        {
            var validationResult = _loginValidator.Validate(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in: {Email}", model.Email);
                return Ok(new { Message = "Login successful" });
            }

            _logger.LogWarning("Invalid login attempt for user: {Email}", model.Email);
            return Unauthorized(new { Message = "Invalid login attempt" });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity.Name; // Get current user's name
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out: {UserName}", userName);
            return Ok(new { Message = "Logout successful" });
        }
    }
}
