using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingSolution.Messaging.QueueProcessing.Msmq
{
    public class IoC : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Bind<DataAccess.Queueable.Interfaces.IOrderRepository>().To<DataAccess.Queueable.Impl.OrderRepository>();
        }
    }
}