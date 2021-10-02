using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(Coffee))]
        public int CoffeeId { get; set; }

        public Coffee Coffee { get; set; }
    }
}
