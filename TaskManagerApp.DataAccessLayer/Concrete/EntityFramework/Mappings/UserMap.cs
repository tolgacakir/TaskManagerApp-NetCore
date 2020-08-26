using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.DataAccessLayer.Concrete.EntityFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(@"Users", "dbo");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("Id");
            builder.Property(u => u.Username).HasColumnName("Username");
            builder.Property(u => u.Password).HasColumnName("Password");
        }
    }
}
