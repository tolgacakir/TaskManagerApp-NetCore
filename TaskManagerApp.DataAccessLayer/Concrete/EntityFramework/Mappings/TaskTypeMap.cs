using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerApp.Entities.Concrete;

namespace TaskManagerApp.DataAccessLayer.Concrete.EntityFramework.Mappings
{
    public class TaskTypeMap : IEntityTypeConfiguration<TaskType>
    {
        public void Configure(EntityTypeBuilder<TaskType> builder)
        {
            builder.ToTable(@"TaskTypes", "dbo");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
