using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Coffee
    {
        public int CoffeeId { get; set; }

        public string CoffeeName { get; set; }

        public double CoffeePrice { get; set; }

        public string CountryOfOrigin { get; set; }

        public ICollection<Comments> Comments { get; set; }
    }
}
