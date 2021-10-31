using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public User()
        {
            IsActive = true;
            //SecurityStamp = new Guid();
        }
        //[Required]
        //[StringLength(100)]
        //public string UserName { get; set; }
        //[Required]
        //[StringLength(500)]
        //public string PasswordHash { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset LastLogineDate { get; set; }
        public ICollection<Post> Posts { get; set; }
        //public Guid SecurityStamp  { get; set; }

    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
        }
    }
    public enum GenderType
    {
        [Display(Name ="مرد")]
        Male=1,
        [Display(Name = "زن")]
        Famle = 2
    }
}
