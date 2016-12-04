using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TicketingSolution.Messaging.QueueWriting.Msmq
{
    public class MsmqQueueWriter : Interfaces.IQueueWriter
    {
        private readonly EndpointAddress endpointAddress;
        private readonly Binding binding;

        public MsmqQueueWriter (MsmqWriterConfiguration config)
        {
            endpointAddress = new EndpointAddress(config.QueueAddress);
            binding = new NetMsmqBinding(NetMsmqSecurityMode.None)
            {
                ExactlyOnce = true
            };
        }

        public async Task CreateOrderRequestAsync(BusinessEntities.OrderRequest orderRequest)
        {
            var msmqClient = new MsmqWcfWrapper.MsmqQueueProcessorClient(binding, endpointAddress);
            await msmqClient.CreateOrderRequestAsync(orderRequest);
        }
    }
}
