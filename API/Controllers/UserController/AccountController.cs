using Application.Dto.LogIn;
using Application.Dto.Register;
using Domain.Models.Address;
using FluentValidation;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, IValidator<RegisterDto> registerValidator, IValidator<LogInDto> loginValidator, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var validationResult = _registerValidator.Validate(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Addresses = new List<AddressModel>
        {
            new AddressModel
            {
                StreetName = model.Address.StreetName,
                StreetNumber = model.Address.StreetNumber,
                Apartment = model.Address.Apartment,
                ZipCode = model.Address.ZipCode,
                Floor = model.Address.Floor,
                City = model.Address.City,
                State = model.Address.State,
                Country = model.Address.Country,
                Latitude = model.Address.Latitude,
                Longitude = model.Address.Longitude
            }
        }
            };
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

        [AllowAnonymous]
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

                // Generate JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                string secretKey = _configuration["JwtSettings:SecretKey"];
                if (secretKey == null)
                {
                    _logger.LogError("JwtSettings:SecretKey is not set in the configuration.");
                    return Unauthorized(new { Message = "Internal server error." });
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

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

                // Return the token
                return Ok(new { Token = tokenString });
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