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
        Task<IEnumerable<Coffee>> GetAllCoffeesAsync(bool trackChanges);
        Task<Coffee> GetCoffeeById(int coffeeId, bool trackChanges);
        Task<Coffee> GetCoffeeWithDetails(int coffeeId, bool trackChanges);
        void CreateCoffee(Coffee coffee);
        void UpdateCoffee(Coffee owner);
        void DeleteCoffee(Coffee owner);
    }
}
