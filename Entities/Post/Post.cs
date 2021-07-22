using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
   public class Post :BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Categoryid { get; set; }
        public int AuthorId { get; set; }

        public Category Category { get; set; }
        public User Author { get; set; }

        public class PostConfiguration : IEntityTypeConfiguration<Post>
        {
            public void Configure(EntityTypeBuilder<Post> builder)
            {
                builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
                builder.Property(p => p.Description).IsRequired();
                builder.HasOne(p => p.Category).WithMany(c => c.Posts).HasForeignKey(p => p.Categoryid);
                builder.HasOne(p => p.Author).WithMany(c => c.Posts).HasForeignKey(p => p.AuthorId);
            }

        }
    }
}
