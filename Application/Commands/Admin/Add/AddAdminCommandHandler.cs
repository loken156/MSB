﻿using Domain.Models.Admin;
using Infrastructure.Repositories.AdminRepo;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Admin.Add
{
    public class AddAdminCommandHandler : IRequestHandler<AddAdminCommand, AdminModel>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AddAdminCommandHandler(IAdminRepository adminRepository, UserManager<IdentityUser> userManager)
        {
            _adminRepository = adminRepository;
            _userManager = userManager;
        }

        public async Task<AdminModel> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = new AdminModel
            {
                Id = request.AdminId.ToString(),
                UserName = request.Email,
                EmployeeEmail = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = "Admin",
                // Permissions = request.Permissions
            };

            var createdAdmin = await _adminRepository.CreateAdminAsync(admin);

            var user = await _userManager.FindByIdAsync(createdAdmin.Id);

            if (user != null)
            {
                // Assign the "Admin" role to the new admin
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return createdAdmin;
        }
    }
}
