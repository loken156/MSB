using Domain.Interfaces;
using Infrastructure.Repositories.UserRepo;
using MediatR;

// This class handles the GetUserByIdQuery, responsible for retrieving a user by their ID from the database.
// It depends on an IUserRepository instance provided via its constructor for data access.
// The Handle method asynchronously processes the query, retrieving the user with the specified ID from the repository.
// If no user is found with the specified ID, it returns null.
// If an exception occurs during the process, it throws a new Exception with the error message.

namespace Application.Queries.User.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, IAppUser>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }


        public async Task<IAppUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            IAppUser wantedUser = await _userRepository.GetUserByIdAsync(request.Id);
            try
            {
                if (wantedUser == null)
                {
                    return null!;
                }
                return wantedUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



    }
}