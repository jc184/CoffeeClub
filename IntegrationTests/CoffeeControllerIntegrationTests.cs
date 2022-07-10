using CoffeeClub;
using Entities.DTOs;
using Entities.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class CoffeeControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;
        public CoffeeControllerIntegrationTests(TestingWebAppFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Read_GET_Action()
        {
            // Act
            var response = await _client.GetAsync("/api/coffee");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            var coffees = JsonConvert.DeserializeObject<List<Coffee>>(responseString);

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("{\"coffeeId\":1,\"coffeeName\":\"Bolivian Blend\",\"coffeePrice\":2.99,\"countryOfOrigin\":\"Bolivia\"}", responseString);
            Assert.True(coffees.Count > 0);
        }

        [Fact]
        public async Task Read_GETById_Action()
        {
            int id = 1;
            // Act
            var response = await _client.GetAsync($"/api/coffee/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("{\"coffeeId\":1,\"coffeeName\":\"Bolivian Blend\",\"coffeePrice\":2.99,\"countryOfOrigin\":\"Bolivia\"}", responseString);
        }


        [Fact]
        public async Task Create_WhenPOSTExecuted_CreateCoffee()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/coffee");

            var formModel = new Dictionary<string, string>
            {
                { "CoffeeName", "Bolivian Blend" },
                { "CoffeePrice", "2.99" },
                { "CountryOfOrigin", "Bolivia" }
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            var response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("{\"coffeeName\":\"Bolivian Blend\",\"coffeePrice\":2.99,\"countryOfOrigin\":\"Bolivia\"}", responseString);

        }

        [Fact]
        public async Task Update_WhenPUT_Executed_UpdateCoffee()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Put, "/api/coffee/1");

            var formModel = new Dictionary<string, string>
            {
                { "CoffeeName", "Bolivian Blend" },
                { "CoffeePrice", "2.99" },
                { "CountryOfOrigin", "Bolivia" }
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            var response = await _client.SendAsync(postRequest);
            
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WhenDELETE_Executed_DeleteCoffee()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Delete, "/api/coffee/11");

            var response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
