using Domain.Models.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Admin.Add
{
    public class AddAdminCommand : IRequest<AdminModel>
    {
        public Guid AdminId { get; set; }

    }

}