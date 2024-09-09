using MediatR;

namespace Application.Commands.DriverCar
{
    public class AssignDriverToCarCommand : IRequest
    {
        public Guid DriverId { get; set; }
        public Guid CarId { get; set; }

        public AssignDriverToCarCommand(Guid carId, Guid driverId)
        {
            CarId = carId;
            DriverId = driverId;
        }
    }
}