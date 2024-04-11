using Domain.Interfaces;
using Infrastructure.Repositories.UserRepo;
using MediatR;

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