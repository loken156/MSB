using Application.Commands.Car.AddCar;
using Application.Commands.Car.DeleteCar;
using Application.Commands.Car.UpdateCar;
using Application.Dto.Car;
using Application.Queries.Car;
using Application.Queries.Car.GetById;
using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;
using Infrastructure.Repositories.OrderRepo;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers.Car
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly GetAllCarsQueryHandler _getAllCarsQueryHandler;
        private readonly GetCarByIdQueryHandler _getCarByIdQueryHandler;
        private readonly AddCarCommandHandler _addCarCommandHandler;
        private readonly DeleteCarCommandHandler _deleteCarCommandHandler;
        private readonly UpdateCarCommandHandler _updateCarCommandHandler;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CarController> _logger;
        private readonly IMediator _mediator;


        public CarController(ICarRepository carRepository, GetAllCarsQueryHandler getAllCarsQueryHandler, GetCarByIdQueryHandler getCarByIdQueryHandler, AddCarCommandHandler addCarCommandHandler, DeleteCarCommandHandler deleteCarCommandHandler, UpdateCarCommandHandler updateCarCommandHandler, IOrderRepository orderRepository)
        {
            _carRepository = carRepository;
            _getAllCarsQueryHandler = getAllCarsQueryHandler;
            _getCarByIdQueryHandler = getCarByIdQueryHandler;
            _addCarCommandHandler = addCarCommandHandler;
            _deleteCarCommandHandler = deleteCarCommandHandler;
            _updateCarCommandHandler = updateCarCommandHandler;
            _orderRepository = orderRepository;

        }

        [HttpGet("{carId}")]
        [Route("Get car by id")]
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
        [Route("Get all cars")]
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
        [Route("Update Car")]
        public async Task<IActionResult> UpdateCar(Guid carId, [FromBody] CarDto carDto)
        {
            try
            {
                var command = new UpdateCarCommand(carId, carDto);
                await _updateCarCommandHandler.Handle(command);
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
                await _deleteCarCommandHandler.Handle(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting car with id: {id}", carId);
                return StatusCode(500, "An error occurred while deleting the car");
            }

        }



        [HttpDelete("{carId}/drivers")]
        [Route("Delete driver from car")]
        public async Task<IActionResult> DeleteDriverFromCar(Guid carId)
        {
            try
            {
                var car = await _carRepository.GetCarById(carId);
                if (car == null)
                {
                    return NotFound();
                }

                // Remove driver from car
                await _carRepository.RemoveDriverFromCar(car);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting driver from car with id: {id}", carId);
                return StatusCode(500, "An error occurred while deleting the driver from the car");
            }
        }



        //[HttpPost("{driverId}/take-order/{startTime}/{endTime}")]
        //[Route("Take order")]
        //public async Task<IActionResult> TakeOrder(Guid driverId, [FromBody] Guid orderId, DateTime startTime, DateTime endTime)
        //{
        //    var driver = await _driverRepository.GetDriverByIdAsync(driverId); ;
        //    if (driver == null)
        //    {
        //        return NotFound("Driver not found.");
        //    }

        //    var order = await _orderRepository.GetOrderByIdAsync(orderId);
        //    if (order == null)
        //    {
        //        return NotFound("Order not found.");
        //    }

        //    var pickupTimeSlot = new TimeSlot { StartTime = startTime, EndTime = endTime };

        //    // Assign the order to the driver
        //    await _driverRepository.AssignOrderToDriver(driver, orderId, pickupTimeSlot);

        //    return Ok("Order successfully assigned to the driver.");
        //}

        private CarModel MapToCarModel(CarDto carDto)
        {
            return new CarModel
            {
                CarId = carDto.CarId,
                Volume = carDto.Volume,
                Type = carDto.Type,
                Availability = carDto.Availability,

            };
        }


    }
}