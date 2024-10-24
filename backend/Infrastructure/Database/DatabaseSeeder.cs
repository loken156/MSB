using Domain.Models.Address;
using Domain.Models.Box;
using Domain.Models.Car;
using Domain.Models.Order;
using Domain.Models.Shelf;
using Domain.Models.Warehouse;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

// Mock database for seeding, should be edited and removed after usings have been verified.

namespace Infrastructure.Database
{
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
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
                new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Maple Street", UnitNumber="#01-02" , ZipCode = "112345", City = "Springfield", State = "Ohio", Country = "USA"},
                new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Oak Avenue", UnitNumber="#02-12" , ZipCode = "154321", City = "Willow Creek", State = "California", Country = "USA"},
                new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Elm Street", UnitNumber="#03-13" , ZipCode = "198765", City = "Oakville", State = "New York", Country = "USA"},
                new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Pine Street", UnitNumber="#04-14" , ZipCode = "167890", City = "Cedarville", State = "Texas", Country = "USA"}
            };
            for (int i = 0; i < users.Length; i++)
            {
                addresses[i].UserId = users[i].Id;
            }

            modelBuilder.Entity<ApplicationUser>().HasData(users);
            modelBuilder.Entity<AddressModel>().HasData(addresses);

            // Mock data for CarModels
            var cars = new CarModel[]
            {
                new CarModel { CarId = Guid.NewGuid(), Volume = 100, Type = "Truck", Availability = "Available" },
                new CarModel { CarId = Guid.NewGuid(), Volume = 200, Type = "Van", Availability = "Available" },
                new CarModel { CarId = Guid.NewGuid(), Volume = 300, Type = "Truck", Availability = "Available" },
                new CarModel { CarId = Guid.NewGuid(), Volume = 400, Type = "Van", Availability = "Available" },
            };
            modelBuilder.Entity<CarModel>().HasData(cars);

            // Mock data for OrderModels
            var orders = new OrderModel[]
            {
                  new OrderModel { OrderId = Guid.NewGuid(), UserId = users[0].Id, OrderDate = DateTime.Now, TotalCost = 1000, OrderStatus = "Created", OrderNumber = 2101011000, CarId = cars[0].CarId, AddressId = addresses[0].AddressId },
                  new OrderModel { OrderId = Guid.NewGuid(), UserId = users[1].Id, OrderDate = DateTime.Now, TotalCost = 2000, OrderStatus = "Created", OrderNumber = 2101011001, CarId = cars[1].CarId, AddressId = addresses[1].AddressId },
                  new OrderModel { OrderId = Guid.NewGuid(), UserId = users[2].Id, OrderDate = DateTime.Now, TotalCost = 3000, OrderStatus = "Created", OrderNumber = 2101011100, CarId = cars[2].CarId, AddressId = addresses[2].AddressId },
                  new OrderModel { OrderId = Guid.NewGuid(), UserId = users[3].Id, OrderDate = DateTime.Now, TotalCost = 4000, OrderStatus = "Created", OrderNumber = 2101010101, CarId = cars[3].CarId, AddressId = addresses[3].AddressId },
            };

            /*modelBuilder.Entity<OrderModel>().HasData(orders);
            //Mock data for BoxModel
            var boxes = new BoxModel[]
            {
                new BoxModel
                {
                    BoxId = Guid.NewGuid(),
                    Type = "Recycled",
                    TimesUsed = 2,
                    Stock = 20,
                    ImageUrl = "https://pngimg.com/uploads/spongebob/spongebob_PNG11.png",
                    UserNotes = "Bubbles",
                    OrderId = orders[0].OrderId,
                    Size = "Medium"
                },
                new BoxModel
                {
                    BoxId = Guid.NewGuid(),
                    Type = "Recycled",
                    TimesUsed = 0,
                    Stock = 10,
                    ImageUrl = "https://cdn.shopify.com/s/files/1/2393/5817/files/6eaedeb6-dd5a-4597-8976-247f08418c99.jpg?v=1692953727",
                    UserNotes = "Burgers",
                    OrderId = orders[1].OrderId,
                    Size = "XL"
                },
                new BoxModel
                {
                    BoxId = Guid.NewGuid(),
                    Type = "Recycled",
                    TimesUsed = 1,
                    Stock = 15,
                    ImageUrl = "https://cdn.shopify.com/s/files/1/2393/5817/files/SB_ES_Squid_002_EA_REV-S.jpg?v=1692953746",
                    UserNotes = "Money",
                    OrderId = orders[2].OrderId,
                    Size = "L"
                },
                new BoxModel
                {
                    BoxId = Guid.NewGuid(),
                    Type = "Recycled",
                    TimesUsed = 2,
                    Stock = 3,
                    ImageUrl = "https://cdn.shopify.com/s/files/1/2393/5817/files/renditionfile_6.jpg?v=1692953765",
                    UserNotes = "Chestnuts",
                    OrderId = orders[3].OrderId,
                    Size = "S"
                }
            };*/

            /*modelBuilder.Entity<BoxModel>().HasData(boxes);

            // Mock data for WarehouseModels
            var warehouses = new WarehouseModel[]
            {
                new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "Warehouse 1"},
                new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "Warehouse 2"},
                new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "Warehouse 3"},
                new WarehouseModel { WarehouseId = Guid.NewGuid(), WarehouseName = "Warehouse 4"},
            };
            modelBuilder.Entity<WarehouseModel>().HasData(warehouses);

            // Mock data for ShelfModels
            var shelves = new ShelfModel[]
            {
                new ShelfModel { ShelfId = Guid.NewGuid(), ShelfRows = 1, ShelfColumn = 1, Occupancy = true ,WarehouseId = warehouses[0].WarehouseId},
                new ShelfModel { ShelfId = Guid.NewGuid(), ShelfRows = 2, ShelfColumn = 2, Occupancy = true ,WarehouseId = warehouses[1].WarehouseId},
                new ShelfModel { ShelfId = Guid.NewGuid(), ShelfRows = 3, ShelfColumn = 3, Occupancy = true ,WarehouseId = warehouses[2].WarehouseId},
                new ShelfModel { ShelfId = Guid.NewGuid(), ShelfRows = 4, ShelfColumn = 4, Occupancy = true ,WarehouseId = warehouses[3].WarehouseId},
            };
            modelBuilder.Entity<ShelfModel>().HasData(shelves);*/
        }
    }
}