using Domain.Models.Address;
using Domain.Models.Admin;
using Domain.Models.Box;
using Domain.Models.Car;
using Domain.Models.Driver;
using Domain.Models.Employee;
using Domain.Models.Order;
using Domain.Models.Shelf;
using Domain.Models.Warehouse;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class MSB_Database : IdentityDbContext<ApplicationUser>, IMSBDatabase
    {
        public MSB_Database(DbContextOptions<MSB_Database> options) : base(options) { }

        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<EmployeeModel> Employees { get; set; }
        public virtual DbSet<AddressModel> Addresses { get; set; }
        public virtual DbSet<DriverModel> Drivers { get; set; }
        public virtual DbSet<CarModel> Cars { get; set; }
        public virtual DbSet<OrderModel> Orders { get; set; }
        public virtual DbSet<WarehouseModel> Warehouses { get; set; }
        public virtual DbSet<ShelfModel> Shelves { get; set; }
        public virtual DbSet<BoxModel> Boxes { get; set; }
        public virtual DbSet<AdminModel> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Database Relationships
            modelBuilder.Entity<AddressModel>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Addresses)
                .WithOne()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DriverModel>().OwnsMany(d => d.Availability);

            // DatabaseSeeder.Seed(modelBuilder); // Uncomment this line to seed the database with mock data

            base.OnModelCreating(modelBuilder);
        }
    }
}