using Domain.Models.Car;
using Infrastructure.Repositories.CarRepo;

namespace Application.Queries.Car.GetById
{
    public class GetCarByIdQueryHandler
    {
        private readonly IMSBDatabase _database;
        private readonly ICarRepository _carRepository;

        public GetCarByIdQueryHandler(ICarRepository carRepository, IMSBDatabase database)
        {
            _carRepository = carRepository;
            _database = database;
        }

        public async Task<CarModel> Handle(GetCarByIdQuery query)
        {
            return await _database.Cars.FindAsync(query.CarId)
                   ?? throw new Exception($"No car found with id {query.CarId}");
        }

    }
}