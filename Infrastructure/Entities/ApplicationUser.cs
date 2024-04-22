using Domain.Interfaces;
using Domain.Models.Address;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser, IAppUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AddressModel> Addresses { get; set; }
    }
}