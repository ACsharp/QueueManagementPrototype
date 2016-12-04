using System;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;


namespace TicketingSolution.DataAccess.Direct.Impl
{
    public class OrderRepository : Interfaces.IOrderRepository
    {
        private string connectionString;

        public OrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<Entities.Order> GetOrderAsync(Guid requestId)
        {
            using (var dataStore = new DataStore.TicketingDataStore(connectionString))
            {
                var item = await dataStore.Orders.FirstOrDefaultAsync(o => o.RequestId == requestId);
                if (item == null) return null;
                return ConvertFromDataStoreEntity(item);
            }
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

