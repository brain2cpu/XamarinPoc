using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinPoc.Models;

namespace XamarinPoc.Interfaces
{
    interface IPizzaDelivery
    {
        Task<IEnumerable<Pizza>> GetVariationsAsync();

        Task<bool> OrderAsync(Order order);
    }
}
