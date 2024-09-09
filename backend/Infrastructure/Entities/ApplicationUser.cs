using Domain.Interfaces;
using Domain.Models.Address;
using Domain.Models.Order;
using Microsoft.AspNetCore.Identity;

// This class defines an application user entity that inherits from IdentityUser,
// adding additional properties such as FirstName, LastName, Email, and collections of Orders and Addresses.

namespace Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser, IAppUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } // Implement the missing property

        public ICollection<OrderModel> Orders { get; set; }
        public ICollection<AddressModel> Addresses { get; set; }
    }
}