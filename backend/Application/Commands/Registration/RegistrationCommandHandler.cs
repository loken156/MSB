using Domain.Models.Address;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

// This class resides in the Application layer and handles the command to register a new user. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the UserManager and SignInManager provided by ASP.NET Core Identity 
// to create a new ApplicationUser based on the registration data provided in the RegistrationCommand. 
// It also creates a new AddressModel for the user's address. After creating the user, it attempts 
// to sign in the user if the registration was successful and returns the result of the registration process.

namespace Application.Commands.Registration
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegistrationCommandHandler(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.RegDto.Email,
                Email = request.RegDto.Email,
                FirstName = request.RegDto.FirstName,
                LastName = request.RegDto.LastName,
                PhoneNumber = request.RegDto.PhoneNumber,
                Addresses = new List<AddressModel>
                {
                    new AddressModel
                    {
                        StreetName = request.RegDto.Address.StreetName,
                        StreetNumber = request.RegDto.Address.StreetNumber,
                        Apartment = request.RegDto.Address.Apartment,
                        ZipCode = request.RegDto.Address.ZipCode,
                        Floor = request.RegDto.Address.Floor,
                        City = request.RegDto.Address.City,
                        State = request.RegDto.Address.State,
                        Country = request.RegDto.Address.Country,
                    }
                }
            };


            var result = await _userManager.CreateAsync(user, request.RegDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;

        }




    }
}