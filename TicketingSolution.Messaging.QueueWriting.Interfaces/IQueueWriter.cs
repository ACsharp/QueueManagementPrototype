using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSolution.Messaging.QueueWriting.Interfaces
{
    public interface IQueueWriter
    {
        Task CreateOrderRequestAsync(BusinessEntities.OrderRequest orderRequest);
    }
}
