using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSolution.BusinessEntities
{
    public class ConfirmedOrder : OrderRequest 
    {
        public int OrderId { get; set; }
    }
}
