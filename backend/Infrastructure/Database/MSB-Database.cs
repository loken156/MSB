using Domain.Models.Address;
using Domain.Models.Admin;
using Domain.Models.Box;
using Domain.Models.Car;
using Domain.Models.Employee;
using Domain.Models.BoxType;
using Domain.Models.Order;
using Domain.Models.Shelf;
using Domain.Models.TimeSlot;
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
        
        public DbSet<BoxTypeModel> BoxTypes { get; set; }
        public DbSet<TimeSlotModel> TimeSlots { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Addresses)
                .WithOne()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Configure one-to-many relationship between OrderModel and BoxModel
            modelBuilder.Entity<OrderModel>()
                .HasMany(o => o.Boxes)      // An Order can have many Boxes
                .WithOne(b => b.Order)      // Each Box belongs to one Order
                .HasForeignKey(b => b.OrderId) // Foreign key in BoxModel
                .OnDelete(DeleteBehavior.Cascade)  // Optional: cascade delete, removing order will remove associated boxes
                .IsRequired(false); // Make the foreign key nullable

            
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
            
            // Configure Employee to Role relationship
            modelBuilder.Entity<EmployeeModel>()
                .HasMany(e => e.Roles) // Employee has many roles
                .WithMany() // A role can have many employees
                .UsingEntity(j => j.ToTable("EmployeeRoles")); // Name the join table "EmployeeRoles"


            // DatabaseSeeder.Seed(modelBuilder); // Uncomment this line to seed the database with mock data

            modelBuilder.Entity<BoxTypeModel>()
                .HasMany(bt => bt.Boxes)
                .WithOne(b => b.BoxType) // Link back to BoxType
                .HasForeignKey(b => b.BoxTypeId) // Use BoxTypeId as the foreign key
                .OnDelete(DeleteBehavior.Restrict); // Optional: prevent cascade delete
            
            // Configure the relationship between ApplicationUser and OrderModel
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Orders)  // A user can have many orders
                .WithOne()                // Order has one user (no navigation property back)
                .HasForeignKey(o => o.UserId)  // Foreign key in OrderModel
                .OnDelete(DeleteBehavior.Cascade); // Optional: Set up cascade delete behavior
        }
    }
}