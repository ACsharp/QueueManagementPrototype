using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSolution.BusinessEntities
{
    public class OrderRequest
    {
        public Guid RequestId { get; set; }

        public string Name { get; set; }

        public short Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }
    }
}
