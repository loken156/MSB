using Domain.Models.Driver;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Car
{
    public class CarModel
    {
        [Key]
        public Guid CarId { get; set; }
        public double Volume { get; set; }
        public string Type { get; set; }
        public string Availability { get; set; }

        public ICollection<DriverModel> Drivers { get; set; }
    }
}