using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinPoc.Interfaces;
using XamarinPoc.Models;

namespace XamarinPoc.Services
{
    class PizzaDelivery : IPizzaDelivery
    {
        // all fake pizza will look the same
        private const string PizzaImage =
            "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Eq_it-na_pizza-margherita_sep2005_sml.jpg/320px-Eq_it-na_pizza-margherita_sep2005_sml.jpg";

        private const string BaseUrl = "https://example.com/";
        private const string MediaType = "application/json";

        private readonly HttpClient _client;

        public PizzaDelivery()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            _client.DefaultRequestHeaders.Add("User-Agent", "AgentSmith");
        }

        public async Task<IEnumerable<Pizza>> GetVariationsAsync()
        {
            //throw new Exception("FAKE exception");

            HttpResponseMessage response = await _client.GetAsync($"{BaseUrl}get_list");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pizza[]>(json);
            }
            // in real life we probably would retry the request and show an error to the user if keeps failing
            // for now we just load the fake data ...

            return await Task.FromResult(new[]
                {
                    new Pizza {Name = "Prosciutto", ImageUri = PizzaImage},
                    new Pizza {Name = "Prosciutto XXL", ImageUri = PizzaImage},
                    new Pizza {Name = "Prosciutto Funghi", ImageUri = PizzaImage},
                    new Pizza {Name = "Prosciutto Funghi XXL", ImageUri = PizzaImage},
                    new Pizza {Name = "Prosciutto Crudo", ImageUri = PizzaImage},
                    new Pizza {Name = "Prosciutto Crudo XXL", ImageUri = PizzaImage},
                    new Pizza {Name = "Quattro Formaggi", ImageUri = PizzaImage},
                    new Pizza {Name = "Quattro Formaggi XXL", ImageUri = PizzaImage},
                    new Pizza {Name = "Quattro Stagione", ImageUri = PizzaImage},
                    new Pizza {Name = "Quattro Stagione XXL", ImageUri = PizzaImage},
                    new Pizza {Name = "Diavola", ImageUri = PizzaImage},
                    new Pizza {Name = "Diavola XXL", ImageUri = PizzaImage},
                    new Pizza {Name = "Hawai", ImageUri = PizzaImage},
                    new Pizza {Name = "Hawai XXL", ImageUri = PizzaImage},
                    new Pizza {Name = "Salami", ImageUri = PizzaImage},
                    new Pizza {Name = "Salami XXL", ImageUri = PizzaImage},
                    new Pizza {Name = "Margherita", ImageUri = PizzaImage},
                    new Pizza {Name = "Margherita XXL", ImageUri = PizzaImage},
                    new Pizza {Name = "Capriciosa", ImageUri = PizzaImage},
                    new Pizza {Name = "Capriciosa XXL", ImageUri = PizzaImage},
                }
            );
        }

        public async Task<bool> OrderAsync(Order order)
        {
            using var requestContent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, MediaType);

            using var postRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{BaseUrl}order"),
                Content = requestContent
            };
            
            var response = await _client.SendAsync(postRequest);
            return response.IsSuccessStatusCode;
        }
    }
}