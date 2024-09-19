using Application.Dto.Car;
using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;
using MediatR;

// This class handles the GetAllCarsQuery, responsible for retrieving all cars. It relies on an 
// ICarRepository instance provided via its constructor. The Handle method asynchronously processes 
// the query, fetching all cars from the repository and returning them as a list of CarModel objects.

namespace Application.Queries.Car
{
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, IEnumerable<CarModel>>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<CarModel>> Handle(GetAllCarsQuery query, CancellationToken cancellationToken)
        {
            var cars = await _carRepository.GetAllCars();
            return cars.ToList();
        }
    }
}