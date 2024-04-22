using Infrastructure.Entities;
using Infrastructure.Repositories.UserRepo;
using MediatR;

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