using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
                return JsonConvert.DeserializeObject<PizzaDetails[]>(json);
            }
            // in real life we probably would retry the request and show an error to the user if keeps failing
            // for now we just load the fake data ...

            return await Task.FromResult(new[]
                {
                    new Pizza {Id = "01", Name = "Prosciutto", ImageUri = PizzaImage},
                    new Pizza {Id = "02", Name = "Prosciutto XXL", ImageUri = PizzaImage},
                    new Pizza {Id = "03", Name = "Prosciutto Funghi", ImageUri = PizzaImage},
                    new Pizza {Id = "04", Name = "Prosciutto Funghi XXL", ImageUri = PizzaImage},
                    new Pizza {Id = "05", Name = "Prosciutto Crudo", ImageUri = PizzaImage},
                    new Pizza {Id = "06", Name = "Prosciutto Crudo XXL", ImageUri = PizzaImage},
                    new Pizza {Id = "07", Name = "Quattro Formaggi", ImageUri = PizzaImage},
                    new Pizza {Id = "08", Name = "Quattro Formaggi XXL", ImageUri = PizzaImage},
                    new Pizza {Id = "09", Name = "Quattro Stagione", ImageUri = PizzaImage},
                    new Pizza {Id = "10", Name = "Quattro Stagione XXL", ImageUri = PizzaImage},
                    new Pizza {Id = "11", Name = "Diavola", ImageUri = PizzaImage},
                    new Pizza {Id = "12", Name = "Diavola XXL", ImageUri = PizzaImage},
                    new Pizza {Id = "13", Name = "Hawai", ImageUri = PizzaImage},
                    new Pizza {Id = "14", Name = "Hawai XXL", ImageUri = PizzaImage},
                    new Pizza {Id = "15", Name = "Salami", ImageUri = PizzaImage},
                    new Pizza {Id = "16", Name = "Salami XXL", ImageUri = PizzaImage},
                    new Pizza {Id = "17", Name = "Margherita", ImageUri = PizzaImage},
                    new Pizza {Id = "18", Name = "Margherita XXL", ImageUri = PizzaImage},
                    new Pizza {Id = "19", Name = "Capriciosa", ImageUri = PizzaImage},
                    new Pizza {Id = "20", Name = "Capriciosa XXL", ImageUri = PizzaImage},
                }
            );
        }

        public Task<PizzaDetails> GetDetailsAsync(string pizzaId)
        {
            return Task.FromResult(new PizzaDetails
            {
                Id = pizzaId,
                Name = "Diavola XXL",
                ImageUri = PizzaImage,
                Price = 21.5m,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis quis dapibus est, in faucibus eros. " +
                              "Phasellus dapibus, nulla quis consectetur accumsan, erat magna tincidunt libero, eu porta lacus dolor porta justo. " +
                              "Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Fusce facilisis urna ut libero laoreet aliquet."
            });
        }

        public async Task<OrderStatus> OrderAsync(Order order)
        {
            //using var requestContent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, MediaType);

            //using var postRequest = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Post,
            //    RequestUri = new Uri($"{BaseUrl}order"),
            //    Content = requestContent
            //};

            //var response = await _client.SendAsync(postRequest);
            //if (response.IsSuccessStatusCode)
            //{
            //    var json = await response.Content.ReadAsStringAsync();
            //    return JsonConvert.DeserializeObject<OrderStatus>(json);
            //}

            //return new OrderStatus
            //{
            //    Id = order.Id,
            //    IsSuccess = false,
            //    Status = response.ReasonPhrase
            //};

            // for now:
            return await Task.FromResult(new OrderStatus
            {
                Id = order.Id,
                IsSuccess = true,
                Status = "cooking"
            });
        }

        public Task<OrderStatus> GetOrderStatusAsync(string orderId)
        {
            return Task.FromResult(new OrderStatus
            {
                Id = orderId,
                IsSuccess = true,
                Status = "cooking"
            });
        }
    }
}