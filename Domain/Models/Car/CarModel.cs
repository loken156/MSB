
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Car
{
    public class CarModel
    {
        [Key]
        public Guid CarId { get; set; }
        public double Volume { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Availability { get; set; } = string.Empty;



        public Guid? DriverId { get; set; }
        public string Employee { get; set; } = string.Empty;
    }
}