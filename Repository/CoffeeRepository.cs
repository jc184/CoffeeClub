using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CoffeeRepository : RepositoryBase<Coffee>, ICoffeeRepository
    {
        public CoffeeRepository(CoffeeClubContext repositoryContext) : base(repositoryContext)
        {
        }


        public void CreateCoffee(Coffee coffee)
        {
            Create(coffee);
        }

        public void DeleteCoffee(Coffee coffee)
        {
            Delete(coffee);
        }

        public async Task<IEnumerable<Coffee>> GetAllCoffeesAsync(bool trackChanges) =>
               await FindAll(trackChanges)
                .OrderBy(c => c.CoffeeName)
                .ToListAsync();
        

        public async Task<Coffee> GetCoffeeByIdAsync(int coffeeId, bool
            trackChanges) =>
            await FindByCondition(coffee => coffee.CoffeeId.Equals(coffeeId), trackChanges)
            .SingleOrDefaultAsync();
        

        public async Task<Coffee> GetCoffeeWithDetailsAsync(int coffeeId, bool trackChanges) =>
        
            await FindByCondition(coffee => coffee.CoffeeId.Equals(coffeeId), trackChanges)
                .Include(comment => comment.Comments)
                .FirstOrDefaultAsync();

        public void UpdateCoffee(Coffee coffee)
        {
            Update(coffee);
        }

    }
}
