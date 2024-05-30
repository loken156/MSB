using Domain.Interfaces;
using Infrastructure.Repositories.UserRepo;
using MediatR;

// This class handles the GetAllUsersQuery, responsible for retrieving all users from the database.
// It depends on an IUserRepository instance provided via its constructor for data access.
// The Handle method asynchronously processes the query, retrieving all users from the repository.
// If no users are found, it throws an InvalidOperationException.

namespace Application.Queries.User.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<IAppUser>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<IAppUser>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<IAppUser> allUsersFromDatabase = await _userRepository.GetAllUsersAsync();
            if (allUsersFromDatabase == null)
            {
                throw new InvalidOperationException("No Users was found");
            }
            return allUsersFromDatabase;
        }
    }
}