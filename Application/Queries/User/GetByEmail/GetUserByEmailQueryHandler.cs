using Application.Queries.User.GetByEmail;
using Domain.Interfaces;
using Infrastructure.Repositories.UserRepo;
using MediatR;

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