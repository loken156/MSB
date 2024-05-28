using Application.Commands.Registration;
using Application.Dto.LogIn;
using Application.Dto.Register;
using FluentValidation;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    // Define the route and make this a controller for handling API requests related to account management
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // Dependencies injected via constructor
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LogInDto> _loginValidator;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        // Constructor to initialize the dependencies
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, IValidator<RegisterDto> registerValidator, IValidator<LogInDto> loginValidator, IConfiguration configuration, IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _configuration = configuration;
            _mediator = mediator;
        }

        // Endpoint to register a new user
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto regDto)
        {
            try
            {
                // Validate the RegisterDto using FluentValidation
                var validationResult = _registerValidator.Validate(regDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                // Instantiate the RegistrationCommand with RegisterDto from the method parameter
                var command = new RegistrationCommand(regDto);
                var result = await _mediator.Send(command);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User registered: {EmployeeEmail}", regDto.Email);
                    return Ok(new { Message = "User registered successfully." });
                }
                else
                {
                    // Log the reasons for failure
                    _logger.LogWarning("Registration failed: {EmployeeEmail}, Errors: {Errors}", regDto.Email, result.Errors);
                    return BadRequest(new { Message = "Registration failed.", Errors = result.Errors });
                }
            }
            catch (Exception ex)
            {
                // Log error and return a server error status
                _logger.LogError(ex, "An error occurred while registering the user: {EmployeeEmail}", regDto.Email);
                return StatusCode(500, new { Message = "Internal server error." });
            }
        }

        // Endpoint to log in a user
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogInDto model)
        {
            // Validate the LogInDto using FluentValidation
            var validationResult = _loginValidator.Validate(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Attempt to sign in the user with provided credentials
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in: {EmployeeEmail}", model.Email);

                // Generate JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                string secretKey = _configuration["JwtSettings:SecretKey"];
                if (secretKey == null)
                {
                    _logger.LogError("JwtSettings:SecretKey is not set in the configuration.");
                    return Unauthorized(new { Message = "Internal server error." });
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                // Define the token descriptor
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, model.Email)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = _configuration["JwtSettings:Issuer"],
                    Audience = _configuration["JwtSettings:Audience"],
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Return the generated token
                return Ok(new { Token = tokenString });
            }

            _logger.LogWarning("Invalid login attempt for user: {EmployeeEmail}", model.Email);
            return Unauthorized(new { Message = "Invalid login attempt" });
        }

        // Endpoint to log out a user
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity.Name; // Get the current user's name
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out: {UserName}", userName);
            return Ok(new { Message = "Logout successful" });
        }
    }
}