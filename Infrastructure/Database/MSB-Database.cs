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
        public DbSet<AdminModel> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mock data for UserModels
            var users = new ApplicationUser[]
            {
            new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "Adam@gmail.com", FirstName = "Adam", LastName = "Andersson", PhoneNumber = "0735097384", PasswordHash = "Adam123",  },
            new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "Bertil@gmail.com", FirstName = "Bertil", LastName = "Bengtsson", PhoneNumber = "0735097384", PasswordHash = "Bertil123" },
            new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "Cecar@gmail.com", FirstName = "Cecar", LastName = "Citron", PhoneNumber = "0735097384", PasswordHash = "Cecar123" },
            new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "Erik@gmail.com", FirstName = "Erik", LastName = "Eriksson", PhoneNumber = "0735097384", PasswordHash = "Erik123" },
            };


            // Mock data for AddressModels
            var addresses = new AddressModel[]
            {
                new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Maple Street", StreetNumber ="21" , Apartment = "Apt 3B", ZipCode = "12345", Floor = "2nd", City = "Springfield", State = "Ohio", Country = "USA", Latitude = 39.9266, Longitude = -83.8064 },
                new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Oak Avenue", StreetNumber ="22" ,Apartment = "Apt 2A", ZipCode = "54321", Floor = "Ground Floor", City = "Willow Creek", State = "California", Country = "USA", Latitude = 37.7833, Longitude = -122.4167 },
                new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Elm Street", StreetNumber ="23" , Apartment = "Apt 5C", ZipCode = "98765", Floor = "3rd", City = "Oakville", State = "New York", Country = "USA", Latitude = 40.7128, Longitude = -74.0060 },
                new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Pine Street", StreetNumber ="24", Apartment = "Apt 10D", ZipCode = "67890", Floor = "4th", City = "Cedarville", State = "Texas", Country = "USA", Latitude = 31.9686, Longitude = -99.9018 }
            };

            for (int i = 0; i < users.Length; i++)
            {
                addresses[i].UserId = users[i].Id;
            }

            modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Addresses)
            .WithOne(a => a.User as ApplicationUser)
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