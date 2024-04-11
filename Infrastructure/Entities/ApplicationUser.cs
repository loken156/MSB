using Domain.Models.Address;
using Domain.Models.Order;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities
{
    /// <summary>
    /// Represents the application user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the collection of addresses associated with the user.
        /// </summary>
        public ICollection<AddressModel> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of orders placed by the user.
        /// </summary>
        public ICollection<OrderModel> Orders { get; set; }
    }
}