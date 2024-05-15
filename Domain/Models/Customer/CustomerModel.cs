using Domain.Interfaces;
using Domain.Models.Address;
using Domain.Models.Order;

namespace Domain.Models.Customer
{
    public class CustomerModel : IAppUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string EmployeeEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AddressModel> Addresses { get; set; }
        public string Role { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MembershipLevel { get; set; }
    }
}