using System.Threading.Tasks;
using TicketingSolution.DataAccess.Entities;

namespace TicketingSolution.DataAccess.Queueable.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
    }
}