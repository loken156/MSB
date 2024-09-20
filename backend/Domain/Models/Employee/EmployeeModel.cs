using Domain.Interfaces;
using Domain.Models.Address;
using Domain.Models.Order;
using Domain.Models.Warehouse;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Employee
{
    public class EmployeeModel : IAppUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public ICollection<AddressModel> Addresses { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
        // Handle multiple roles
        public ICollection<IdentityRole> Roles { get; set; } = new List<IdentityRole>();        
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public Guid WarehouseId { get; set; }
        //public WarehouseModel Warehouse { get; set; }
    }

}