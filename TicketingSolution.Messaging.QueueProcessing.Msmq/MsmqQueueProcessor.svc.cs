using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.BusinessEntities;
using TicketingSolution.DataAccess.Queueable.Interfaces;
using TicketingSolution.Messaging.QueueProcessing.Msmq;

namespace TicketingSolutionMsmqQueueProcessing
{
    public class MsmqQueueProcessor : IMsmqQueueProcessor
    {
        private readonly IOrderRepository orderRepository;
        public MsmqQueueProcessor(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public async Task CreateOrderRequest(OrderRequest orderRequest)
        {
            var order = new TicketingSolution.DataAccess.Entities.Order
            {
                EventDate = orderRequest.EventDate,
                Name = orderRequest.Name,
                RequestId = orderRequest.RequestId,
                Quantity = orderRequest.Quantity
            };
            await orderRepository.CreateAsync(order);
        }
    }
}
