using Domain.Interfaces;
using Domain.Models.Address;
using Domain.Models.Car;
using Domain.Models.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Driver
{
    public class DriverModel : IAppUser
    {
        public DriverModel()
        {
            // Init to avoid null reference exceptions
            Orders = new List<OrderModel>();
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AddressModel> Addresses { get; set; }
        public string Role { get; set; }
        public Guid CurrentCarId { get; set; }
        public CarModel CurrentCar { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public ICollection<TimeSlot> Availability { get; set; }
    }
}