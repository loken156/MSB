using Domain.Models.Address;
using Domain.Models.Driver;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Employee
{
    public class EmployeeModel
    {
        [Key]
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;


        //public ICollection<WareHouseModel> WareHouses { get; set; }
        //public ICollection<AddressModel> Addresses { get; set; }
        public ICollection<DriverModel> Drivers { get; set; }
    }
}