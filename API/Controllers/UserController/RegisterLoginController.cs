//using Application.Commands.Users.LogIn;
//using Application.Dto.LogIn;
//using Application.Dto.Register;
//using Application.Validators.UserValidator;
//using FluentValidation;
//using Infrastructure.Repositories.UserRepo;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers.UserController
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RegisterLoginController : ControllerBase
//    {
//        private readonly IMediator _mediator;
//        private readonly IConfiguration _configuration;
//        private readonly IUserRepository _userRepository;
//        private readonly UserValidations _userValidations;
//        private readonly LogInValidations _logInValidations;
//        private readonly ILogger<RegisterLoginController> _logging;

//        public RegisterLoginController(IMediator mediator, IConfiguration configuration, IUserRepository userRepository, UserValidations validations, LogInValidations logInValidations, ILogger<RegisterLoginController> logger)
//        {
//            _configuration = configuration;
//            _mediator = mediator;
//            _userRepository = userRepository;
//            _userValidations = validations;
//            _logInValidations = logInValidations;
//            _logging = logger;
//        }
//        //------------------------------------------------------------------------------------

//        [HttpPost]
//        [Route("Register")]
//        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
//        {
//            var validationResult = _userValidations.Validate(registerDto);
//            if (!validationResult.IsValid)
//            {
//                return BadRequest(validationResult.Errors);
//            }

//            try
//            {
//                var command = new AddUserCommand(registerDto);
//                var result = await _mediator.Send(command);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                _logging.LogError(ex, "Error occurred during registration.");
//                return StatusCode(500, new { Message = "An error occurred while processing your request." });
//            }
//        }

//        //------------------------------------------------------------------------------------

//        [HttpPost]
//        [Route("Login")]
//        public async Task<IActionResult> Login([FromBody] LogInDto logInDto)
//        {

//            var validationResult = _logInValidations.Validate(logInDto);
//            if (!validationResult.IsValid)
//            {
//                return BadRequest(validationResult.Errors);
//            }

//            try
//            {
//                var command = new UserLoginCommand(logInDto);
//                var user = await _mediator.Send(command);

//                if (user != null)
//                {
//                    return Ok(new { Message = "Login Successful" });
//                }
//                else
//                {
//                    return Unauthorized(new { Message = "Invalid Email or Password" });
//                }

//            }
//            catch (KeyNotFoundException)
//            {
//                _logging.LogWarning("Attempted login for non-existing user."); // Log with context, avoiding sensitive data
//                return NotFound(new { Message = "User not found" });
//            }
//            catch (UnauthorizedAccessException)
//            {
//                _logging.LogWarning("Unauthorized login attempt."); // Log without exposing sensitive details
//                return Unauthorized(new { Message = "Invalid Email or Password" });
//            }
//            catch (Exception ex)
//            {
//                _logging.LogError(ex, "Error occurred during login."); // Log error securely
//                return BadRequest(new { Message = "An error occurred while processing your request." });
//            }
//        }
//    }
//}