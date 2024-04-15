using Application.Dto.Register;
using Domain.Models.Address;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IdentityResult> Handle(RegistrationCommand request, CancellationToken cancellationToken )
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
