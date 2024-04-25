using Domain.Models.Admin;
using MediatR;

namespace Application.Commands.Admin.Add
{
    public class AddAdminCommand : IRequest<AdminModel>
    {
        public Guid AdminId { get; set; }

    }

}