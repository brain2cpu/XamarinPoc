using Newtonsoft.Json;

namespace XamarinPoc.Models
{
    public class Pizza
    {
        [JsonProperty("productId")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUri { get; set; }
        
        [JsonProperty("price")]
        public decimal Price { get; set; }

        public override string ToString() => Name;
    }
}