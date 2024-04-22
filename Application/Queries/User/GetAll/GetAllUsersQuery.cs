using Domain.Interfaces;
using MediatR;

namespace Application.Queries.User.GetAll
{
    public class GetAllUsersQuery : IRequest<List<IAppUser>>
    {

    }
}