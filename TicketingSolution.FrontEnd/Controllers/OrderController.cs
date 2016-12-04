using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketingSolution.FrontEnd.Controllers
{
    public class OrderController : Controller
    {
        private Messaging.QueueWriting.Interfaces.IQueueWriter queueWriter;
        private DataAccess.Direct.Interfaces.IOrderRepository orderRepository;

        public OrderController(Messaging.QueueWriting.Interfaces.IQueueWriter queueWriter,
            DataAccess.Direct.Interfaces.IOrderRepository orderRepository)
        {
            this.queueWriter = queueWriter;
            this.orderRepository = orderRepository;
        }

        public async Task<IActionResult> Status(Guid id)
        {
            var result = await orderRepository.GetOrderAsync(id);
            if (result == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View("NotProcessed");
            }
            else
            {
                var confirmedOrder = new BusinessEntities.ConfirmedOrder
                {
                    EventDate = result.EventDate,
                    OrderId = result.OrderId,
                    Name = result.Name,
                    Quantity = result.Quantity,
                    RequestId = result.RequestId
                };
                return View(confirmedOrder);
            }
        }

        public IActionResult Create()
        {
            return View(new BusinessEntities.OrderRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BusinessEntities.OrderRequest orderRequest)
        {
            orderRequest.RequestId = Guid.NewGuid();
            await queueWriter.CreateOrderRequestAsync(orderRequest);
            return View("Confirmation", orderRequest);
        }
    }
}
