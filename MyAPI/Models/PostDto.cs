using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Models
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Categoryid { get; set; }
        public int AuthorId { get; set; }

        public string CategoryName { get; set; }
        public string AuthorFullName { get; set; }
    }
}
