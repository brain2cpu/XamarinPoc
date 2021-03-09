namespace XamarinPoc.Models
{
    public class OrderItem
    {
        public string Id { get; set; }
        
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; } = 0m;
    }
}
