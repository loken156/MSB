using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;

// This class handles the GetAllCarsQuery, responsible for retrieving all cars. It relies on an 
// ICarRepository instance provided via its constructor. The Handle method asynchronously processes 
// the query, fetching all cars from the repository and returning them as a list of CarModel objects.

namespace Application.Queries.Car
{
    public class GetAllCarsQueryHandler
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<CarModel>> Handle(GetAllCarsQuery query)
        {
            var cars = await _carRepository.GetAllCars();
            return cars.ToList();
        }
    }
}