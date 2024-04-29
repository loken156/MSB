using Domain.Models.Car;
using MediatR;

namespace Application.Queries.Car.GetById
{
    public class GetCarByIdQuery : IRequest<CarModel>
    {
        public Guid CarId { get; set; }

        public GetCarByIdQuery(Guid carId)
        {
            CarId = carId;
        }
    }
}
