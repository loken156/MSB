using Application.Dto.Car;
using Application.Dto.Employee;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Driver
{
    public class DriverDto
    {
        [Required] public Guid DriverId { get; set; }
        [Required] public Guid EmployeeId { get; set; }
        [Required] public Guid CarId { get; set; }
    }

    public class DriverDetailDto : DriverDto
    {
        public EmployeeDto Employee { get; set; }
        public CarDto Car { get; set; }
    }

}