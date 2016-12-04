using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSolution.DataAccess.Direct.Interfaces
{
    public interface IOrderRepository
    {
        Task<Entities.Order> GetOrderAsync(Guid requestId);
    }
}
