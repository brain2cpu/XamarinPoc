using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinPoc.Models;

namespace XamarinPoc.Interfaces
{
    interface IPizzaDelivery
    {
        Task<IEnumerable<Pizza>> GetVariationsAsync();
        
        Task<PizzaDetails> GetDetailsAsync(int pizzaId);

        Task<OrderStatus> OrderAsync(Order order);

        Task<string> GetOrderStatusAsync(int orderId);
    }
}
