using Domain.Interfaces;
using Domain.Models.Address;
using Domain.Models.Warehouse;

namespace Domain.Models.Employee
{
    public class EmployeeModel : IAppUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<AddressModel> Addresses { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public Guid WarehouseId { get; set; }
        public WarehouseModel Warehouse { get; set; }
    }
}