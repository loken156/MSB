using Domain.Models.Driver;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Car
{
    public class CarModel
    {
        [Key]
        public Guid CarId { get; set; }
        public double Volume { get; set; }
        public string Type { get; set; }
        public string Availability { get; set; }

        public Guid? DriverId { get; set; }
        public DriverModel Driver { get; set; }
    }
}