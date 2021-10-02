using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CoffeeDTO
    {
        public int CoffeeId { get; set; }

        public string CoffeeName { get; set; }

        public double CoffeePrice { get; set; }

        public string CountryOfOrigin { get; set; }

    }
}
