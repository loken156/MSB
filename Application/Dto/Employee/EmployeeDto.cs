using Domain.Models.Driver;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Employee
{
    public class EmployeeDto
    {
        [Required] public Guid EmployeeId { get; set; }
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public List<string> Roles { get; set; } = new List<string>();

        //public ICollection<WareHouseModel> WareHouses { get; set; }
        //public ICollection<AddressModel> Addresses { get; set; }
        public ICollection<DriverModel> Drivers { get; set; } = new List<DriverModel>();
    }
}
