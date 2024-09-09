using Infrastructure.Entities;
using Infrastructure.Repositories.UserRepo;
using MediatR;

// This class resides in the Application layer and handles the command to delete a user. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the user repository in the Infrastructure layer to retrieve and 
// delete the user entity based on the provided UserId. If the user is not found, it throws an 
// InvalidOperationException. Otherwise, it deletes the user from the database and returns the 
// deleted user entity. If an error occurs during the process, it throws an exception with an 
// appropriate error message for error handling and logging purposes.

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApplicationUser>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApplicationUser> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser userToDelete = await _userRepository.GetUserByIdAsync(request.Id) as ApplicationUser
                                               ?? throw new InvalidOperationException("No user with the given id was found");

                await _userRepository.DeleteUserAsync(request.Id);

                return userToDelete;
            }
            catch (Exception ex)
            {
                var newException = new Exception($"An Error occurred when deleting user with id {request.Id}", ex);
                throw newException;
            }
        }
    }
}