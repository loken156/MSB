using Infrastructure.Repositories.CarRepo;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

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