using Application.Queries.User.GetByEmail;
using Domain.Interfaces;
using Infrastructure.Repositories.UserRepo;
using MediatR;

// This class handles the GetUserByEmailQuery, responsible for retrieving a user by their email from the database.
// It depends on an IUserRepository instance provided via its constructor for data access.
// The Handle method asynchronously processes the query, retrieving the user with the specified email from the repository.
// If the email is null or empty, it throws an ArgumentException.
// If no user is found with the specified email, it throws a KeyNotFoundException.

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, IAppUser>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IAppUser> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ArgumentException("Username cannot be null or empty", nameof(request.Email));
        }

        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with EmployeeEmail '{request.Email}' couldn't be found");
        }

        return user;
    }
}