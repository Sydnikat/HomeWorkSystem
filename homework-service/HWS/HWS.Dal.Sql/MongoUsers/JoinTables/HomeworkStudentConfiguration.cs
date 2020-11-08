using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    internal class HomeworkStudentConfiguration : IEntityTypeConfiguration<HomeworkStudentJoin>
    {
        public void Configure(EntityTypeBuilder<HomeworkStudentJoin> builder)
        {
            builder
                 .HasKey(hsj => new { hsj.HomeworkId, hsj.StudentId });

            builder
                .HasOne(hsj => hsj.Student)
                .WithMany(g => g.Homeworks)
                .HasForeignKey(hsj => hsj.StudentId);

            builder
                .HasOne(hsj => hsj.Homework)
                .WithMany(s => s.Students)
                .HasForeignKey(hsj => hsj.HomeworkId);
        }
    }
}
