using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSolution.DataAccess.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public Guid RequestId { get; set; }

        public string Name { get; set; }

        public short Quantity { get; set; }

        public DateTime EventDate { get; set; }
    }
}
