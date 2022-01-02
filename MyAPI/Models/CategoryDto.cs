using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace MyAPI.Models
{
    public class CategoryDto:BaseDto<CategoryDto, Category,int>
    {
  
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

    }
}
