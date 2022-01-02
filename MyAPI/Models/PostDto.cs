using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace MyAPI.Models
{
    public class PostDto:BaseDto<PostDto,Post,Guid>
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public int Categoryid { get; set; }
        public int AuthorId { get; set; }

        public string CategoryName { get; set; }
        public string AuthorFullName { get; set; }
    }
}
