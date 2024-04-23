using Domain.Models.Address;
using Domain.Models.Order;

namespace Domain.Interfaces
{
    public interface IAppUser
    {
        string Id { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        ICollection<AddressModel> Addresses { get; set; }
        ICollection<OrderModel> Orders { get; set; }
    }
}