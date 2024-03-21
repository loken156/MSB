﻿using Infrastructure.Database;
using Infrastructure.Repositories.DriverRepo;
using Infrastructure.Repositories.EmployeeRepo;
using Infrastructure.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();

            services.AddDbContext<MSB_Database>(options =>
                   options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                       new MySqlServerVersion(new Version(8, 3, 0)))
            );


            return services;

        }




    }
}
