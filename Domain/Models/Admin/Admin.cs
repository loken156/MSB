using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Admin
{
    public class Admin
    {
        public Guid AdminId { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
    }
}
