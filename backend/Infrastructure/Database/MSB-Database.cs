using Domain.Models.Address;
using Domain.Models.Admin;
using Domain.Models.Box;
using Domain.Models.Car;
using Domain.Models.Employee;
using Domain.Models.Order;
using Domain.Models.Shelf;
using Domain.Models.Warehouse;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Database context for program

namespace Infrastructure.Database
{
    public class MSB_Database : IdentityDbContext<ApplicationUser>
    {
        public MSB_Database(DbContextOptions<MSB_Database> options) : base(options) { }

        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<EmployeeModel> Employees { get; set; }
        public virtual DbSet<AddressModel> Addresses { get; set; }

        public virtual DbSet<CarModel> Cars { get; set; }
        public virtual DbSet<OrderModel> Orders { get; set; }
        public virtual DbSet<WarehouseModel> Warehouses { get; set; }
        public virtual DbSet<ShelfModel> Shelves { get; set; }
        public virtual DbSet<BoxModel> Boxes { get; set; }
        public DbSet<AdminModel> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
                .HasOne(w => w.Address) // Warehouse has one Address
                .WithMany() // Address can have many Warehouses (or you can leave this out if no inverse navigation property)
                .HasForeignKey(w => w.AddressId);// Foreign key in WarehouseModel

            // DatabaseSeeder.Seed(modelBuilder); // Uncomment this line to seed the database with mock data

            base.OnModelCreating(modelBuilder);
        }
    }
}