using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSolution.Messaging.QueueProcessing.Msmq
{
    [ServiceContract]
    public interface IMsmqQueueProcessor
    {

        [OperationContract(IsOneWay = true)]
        Task CreateOrderRequest(BusinessEntities.OrderRequest orderRequest);
    }

}
