using Application.Dto.Adress;
using Application.Dto.Box;
using Application.Dto.Employee;
using Application.Dto.LogIn;
using Application.Dto.Register;
using Application.Dto.Shelf;
using Application.Validators.AddressValidator;
using Application.Validators.BoxValidator;
using Application.Validators.EmployeeValidator;
using Application.Validators.ShelfValidator;
using Application.Validators.UserValidator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient<IValidator<RegisterDto>, UserValidations>();
            services.AddTransient<IValidator<AddressDto>, AddressValidations>();
            services.AddTransient<IValidator<BoxDto>, BoxValidator>();
            services.AddTransient<IValidator<ShelfDto>, ShelfValidations>();
            services.AddTransient<IValidator<EmployeeDto>, EmployeeValidations>();
            services.AddTransient<IValidator<LogInDto>, LogInDtoValidator>();

            // Register IAddressValidations with its implementation
            services.AddScoped<IAddressValidations, AddressValidations>();

            return services;
        }

    }
}