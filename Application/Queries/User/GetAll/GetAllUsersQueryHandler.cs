using Domain.Interfaces;
using Infrastructure.Repositories.UserRepo;
using MediatR;

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
