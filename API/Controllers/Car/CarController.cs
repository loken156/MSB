using Application.Commands.Car.AddCar;
using Application.Commands.Car.DeleteCar;
using Application.Commands.Car.UpdateCar;
using Application.Dto.Car;
using Application.Dto.Driver;
using Application.Queries.Car;
using Application.Queries.Car.GetById;
using Domain.Models;
using Domain.Models.Car;
using Domain.Models.Driver;
using Infrastructure.Repositories.CarRepo;
using Infrastructure.Repositories.DriverRepo;
using Infrastructure.Repositories.OrderRepo;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers.Car
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly GetAllCarsQueryHandler _getAllCarsQueryHandler;
        private readonly GetCarByIdQueryHandler _getCarByIdQueryHandler;
        private readonly AddCarCommandHandler _addCarCommandHandler;
        private readonly DeleteCarCommandHandler _deleteCarCommandHandler;
        private readonly UpdateCarCommandHandler _updateCarCommandHandler;
        private readonly IOrderRepository _orderRepository;


        public CarController(ICarRepository carRepository, IDriverRepository driverRepository, GetAllCarsQueryHandler getAllCarsQueryHandler, GetCarByIdQueryHandler getCarByIdQueryHandler, AddCarCommandHandler addCarCommandHandler, DeleteCarCommandHandler deleteCarCommandHandler, UpdateCarCommandHandler updateCarCommandHandler, IOrderRepository orderRepository)
        {
            _carRepository = carRepository;
            _driverRepository = driverRepository;
            _getAllCarsQueryHandler = getAllCarsQueryHandler;
            _getCarByIdQueryHandler = getCarByIdQueryHandler;
            _addCarCommandHandler = addCarCommandHandler;
            _deleteCarCommandHandler = deleteCarCommandHandler;
            _updateCarCommandHandler = updateCarCommandHandler;
            _orderRepository = orderRepository;

        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCarById(Guid carId)
        {
            var query = new GetCarByIdQuery(carId);
            var car = await _getCarByIdQueryHandler.Handle(query);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var query = new GetAllCarsQuery();
            var cars = await _getAllCarsQueryHandler.Handle(query);
            return Ok(cars);
        }

        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CarDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new AddCarCommand(carDto);
            await _addCarCommandHandler.Handle(command);
            return Ok(carDto);
        }

        [HttpPut("{carId}")]
        public async Task<IActionResult> UpdateCar(Guid carId, [FromBody] CarDto carDto)
        {
            var command = new UpdateCarCommand(carId, carDto);
            await _updateCarCommandHandler.Handle(command);
            return NoContent();
        }


        [HttpDelete("{carId}")]
        public async Task<IActionResult> DeleteCar(Guid carId)
        {
            var command = new DeleteCarCommand(carId);
            await _deleteCarCommandHandler.Handle(command);
            return NoContent();
        }

        [HttpPost("{carId}/drivers")]
        public async Task<IActionResult> AddDriverToCar(Guid carId, [FromBody] DriverDto driverDto)
        {
            var car = await _carRepository.GetCarById(carId);
            if (car == null)
            {
                return NotFound();
            }

            var driverModel = MapToDriverModel(driverDto);

            // Add driver to car
            await _carRepository.AssignDriverToCar(car, driverModel);

            return Ok(car);
        }

        [HttpDelete("{carId}/drivers")]
        public async Task<IActionResult> DeleteDriverFromCar(Guid carId)
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

        [HttpPut("{carId}/drivers")]
        public async Task<IActionResult> ChangeDriverForCar(Guid carId, [FromBody] DriverDto driverDto)
        {
            var car = await _carRepository.GetCarById(carId);
            if (car == null)
            {
                return NotFound();
            }

            var driverModel = MapToDriverModel(driverDto);

            // Change the driver for the car
            car.DriverId = Guid.Parse(driverModel.Id);
            await _carRepository.UpdateCar(car);

            return Ok(car);
        }

        [HttpPost("{driverId}/take-order/{startTime}/{endTime}")]
        public async Task<IActionResult> TakeOrder(Guid driverId, [FromBody] Guid orderId, DateTime startTime, DateTime endTime)
        {
            var driver = await _driverRepository.GetDriverByIdAsync(driverId); ;
            if (driver == null)
            {
                return NotFound("Driver not found.");
            }

            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            var pickupTimeSlot = new TimeSlot { StartTime = startTime, EndTime = endTime };

            // Assign the order to the driver
            await _driverRepository.AssignOrderToDriver(driver, orderId, pickupTimeSlot);

            return Ok("Order successfully assigned to the driver.");
        }


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

        private DriverModel MapToDriverModel(DriverDto driverDto)
        {
            return new DriverModel
            {
                Id = driverDto.DriverId.ToString(),
                // Initialize other properties as needed
            };
        }
    }
}

