using Domain.Interfaces;
using Domain.Models.Address;
using Domain.Models.Order;

namespace Domain.Models.Admin
{
    public class AdminModel : IAppUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AddressModel> Addresses { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
        public string Role { get; set; }
        public ICollection<string> Permissions { get; set; }

        // Example Permissions:
        // 1.	ManageUsers: This permission allows the admin to create, update, delete, and view users.
        // 2.	ManageRoles: This permission allows the admin to create, update, delete, and view roles.
        // 3.	ManageOrders: This permission allows the admin to create, update, delete, and view orders.
        // 4.	ManageProducts: This permission allows the admin to create, update, delete, and view products.
        // 5.	ManageWarehouses: This permission allows the admin to create, update, delete, and view warehouses.
        // 6.	ViewReports: This permission allows the admin to view various reports.
        // 7.	ManageSettings: This permission allows the admin to change application settings.
    }
}
