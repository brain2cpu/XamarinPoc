using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinPoc.Models;

namespace XamarinPoc.Interfaces
{
    interface IPizzaDelivery
    {
        Task<IEnumerable<Pizza>> GetVariationsAsync();
        
        Task<PizzaDetails> GetDetailsAsync(string pizzaId);

        Task<OrderStatus> OrderAsync(Order order);

        Task<OrderStatus> GetOrderStatusAsync(string orderId);
    }
}
