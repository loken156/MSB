using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;
using MediatR;

namespace Application.Queries.Car.GetById
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarModel>
    {
        private readonly ICarRepository _carRepository;

        public GetCarByIdQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<CarModel> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var carId = request.CarId;
                var car = await _carRepository.GetCarById(carId);
                if (car == null)
                {
                    throw new KeyNotFoundException($"A car with the id {carId} was not found.");
                }
                return car;
            }
            catch (Exception ex)
            {
                // Handle exceptions accordingly
                throw new Exception("Error occurred while fetching car by ID", ex);
            }
        }

    }
}
