using Application.Dto.AddShelf;
using Application.Dto.Adress;
using Application.Dto.Employee;
using Application.Dto.LogIn;
using Application.Dto.Register;
using Application.Validators.AddressValidator;
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
            services.AddTransient<IValidator<AddShelfDto>, ShelfValidations>();
            services.AddTransient<IValidator<EmployeeDto>, EmployeeValidations>();



            services.AddTransient<IValidator<LogInDto>, LogInDtoValidator>();


            return services;
        }

    }
}