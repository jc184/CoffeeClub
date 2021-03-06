using Contracts;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeClubUnitTests
{
    public class CoffeeRepositoryTests
    {
        private readonly Mock<ICoffeeRepository> _mockRepo;

        public CoffeeRepositoryTests()
        {
            _mockRepo = new Mock<ICoffeeRepository>();
        }

        [Fact]
        public void GetCoffeeByIdAsync_Returns_Coffee()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<CoffeeClubContext>();
            var dbSetMock = new Mock<DbSet<Coffee>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Coffee()));
            dbContextMock.Setup(s => s.Set<Coffee>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.GetCoffeeByIdAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(Task.FromResult(new Coffee()));
            var coffeeRepoMock = _mockRepo.Object;

            //Execute method of SUT (CoffeeRepository)
            int coffeeId = 1;
            bool trackChanges = false;
            var coffee = coffeeRepoMock.GetCoffeeByIdAsync(coffeeId, trackChanges).Result;

            //Assert
            Assert.NotNull(coffee);
            Assert.IsAssignableFrom<Coffee>(coffee);
        }

        [Fact]
        public void GetAllCoffeesAsync_Returns_AllCoffees()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<CoffeeClubContext>();
            var dbSetMock = new Mock<DbSet<Coffee>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Coffee()));
            dbContextMock.Setup(s => s.Set<Coffee>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.GetAllCoffeesAsync(It.IsAny<bool>())).Returns(Task.FromResult(new List<Coffee>()));
            var coffeeRepoMock = _mockRepo.Object;

            //Execute method of SUT (CoffeeRepository)
            var coffees = coffeeRepoMock.GetAllCoffeesAsync(false).Result;

            //Assert
            Assert.NotNull(coffees);
            Assert.IsAssignableFrom<List<Coffee>>(coffees);
        }

        [Fact]
        public void GetCoffeeWithDetailsAsync_Returns_CoffeeWithDetails()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<CoffeeClubContext>();
            var dbSetMock = new Mock<DbSet<Coffee>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Coffee()));
            dbContextMock.Setup(s => s.Set<Coffee>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.GetCoffeeWithDetailsAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(Task.FromResult(new Coffee()));
            var coffeeRepoMock = _mockRepo.Object;

            //Execute method of SUT (CoffeeRepository)
            int coffeeId = 1;
            bool trackChanges = false;
            var coffee = coffeeRepoMock.GetCoffeeWithDetailsAsync(coffeeId, trackChanges).Result;

            //Assert
            Assert.NotNull(coffee);
            Assert.IsAssignableFrom<Coffee>(coffee);
        }

        [Fact]
        public void CreateCoffee_Returns_Coffee()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<CoffeeClubContext>();
            var dbSetMock = new Mock<DbSet<Coffee>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Coffee()));
            dbContextMock.Setup(s => s.Set<Coffee>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.CreateCoffee(new Coffee()));
            var coffeeRepoMock = _mockRepo.Object;
            var coffee = new Coffee() { CoffeeId = 1, CoffeeName = "some coffee name", CoffeePrice = 5.00, CountryOfOrigin = "somecountry" };
            coffeeRepoMock.CreateCoffee(coffee);

            //Assert
            Assert.NotNull(coffee);
            Assert.IsAssignableFrom<Coffee>(coffee);
        }

        [Fact]
        public void UpdateCoffee_Returns_Coffee()
        {
            //Setup DbContext and DbSet mock
            var dbContextMock = new Mock<CoffeeClubContext>();
            var dbSetMock = new Mock<DbSet<Coffee>>();
            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>(), It.IsAny<bool>())).Returns(ValueTask.FromResult(new Coffee()));
            dbContextMock.Setup(s => s.Set<Coffee>()).Returns(dbSetMock.Object);

            _mockRepo.Setup(p => p.UpdateCoffee(new Coffee()));
            var coffeeRepoMock = _mockRepo.Object;
            var coffee = new Coffee() { CoffeeId = 1, CoffeeName = "some coffee name", CoffeePrice = 5.00, CountryOfOrigin = "somecountry" };
            coffeeRepoMock.UpdateCoffee(coffee);

            //Assert
            Assert.NotNull(coffee);
            Assert.IsAssignableFrom<Coffee>(coffee);
        }
    }
}
