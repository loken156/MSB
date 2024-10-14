using Domain.Models.Address;
using Infrastructure.Entities;
using Infrastructure.Repositories.UserRepo;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApplicationUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager; // Inject UserManager for normalizing the username

        public UpdateUserCommandHandler(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            // Fetch the user by ID
            var user = await _userRepository.GetUserByIdAsync(command.UserId) as ApplicationUser;
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Initialize the Addresses collection if it's null
            user.Addresses ??= new List<AddressModel>();

            // Update user properties from the DTO, if they are provided
            if (!string.IsNullOrEmpty(command.UpdateUserInfoDto.Email) && user.Email != command.UpdateUserInfoDto.Email)
            {
                // Update Email
                user.Email = command.UpdateUserInfoDto.Email;

                // Sync UserName with the new Email
                user.UserName = user.Email;

                // Normalize the UserName and update NormalizedUserName
                user.NormalizedUserName = _userManager.NormalizeName(user.Email);
            }

            user.FirstName = command.UpdateUserInfoDto.FirstName ?? user.FirstName;
            user.LastName = command.UpdateUserInfoDto.LastName ?? user.LastName;
            user.PhoneNumber = command.UpdateUserInfoDto.PhoneNumber ?? user.PhoneNumber;

            // Update the address if provided in the DTO
            if (command.UpdateUserInfoDto.Address != null)
            {
                var addressDto = command.UpdateUserInfoDto.Address;
                var addressToUpdate = user.Addresses.FirstOrDefault() ?? new AddressModel();

                if (!user.Addresses.Any())
                {
                    user.Addresses.Add(addressToUpdate);
                }

                addressToUpdate.StreetName = addressDto.StreetName ?? addressToUpdate.StreetName;
                addressToUpdate.UnitNumber = addressDto.UnitNumber ?? addressToUpdate.UnitNumber;
                addressToUpdate.ZipCode = addressDto.ZipCode ?? addressToUpdate.ZipCode;
            }

            // Use UserManager to update the user and persist the changes
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update the user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return user;
        }
    }
}
