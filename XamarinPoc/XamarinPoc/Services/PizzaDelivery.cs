using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinPoc.Interfaces;
using XamarinPoc.Models;

namespace XamarinPoc.Services
{
    class PizzaDelivery : IPizzaDelivery, IDisposable
    {
        private const string BaseUrl = "http://192.168.1.69:8080/api/";
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
            HttpResponseMessage response = await _client.GetAsync($"{BaseUrl}Products/1/1/100");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PizzaDetails[]>(json);
            }
            // in real life we probably would retry the request and show an error to the user if keeps failing
            // and we'd throw a custom exception NOT this one:
            throw new Exception("API request failed");
        }

        public async Task<PizzaDetails> GetDetailsAsync(int pizzaId)
        {
            var response = await _client.GetAsync($"{BaseUrl}Products/{pizzaId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PizzaDetails>(json);
            }

            throw new Exception("API request failed");
        }

        public async Task<OrderStatus> OrderAsync(Order order)
        {
            using var requestContent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, MediaType);

            using var postRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{BaseUrl}Orders"),
                Content = requestContent
            };

            var response = await _client.SendAsync(postRequest);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<OrderStatus>(json);
                status.IsSuccess = true;

                return status;
            }

            return new OrderStatus
            {
                IsSuccess = false
            };
        }

        public async Task<string> GetOrderStatusAsync(int orderId)
        {
            var response = await _client.GetAsync($"{BaseUrl}Orders/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(json);
            }

            return await Task.FromResult("FAILED");
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}