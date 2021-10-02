using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected CoffeeClubContext CoffeeClubContext { get; set; }

        public RepositoryBase(CoffeeClubContext coffeeClubContext)
        {
            this.CoffeeClubContext = coffeeClubContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges
                ? CoffeeClubContext.Set<T>()
                    .AsNoTracking()
                : CoffeeClubContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges
                ? CoffeeClubContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking()
                : CoffeeClubContext.Set<T>()
                    .Where(expression);

        public void Create(T entity)
        {
            this.CoffeeClubContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.CoffeeClubContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.CoffeeClubContext.Set<T>().Remove(entity);
        }
    }
}
