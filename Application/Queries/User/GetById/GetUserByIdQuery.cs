using Domain.Interfaces;
using MediatR;

namespace Application.Queries.User.GetById
{
    public class GetUserByIdQuery : IRequest<IAppUser>
    {
        public GetUserByIdQuery(string userId)
        {

            Id = userId;
        }

        public string Id { get; }
    }
}