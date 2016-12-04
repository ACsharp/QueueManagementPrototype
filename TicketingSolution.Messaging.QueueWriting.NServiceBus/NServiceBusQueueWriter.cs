using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.BusinessEntities;

namespace TicketingSolution.Messaging.QueueWriting.NServiceBus
{
    public class NServiceBusQueueWriter : Interfaces.IQueueWriter
    {
        private readonly IEndpointInstance endpointInstance;
        public NServiceBusQueueWriter(NServiceBusWriterConfiguration configuraton)
        {

            var endpointConfiguration = new EndpointConfiguration("TicketSolutionNServiceBusSender");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendOnly();

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            var routing = transport.Routing();

            routing.RouteToEndpoint(typeof(NServiceBusMessages.CreateOrder).Assembly, configuraton.QueueName);

            endpointInstance = Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task CreateOrderRequestAsync(OrderRequest orderRequest)
        {
            var message = new NServiceBusMessages.CreateOrder
            {
                OrderRequest = orderRequest
            };
            await endpointInstance.Send(message).ConfigureAwait(false);
        }
    }
}
