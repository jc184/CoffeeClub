using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ICoffeeRepository : IRepositoryBase<Coffee>
    {
        Task<List<Coffee>> GetAllCoffeesAsync(bool trackChanges);
        Task<Coffee> GetCoffeeByIdAsync(int coffeeId, bool trackChanges);
        Task<Coffee> GetCoffeeWithDetailsAsync(int coffeeId, bool trackChanges);
        void CreateCoffee(Coffee coffee);
        void UpdateCoffee(Coffee coffee);
        void DeleteCoffee(Coffee coffee);
    }
}
