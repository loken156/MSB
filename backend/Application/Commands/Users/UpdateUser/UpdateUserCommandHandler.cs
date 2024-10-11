using Domain.Models.Address;
using Infrastructure.Entities;
using Infrastructure.Repositories.UserRepo;
using MediatR;

// This class resides in the Application layer and handles the command to update a user's information. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the user repository in the Infrastructure layer to retrieve the user entity 
// based on the provided UserId. If the user is not found, it throws a KeyNotFoundException. Otherwise, 
// it updates the user's properties with the values provided in the UpdateUserCommand, including email, 
// first name, last name, and phone number. It also updates the user's address if provided in the command. 
// The address update involves either updating the existing address or adding a new one if none exists. 
// After updating the user information, it saves the changes to the database and returns the updated 
// ApplicationUser entity.

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApplicationUser>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApplicationUser> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(command.UserId) as ApplicationUser;
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            // Initialize the Addresses collection if it's null
            user.Addresses ??= new List<AddressModel>();

            // Update properties if they are not null in the DTO
            user.Email = command.UpdateUserInfoDto.Email ?? user.Email;
            user.FirstName = command.UpdateUserInfoDto.FirstName ?? user.FirstName;
            user.LastName = command.UpdateUserInfoDto.LastName ?? user.LastName;
            user.PhoneNumber = command.UpdateUserInfoDto.PhoneNumber ?? user.PhoneNumber;

            // Update the Address if it's not null
            if (command.UpdateUserInfoDto.Address != null)
            {
                var addressDto = command.UpdateUserInfoDto.Address;
                // Assuming you want to update the first address or add a new one if none exists
                var addressToUpdate = user.Addresses.FirstOrDefault() ?? new AddressModel();
                if (!user.Addresses.Any())
                {
                    user.Addresses.Add(addressToUpdate);
                }

                // Update address properties
                addressToUpdate.StreetName = addressDto.StreetName ?? addressToUpdate.StreetName;
                addressToUpdate.UnitNumber = addressDto.ZipCode ?? addressToUpdate.UnitNumber;
                addressToUpdate.ZipCode = addressDto.ZipCode ?? addressToUpdate.ZipCode;
                // ... Update other address fields as necessary
            }

            await _userRepository.UpdateUserAsync(user);

            return user;
        }
    }
}