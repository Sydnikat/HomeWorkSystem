using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    internal class HomeworkGraderConfiguration : IEntityTypeConfiguration<HomeworkGranderJoin>
    {
        public void Configure(EntityTypeBuilder<HomeworkGranderJoin> builder)
        {
            builder
                 .HasKey(hgj => new { hgj.HomeworkId, hgj.GraderId });

            builder
                .HasOne(hgj => hgj.Grader)
                .WithMany(g => g.Homeworks)
                .HasForeignKey(hgj => hgj.GraderId);

            builder
                .HasOne(hgj => hgj.Homework)
                .WithMany(s => s.Graders)
                .HasForeignKey(hgj => hgj.HomeworkId);
        }
    }
}
