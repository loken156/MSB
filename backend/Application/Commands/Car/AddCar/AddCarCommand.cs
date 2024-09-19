using Application.Dto.Car;
using MediatR;
namespace Application.Commands.Car.AddCar
{
    public class AddCarCommand : IRequest<CarDto>
    {
        public CarDto Car { get; set; }
        public AddCarCommand(CarDto car)
        {
            Car = car;
        }
    }
}