using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Stripe
{
    public class CheckoutSessionRequest
    {
        public List<LineItem> LineItems { get; set; }
        public string Mode { get; set; }
    }
}
