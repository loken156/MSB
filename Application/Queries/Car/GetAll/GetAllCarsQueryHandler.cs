using Domain.Models.Car;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Car
{
    public class GetAllCarsQueryHandler
    {

        private readonly IMSBDatabase _database;

        public GetAllCarsQueryHandler(IMSBDatabase database)
        {
            _database = database;
        }

        public async Task<List<CarModel>> Handle(GetAllCarsQuery query)
        {
            return await _database.Cars.ToListAsync();
        }
    }
}