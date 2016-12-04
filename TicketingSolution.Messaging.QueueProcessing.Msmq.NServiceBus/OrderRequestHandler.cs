using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSolution.Messaging.QueueProcessing.NServiceBus
{
    public class OrderRequestHandler : IHandleMessages<NServiceBusMessages.CreateOrder>
    {
        private readonly DataAccess.Queueable.Interfaces.IOrderRepository orderRepository;

        public OrderRequestHandler()
        {
            orderRepository = new DataAccess.Queueable.Impl.OrderRepository();
        }
        public async Task Handle(NServiceBusMessages.CreateOrder message, IMessageHandlerContext context)
        {
            var order = new TicketingSolution.DataAccess.Entities.Order
            {
                EventDate = message.OrderRequest.EventDate,
                Name = message.OrderRequest.Name,
                RequestId = message.OrderRequest.RequestId,
                Quantity = message.OrderRequest.Quantity
            };
            await orderRepository.CreateAsync(order);
        }

    }
}
