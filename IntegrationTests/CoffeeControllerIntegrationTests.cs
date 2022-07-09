using AutoMapper;
using CoffeeClub;
using Entities.DTOs;
using Entities.Models;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("{\"coffeeId\":1,\"coffeeName\":\"Cappuccino\",\"coffeePrice\":2.5,\"countryOfOrigin\":\"Italy\"}", responseString);
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
            Assert.Contains("{\"coffeeId\":1,\"coffeeName\":\"Cappuccino\",\"coffeePrice\":2.5,\"countryOfOrigin\":\"Italy\"}", responseString);
        }

        //[Fact]
        //public async Task Create_POST_Action()
        //{
        //    var payload = new CoffeeForCreationDTO() { CoffeeName = "Bolivian Blend", CoffeePrice = 2.99, CountryOfOrigin = "Bolivia" };
        //    //HttpContent content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
        //    var content = new StringContent(JsonConvert.SerializeObject(payload.ToString()), Encoding.UTF8, "application/json");
        //    // Act
        //    var response = await _client.PostAsync("/api/coffee", content);

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();

        //    Assert.Contains("Create Record", responseString);
        //}
    }
}
