using Infrastructure.Entities;
using MediatR;

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest<ApplicationUser>
    {
        public DeleteUserCommand(string id)
        {
            Id = id;
        }
        public string Id { get; }
    }
}
