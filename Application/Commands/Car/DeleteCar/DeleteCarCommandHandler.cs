using Infrastructure.Repositories.CarRepo;

// This class resides in the Application layer and handles the command to delete a car. 
// It interacts with the car repository in the Infrastructure layer to retrieve and delete 
// the car entity based on the provided CarId. The class defines a Handle method to execute 
// the command, where it retrieves the existing car from the repository and deletes it if found.

namespace Application.Commands.Car.DeleteCar
{
    public class DeleteCarCommandHandler
    {
        private readonly ICarRepository _carRepository;
        public DeleteCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task Handle(DeleteCarCommand command)
        {
            var existingCar = await _carRepository.GetCarById(command.CarId);
            if (existingCar != null)
            {
                await _carRepository.DeleteCar(existingCar);
            }
        }
    }
}