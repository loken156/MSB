using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Stripe
{
    public class LineItem
    {
        public string PriceId { get; set; }
        public int Quantity { get; set; }
    }
}
