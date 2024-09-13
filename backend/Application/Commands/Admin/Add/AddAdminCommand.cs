using Domain.Models.Admin;
using MediatR;

namespace Application.Commands.Admin.Add
{
    public class AddAdminCommand : IRequest<AdminModel>
    {
        public Guid AdminId { get; set; }
        public string Email { get; set; }
        
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public string Permissions { get; set; }
    }
}