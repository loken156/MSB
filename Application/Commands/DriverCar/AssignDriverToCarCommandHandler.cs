using Infrastructure.Repositories.CarRepo;
using Infrastructure.Repositories.DriverRepo;
using MediatR;

namespace Application.Commands.DriverCar
{
    public class AssignDriverToCarCommandHandler : IRequestHandler<AssignDriverToCarCommand>
    {
        private readonly ICarRepository _carRepository;
        private readonly IDriverRepository _driverRepository;

        public AssignDriverToCarCommandHandler(ICarRepository repository, IDriverRepository driverRepository)
        {
            _carRepository = repository;
            _driverRepository = driverRepository;
        }


        public async Task Handle(AssignDriverToCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetCarById(request.CarId);
            if (car == null)
            {
                throw new Exception("Car not found");
            }

            var driver = await _driverRepository.GetDriverByIdAsync(request.DriverId);
            if (driver == null)
            {
                throw new Exception("Driver not found");
            }

            await _carRepository.AssignDriverToCar(car, driver);

            return;
        }

    }
}