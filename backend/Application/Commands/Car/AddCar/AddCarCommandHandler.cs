using Application.Dto.Car;
using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;

// This class resides in the Application layer and handles the command to add a new car. 
// It interacts with the car repository in the Infrastructure layer to persist the new car entity. 
// The class does not use MediatR for command processing. It defines a Handle method to execute 
// the command, which internally maps the incoming CarDto to a CarModel and adds it to the repository.

namespace Application.Commands.Car.AddCar
{
    public class AddCarCommandHandler
    {
        private readonly ICarRepository _carRepository;
        public AddCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task Handle(AddCarCommand command)
        {
            var carModel = MapToCarModel(command.Car);
            await _carRepository.AddCar(carModel);
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
    }
}