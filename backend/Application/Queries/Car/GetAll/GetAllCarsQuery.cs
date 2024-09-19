using Application.Dto.Car;
using Domain.Models.Car;
using MediatR;

namespace Application.Queries.Car
{
    public class GetAllCarsQuery : IRequest<IEnumerable<CarModel>>
    {

    }
}