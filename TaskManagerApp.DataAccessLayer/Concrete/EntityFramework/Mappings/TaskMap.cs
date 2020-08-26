using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.DataAccessLayer.Concrete.EntityFramework.Mappings
{
    public class TaskMap : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable(@"Tasks", "dbo");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.UserId).HasColumnName("UserId");
            builder.Property(t => t.Title).HasColumnName("Title");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.IsCompleted).HasColumnName("IsCompleted");
            builder.Property(t => t.TypeId).HasColumnName("TypeId");
            builder.Property(t => t.StartingDate).HasColumnName("StartingDate");
        }
    }
}
