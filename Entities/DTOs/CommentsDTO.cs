using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CommentsDTO
    {
        public int CommentId { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
