
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

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
        public string Employee { get; set; }
    }
}