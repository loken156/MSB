using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;

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