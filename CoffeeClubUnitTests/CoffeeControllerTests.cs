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
        public async Task GetAllCoffees_Returns_AllCoffees()
        {
            // Arrange
            var mockLogger = new Mock<ILoggerManager>();
            var mockCoffeeRepository = new Mock<IRepositoryManager>();
            mockCoffeeRepository.Setup(repo => repo.Coffee.GetAllCoffeesAsync(It.IsAny<bool>())).Returns(Task.FromResult(new List<Coffee>()));
            var mockMapper = new Mock<IMapper>();
            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockLogger.Object, mockMapper.Object);
            
            // Act
            var result = await coffeeController.GetCoffees();

            // Assert
            var coffees = new CoffeeDTO[0];
            var actionResult = Assert.IsAssignableFrom<ActionResult>(result);
            var model = Assert.IsAssignableFrom<OkObjectResult>(actionResult);
            Assert.Equal(coffees, model.Value);
        }

        [Fact]
        public async Task GetCoffee_Returns_Coffee()
        {
            // Arrange
            var mockLogger = new Mock<ILoggerManager>();
            var mockCoffeeRepository = new Mock<IRepositoryManager>();
            mockCoffeeRepository.Setup(repo => repo.Coffee.GetCoffeeByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(Task.FromResult(new Coffee()));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<CoffeeDTO>(It.IsAny<Coffee>())).Returns(new CoffeeDTO());
            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = await coffeeController.GetCoffee(1);
            var coffee = new CoffeeDTO() { CoffeeId = 0, CoffeeName = null, CoffeePrice = 0, CountryOfOrigin = null };
            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<CoffeeDTO>(objectResult.Value);
            
            Assert.Equal(coffee.CoffeeId, model.CoffeeId);
            Assert.Equal(coffee.CoffeeName, model.CoffeeName);
            Assert.Equal(coffee.CoffeePrice, model.CoffeePrice);
            Assert.Equal(coffee.CountryOfOrigin, model.CountryOfOrigin);
        }

        [Fact]
        public async Task GetCoffee_Returns_NotFound()
        {
            // Arrange
            var mockLogger = new Mock<ILoggerManager>();
            var mockCoffeeRepository = new Mock<IRepositoryManager>();
            mockCoffeeRepository.Setup(repo => repo.Coffee.GetCoffeeByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(Task.FromResult((Coffee)null));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<CoffeeDTO>(It.IsAny<Coffee>())).Returns(new CoffeeDTO());
            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = await coffeeController.GetCoffee(-1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateCoffee_Returns_Coffee()
        {
            // Arrange
            var mockLogger = new Mock<ILoggerManager>();
            var mockCoffeeRepository = new Mock<IRepositoryManager>();
            mockCoffeeRepository.Setup(repo => repo.Coffee.CreateCoffee(It.IsAny<Coffee>()));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<CoffeeForCreationDTO>(It.IsAny<Coffee>())).Returns(new CoffeeForCreationDTO());
            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockLogger.Object, mockMapper.Object);
            var coffee = new CoffeeForCreationDTO() { CoffeeName = null, CoffeePrice = 0, CountryOfOrigin = null };
            // Act
            var result = await coffeeController.CreateCoffee(coffee);
            // Assert
            var objectResult = Assert.IsType<CreatedAtRouteResult>(result);
            var model = Assert.IsAssignableFrom<CoffeeForCreationDTO>(objectResult.Value);
            Assert.Equal(coffee.CoffeeName, model.CoffeeName);
            Assert.Equal(coffee.CoffeePrice, model.CoffeePrice);
            Assert.Equal(coffee.CountryOfOrigin, model.CountryOfOrigin);
        }

        [Fact]
        public async Task UpdateCoffee_Returns_NotFound()
        {
            // Arrange
            var mockLogger = new Mock<ILoggerManager>();
            var mockCoffeeRepository = new Mock<IRepositoryManager>();
            mockCoffeeRepository.Setup(repo => repo.Coffee.UpdateCoffee(It.IsAny<Coffee>()));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<CoffeeForUpdateDTO>(It.IsAny<Coffee>())).Returns(new CoffeeForUpdateDTO());
            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockLogger.Object, mockMapper.Object);
            var coffee = new CoffeeForUpdateDTO() { CoffeeName = null, CoffeePrice = 0, CountryOfOrigin = null };
            int id = 0;
            // Act
            var result = await coffeeController.UpdateCoffee(id, coffee);
            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task DeleteCoffee_Returns_NotFound()
        {
            // Arrange
            var mockLogger = new Mock<ILoggerManager>();
            var mockCoffeeRepository = new Mock<IRepositoryManager>();
            mockCoffeeRepository.Setup(repo => repo.Coffee.DeleteCoffee(It.IsAny<Coffee>()));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<CoffeeDTO>(It.IsAny<Coffee>())).Returns(new CoffeeDTO());
            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockLogger.Object, mockMapper.Object);
            int id = 1;
            // Act
            var result = await coffeeController.DeleteCoffee(id);
            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task DeleteCoffee_Returns_Coffee()
        {
            // Arrange
            var mockLogger = new Mock<ILoggerManager>();
            var mockCoffeeRepository = new Mock<IRepositoryManager>();
            mockCoffeeRepository.Setup(repo => repo.Coffee.DeleteCoffee(It.IsAny<Coffee>()));
            var mockMapper = new Mock<IMapper>();
            //mockMapper.Setup(x => x.Map<CoffeeDTO>(It.IsAny<Coffee>())).Returns(new CoffeeDTO());
            var coffeeController = new CoffeeController(mockCoffeeRepository.Object, mockLogger.Object, mockMapper.Object);
            int id = 1;
            // Act
            var result = await coffeeController.DeleteCoffee(id);
            // Assert
            var objectResult = Assert.IsType<NoContentResult>(result);
            var model = Assert.IsAssignableFrom<CoffeeDTO>(objectResult);
            Assert.Equal(204, objectResult.StatusCode);
        }
    }
}
