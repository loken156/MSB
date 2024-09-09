using Infrastructure.Repositories.CarRepo;

// This class resides in the Application layer and handles the command to update a car. 
// It interacts with the car repository in the Infrastructure layer to retrieve and update 
// the car entity based on the provided CarId. The class defines a Handle method to execute 
// the command, where it retrieves the existing car from the repository, updates its properties 
// with the values from the UpdatedCar property of the command, and then updates the car in the repository.

namespace Application.Commands.Car.UpdateCar
{
    public class UpdateCarCommandHandler
    {
        private readonly ICarRepository _carRepository;
        public UpdateCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task Handle(UpdateCarCommand command)
        {
            var existingCar = await _carRepository.GetCarById(command.CarId);
            if (existingCar != null)
            {
                existingCar.Volume = command.UpdatedCar.Volume;
                existingCar.Type = command.UpdatedCar.Type;
                existingCar.Availability = command.UpdatedCar.Availability;

                await _carRepository.UpdateCar(existingCar);
            }
        }
    }
}