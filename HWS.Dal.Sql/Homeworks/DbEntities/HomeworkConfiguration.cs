using HWS.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.Homeworks.DbEntities
{
    internal class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder
                .ToTable("Homeworks");

            builder
               .HasMany(h => h.Comments)
               .WithOne(c => c.Homework)
               .HasForeignKey(c => c.HomeworkId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasMany(h => h.Assignments)
                .WithOne(a => a.Homework)
                .HasForeignKey(a => a.HomeworkId);
        }
    }
}
