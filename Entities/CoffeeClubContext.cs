using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CoffeeClubContext : DbContext
    {
        public CoffeeClubContext()
        {

        }
        public CoffeeClubContext(DbContextOptions<CoffeeClubContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseInMemoryDatabase("InMemoryDb");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coffee>().HasMany(s => s.Comments);
        }
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}
