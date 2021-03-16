using Newtonsoft.Json;

namespace XamarinPoc.Models
{
    public class PizzaDetails : Pizza
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("weight")]
        public string Weight { get; set; }
    }
}
