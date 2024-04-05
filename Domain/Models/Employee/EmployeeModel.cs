using Domain.Models.Driver;
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
        public List<string> Roles { get; set; } = new List<string>();

        //public ICollection<WareHouseModel> WareHouses { get; set; }
        //public ICollection<AddressModel> Addresses { get; set; }
        public ICollection<DriverModel> Drivers { get; set; }
    }
}

