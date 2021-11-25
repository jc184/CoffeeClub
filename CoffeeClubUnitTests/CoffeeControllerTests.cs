using AutoMapper;
using CoffeeClub.Controllers;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeClubUnitTests
{
    public class CoffeeControllerTests
    {

        [Fact]
        public async Task GetCoffees_Returns_AllCoffees()
        {
            // Arrange
            var mockRepo = new Mock<IRepositoryManager>();
            var mockLogger = new Mock<ILoggerManager>();
            var mockMapper = new Mock<IMapper>();
            mockRepo.Setup(repo => repo.Coffee.GetAllCoffeesAsync(It.IsAny<bool>())).Returns(Task.FromResult(new List<Coffee>()));

            var controller = new CoffeeController(mockRepo.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = await controller.GetCoffees();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(
                actionResult);

        }

        [Fact]
        public async Task GetCoffee_Returns_Coffee()
        {
            // Arrange
            var mockRepo = new Mock<IRepositoryManager>();
            var mockLogger = new Mock<ILoggerManager>();
            var mockMapper = new Mock<IMapper>();
            mockRepo.Setup(repo => repo.Coffee.GetCoffeeByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(Task.FromResult(new Coffee()));

            var controller = new CoffeeController(mockRepo.Object, mockLogger.Object, mockMapper.Object);

            // Act
            int Id = 1; 
            var result = await controller.GetCoffee(Id);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(
                actionResult);

        }

        //[Fact]
        //public async Task CreateCoffee_Returns_Coffee()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IRepositoryManager>();
        //    var mockLogger = new Mock<ILoggerManager>();
        //    var mockMapper = new Mock<IMapper>();
        //    mockRepo.Setup(repo => repo.Coffee.CreateCoffee(It.IsAny<Coffee>())).Verifiable();

        //    string name = null;
        //    var controller = new CoffeeController(mockRepo.Object, mockLogger.Object, mockMapper.Object);

        //    // Act
        //    Coffee coffee = new Coffee();
        //    CoffeeForCreationDTO coffeeDto = new CoffeeForCreationDTO() { CoffeeName = "somename", CoffeePrice = 5.00, CountryOfOrigin = "somecountry" };
            
        //    //var mappedCoffee = mockMapper.Object.Map(coffee, coffeeDto);
        //    var actionResult = controller.CreateCoffee(coffeeDto).Result;
        //    //var result = actionResult.Result;
        //    CreatedAtRouteResult carResult = actionResult as CreatedAtRouteResult;
        //    // Assert
        //    //var actionResult = Assert.IsAssignableFrom<CreatedAtRouteResult>(result).Value;
        //    //var model = Assert.IsAssignableFrom<OkObjectResult>(
        //    //    actionResult);
        //    //Assert.NotNull(createdResult);
        //    //Assert.Equal("DefaultApi", createdResult.RouteName);
        //    //Assert.NotNull(createdResult.RouteValues["id"]);
        //    Assert.IsType<CreatedAtRouteResult>(carResult);
        //}
    }
}
