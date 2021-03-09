using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace XamarinPoc.Models
{
    public class Order
    {
        private readonly List<OrderItem> _items = new ();

        public string Id { get; set; }

        public decimal Price { get; set; } = 0m;
        
        public IReadOnlyList<OrderItem> Items => new ReadOnlyCollection<OrderItem>(_items);

        public void SetItem(OrderItem o)
        {
            if (o == null)
                throw new NullReferenceException("A valid OrderItem must be added");

            var ex = _items.SingleOrDefault(x => string.Equals(x.Id, o.Id));
            // new item
            if (ex == null)
            {
                if (o.Quantity == 0)
                    throw new InvalidOperationException("Cannot add item with zero quantity");

                Price += o.UnitPrice * o.Quantity;
                _items.Add(o);

                return;
            }

            Price -= ex.UnitPrice * ex.Quantity;
            if (o.Quantity == 0)
            {
                _items.Remove(ex);
            }
            else
            {
                Price += o.UnitPrice * o.Quantity;
                ex.Quantity = o.Quantity;
            }
        }

        public bool IsValid => _items.Any() && _items.Sum(x => x.Quantity) > 0;
    }
}
