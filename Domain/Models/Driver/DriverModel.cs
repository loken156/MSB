using Domain.Models.Car;
using Domain.Models.Employee;
using Domain.Models.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Driver
{
    public class DriverModel
    {
        [Key]
        public Guid DriverId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid CarId { get; set; }
        public Guid OrderId { get; set; }

        public EmployeeModel Employee { get; set; }
        public CarModel Car { get; set; }
        public OrderModel Order { get; set; }
    }
}