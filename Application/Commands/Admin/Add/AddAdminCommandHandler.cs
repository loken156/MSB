using Domain.Models.Admin;
using Infrastructure.Entities;
using Infrastructure.Repositories.AdminRepo;
using MediatR;
using Microsoft.AspNetCore.Identity;

// This class handles the command to add a new admin. It uses MediatR for processing the command,
// interacts with the admin repository to create the admin in the data source, and assigns the "Admin" role
// using the ASP.NET Core Identity framework.

namespace Application.Commands.Admin.Add
{
    public class AddAdminCommandHandler : IRequestHandler<AddAdminCommand, AdminModel>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddAdminCommandHandler(IAdminRepository adminRepository, UserManager<ApplicationUser> userManager)
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
                Email = request.Email,
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