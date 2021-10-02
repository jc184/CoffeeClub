using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CommentsForCreationDTO
    {
        public string Comment { get; set; }

        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
