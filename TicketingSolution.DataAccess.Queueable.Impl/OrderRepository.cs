using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.DataAccess.Queueable.Interfaces;

namespace TicketingSolution.DataAccess.Queueable.Impl
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<Entities.Order> CreateAsync(Entities.Order order)
        {
            var newOrder = ConvertToDataStoreEntity(order);
            using (var dataStore = new DataStore.TicketingDataStore())
            {
                dataStore.Orders.Add(newOrder);
                await dataStore.SaveChangesAsync();
                return ConvertFromDataStoreEntity(newOrder);
            }
        }

        private DataStore.Order ConvertToDataStoreEntity(Entities.Order value)
        {
            return new DataStore.Order
            {
                OrderId = value.OrderId,
                Name = value.Name,
                Quantity = value.Quantity,
                RequestId = value.RequestId,
                EventDate = value.EventDate
            };
        }

        private Entities.Order ConvertFromDataStoreEntity(DataStore.Order value)
        {
            return new Entities.Order
            {
                OrderId = value.OrderId,
                Name = value.Name,
                Quantity = value.Quantity,
                RequestId = value.RequestId,
                EventDate = value.EventDate
            };
        }
    }
}
