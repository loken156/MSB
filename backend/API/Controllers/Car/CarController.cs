using Application.Commands.Car.AddCar;
using Application.Commands.Car.DeleteCar;
using Application.Commands.Car.UpdateCar;
using Application.Dto.Car;
using Application.Queries.Car;
using Application.Queries.Car.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Car
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CarController> _logger;

        public CarController(IMediator mediator, ILogger<CarController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCarById(Guid carId)
        {
            try
            {
                var query = new GetCarByIdQuery(carId);
                var car = await _mediator.Send(query);
                if (car == null)
                {
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting car by id");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("Getallcars")]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                var query = new GetAllCarsQuery();
                var cars = await _mediator.Send(query);
                return Ok(cars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all cars");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("AddCar")]
        public async Task<IActionResult> AddCar([FromBody] CarDto carDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var command = new AddCarCommand(carDto);
                await _mediator.Send(command);
                return Ok(carDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding car with command: {Command}", carDto);
                return StatusCode(500, "An error occurred while adding the car");
            }
        }

        [HttpPut("{carId}")]
        public async Task<IActionResult> UpdateCar(Guid carId, [FromBody] CarDto carDto)
        {
            try
            {
                var command = new UpdateCarCommand(carId, carDto);
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating car with id: {id}", carId);
                return StatusCode(500, "An error occurred while updating the car");
            }
        }

        [HttpDelete("{carId}")]
        public async Task<IActionResult> DeleteCar(Guid carId)
        {
            try
            {
                var command = new DeleteCarCommand(carId);
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting car with id: {id}", carId);
                return StatusCode(500, "An error occurred while deleting the car");
            }
        }
    }
}