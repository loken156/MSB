using Infrastructure.Repositories.CarRepo;
using MediatR;

// This class resides in the Application layer and handles the command to assign a driver to a car. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the car repository in the Infrastructure layer to retrieve the car 
// entity based on the provided CarId. If the car is not found, it throws an exception. 
// Otherwise, it returns, indicating successful execution of the command.

namespace Application.Commands.DriverCar
{
    public class AssignDriverToCarCommandHandler : IRequestHandler<AssignDriverToCarCommand>
    {
        private readonly ICarRepository _carRepository;

        public AssignDriverToCarCommandHandler(ICarRepository repository)
        {
            _carRepository = repository;

        }
        public async Task Handle(AssignDriverToCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetCarById(request.CarId);
            if (car == null)
            {
                throw new Exception("Car not found");
            }

            return;
        }

    }
}