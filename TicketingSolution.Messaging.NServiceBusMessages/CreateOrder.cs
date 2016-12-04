using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSolution.Messaging.NServiceBusMessages
{
    public class CreateOrder : ICommand
    {
        public BusinessEntities.OrderRequest OrderRequest { get; set; }
    }
}
