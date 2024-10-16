﻿using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Repositories.AddressRepo;
using Infrastructure.Repositories.AdminRepo;
using Infrastructure.Repositories.BoxRepo;
using Infrastructure.Repositories.CarRepo;
using Infrastructure.Repositories.EmployeeRepo;
using Infrastructure.Repositories.OrderRepo;
using Infrastructure.Repositories.ShelfRepo;
using Infrastructure.Repositories.TimeSlotRepo;
using Infrastructure.Repositories.UserRepo;
using Infrastructure.Repositories.WarehouseRepo;
using Infrastructure.Services;
using Infrastructure.Services.Caching;
using Infrastructure.Services.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddDbContext<MSB_Database>(options =>
                   options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                       new MySqlServerVersion(new Version(8, 3, 0))));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IShelfRepository, ShelfRepository>();
            services.AddScoped<IBoxRepository, BoxRepository>();
            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();
            services.AddScoped<ILabelPrinterService, LabelPrinterService>();
            services.AddScoped<TimeSlotService>(); // Register the service TimeSlot Service

            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<IMessageSender>(provider =>
                 new EmailNotificationService(
                     smtpServer: "smtp.example.com",
                     port: 587, // SMTP port (587 for TLS)
                     senderEmail: "your-email@example.com",
                     senderPassword: "your-email-password")
             );

            services.AddScoped<INotificationService, MessagingNotificationService>();
            services.AddDbContext<MSB_Database>(options =>
                   options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                       new MySqlServerVersion(new Version(8, 0, 21)))
                   .EnableSensitiveDataLogging()

            );


            return services;

        }
    }
}