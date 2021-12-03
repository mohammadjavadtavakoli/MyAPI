using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.CustomMapping;

namespace MyAPI.Models
{
    public class PostCustomMapping : IHaveCustomMapping
    {
        public void CreateMappings(Profile profile)
        {
            profile.CreateMap<Post, PostDto>().ReverseMap()
               .ForMember(p => p.Author, opt => opt.Ignore())
              .ForMember(p => p.Category, opt => opt.Ignore());
        }
    }
}
