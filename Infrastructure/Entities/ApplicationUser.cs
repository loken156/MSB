using Domain.Interfaces;
using Domain.Models.Address;
using Domain.Models.Order;
using Microsoft.AspNetCore.Identity;

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