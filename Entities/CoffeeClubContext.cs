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
        public CoffeeClubContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}
