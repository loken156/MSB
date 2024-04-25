using Domain.Models.Address;
using Domain.Models.Admin;
using Domain.Models.Box;
using Domain.Models.Car;
using Domain.Models.Employee;
using Domain.Models.Order;
using Domain.Models.Shelf;
using Domain.Models.Warehouse;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

public interface IMSBDatabase
{
    DbSet<ApplicationUser> Users { get; set; }
    DbSet<EmployeeModel> Employees { get; set; }
    DbSet<AddressModel> Addresses { get; set; }
    DbSet<CarModel> Cars { get; set; }
    DbSet<OrderModel> Orders { get; set; }
    DbSet<WarehouseModel> Warehouses { get; set; }
    DbSet<ShelfModel> Shelves { get; set; }
    DbSet<BoxModel> Boxes { get; set; }
    DbSet<AdminModel> Admins { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}