using Domain.Interfaces;
using MediatR;

namespace Application.Queries.User.GetByEmail
{
    public class GetUserByEmailQuery : IRequest<IAppUser>
    {
        public string Email { get; set; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }


}