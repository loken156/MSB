using Application.Dto.Car;
using Application.Dto.Driver;
using Domain.Models.Driver;
using Infrastructure.Repositories.DriverRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Driver
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public DriversController(IDriverRepository driverRepository, UserManager<IdentityUser> userManager)
        {
            _driverRepository = driverRepository;
            _userManager = userManager;
        }

        // GET: api/Drivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDetailDto>>> GetDrivers()
        {
            var drivers = await _driverRepository.GetAllDrivers();

            var driverDtos = drivers.Select(driver => new DriverDetailDto
            {
                DriverId = new Guid(driver.Id),
                EmployeeId = new Guid(driver.UserName), // Assuming UserName is used as EmployeeId
                CarId = driver.CurrentCarId,
                LicenseNumber = driver.LicenseNumber
            });

            return Ok(driverDtos);
        }


        // POST: api/Drivers
        [HttpPost]
        public async Task<ActionResult<DriverDetailDto>> PostDriver(DriverDto driverDto)
        {
            var driver = new DriverModel
            {
                Id = driverDto.DriverId.ToString(),
                UserName = driverDto.EmployeeId.ToString(), // Assuming EmployeeId is used as UserName
                Email = driverDto.EmployeeId.ToString(), // Assuming EmployeeId is used as Email
                FirstName = "", // Add appropriate value
                LastName = "", // Add appropriate value
                CurrentCarId = driverDto.CarId,
                LicenseNumber = driverDto.LicenseNumber
            };

            _driverRepository.AddDriver(driver);

            var user = await _userManager.FindByIdAsync(driver.Id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Assign the "Driver" role to the new driver
            var result = await _userManager.AddToRoleAsync(user, "Driver");

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
        }

        // PUT: api/Drivers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver(Guid id, DriverDto driverDto)
        {
            if (id != driverDto.DriverId)
            {
                return BadRequest();
            }

            var driver = new DriverModel
            {
                Id = driverDto.DriverId.ToString(),
                UserName = driverDto.EmployeeId.ToString(), // Assuming EmployeeId is used as UserName
                Email = driverDto.EmployeeId.ToString(), // Assuming EmployeeId is used as Email
                FirstName = "", // Add appropriate value
                LastName = "", // Add appropriate value
                CurrentCarId = driverDto.CarId,
                LicenseNumber = driverDto.LicenseNumber
            };

            await _driverRepository.UpdateDriver(driver);

            var user = await _userManager.FindByIdAsync(driver.Id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Check if the user is already in the "Driver" role
            var isInRole = await _userManager.IsInRoleAsync(user, "Driver");

            if (!isInRole)
            {
                // If not, assign the "Driver" role to the user
                var result = await _userManager.AddToRoleAsync(user, "Driver");

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }

            return NoContent();
        }


        // DELETE: api/Drivers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DriverDto>> DeleteDriver(Guid id)
        {
            var driver = await _driverRepository.GetDriverByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            _driverRepository.DeleteDriver(id);

            // You can return the deleted driver DTO if needed
            return Ok(new DriverDto
            {
                DriverId = new Guid(driver.Id),
                EmployeeId = new Guid(driver.UserName), // Assuming UserName is used as EmployeeId
                CarId = driver.CurrentCarId,
                LicenseNumber = driver.LicenseNumber
            });
        }

        // GET: api/Drivers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDetailDto>> GetDriver(Guid id)
        {
            var driver = await _driverRepository.GetDriverByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            var driverDto = new DriverDetailDto
            {
                DriverId = new Guid(driver.Id),
                EmployeeId = new Guid(driver.UserName), // Assuming UserName is used as EmployeeId
                CarId = driver.CurrentCarId,
                LicenseNumber = driver.LicenseNumber
            };

            return Ok(driverDto);
        }

        [HttpGet("AvailablePickupTimes")]
        public async Task<IActionResult> GetAvailablePickupTimes()
        {
            var drivers = await _driverRepository.GetAllDrivers();
            var availableTimes = drivers.SelectMany(d => d.Availability).ToList();
            return Ok(availableTimes);
        }
    }
}
