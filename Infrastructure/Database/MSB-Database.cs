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
    public class MSB_Database : IdentityDbContext<ApplicationUser>
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

            // Configure one-to-many relationship between ShelfModel and BoxModel
            modelBuilder.Entity<ShelfModel>()
                .HasMany(s => s.Boxes) // Shelf has many Boxes
                .WithOne(b => b.Shelf) // Each Box belongs to one Shelf
                .HasForeignKey(b => b.ShelfId) // Box holds the foreign key
                .OnDelete(DeleteBehavior.Cascade); // Optional: Configures cascade delete

            // Example of configuring other relationships
            modelBuilder.Entity<WarehouseModel>()
                .HasMany(w => w.Shelves) // Warehouse has many Shelves
                .WithOne(s => s.Warehouse) // Each Shelf belongs to one Warehouse
                .HasForeignKey(s => s.WarehouseId); // ForeignKey in ShelfModel

            // DatabaseSeeder.Seed(modelBuilder); // Uncomment this line to seed the database with mock data

            base.OnModelCreating(modelBuilder);
        }
    }
}