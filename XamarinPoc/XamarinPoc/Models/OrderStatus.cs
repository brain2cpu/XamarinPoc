using Newtonsoft.Json;

namespace XamarinPoc.Models
{
    public class OrderStatus
    {
        [JsonProperty("orderId")]
        public int Id { get; set; }

        [JsonProperty("totalPrice")]
        public decimal Price { get; set; }

        [JsonProperty("totalQuantity")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public bool IsSuccess { get; set; }
    }
}
