using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    internal class GroupStudentConfiguration : IEntityTypeConfiguration<GroupStudentJoin>
    {
        public void Configure(EntityTypeBuilder<GroupStudentJoin> builder)
        {
            builder
                .HasKey(gsj => new { gsj.GroupId, gsj.StudentId });

            builder
                .HasOne(gsj => gsj.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(gsj => gsj.GroupId);

            builder
                .HasOne(gsj => gsj.Student)
                .WithMany(s => s.Groups)
                .HasForeignKey(gsj => gsj.StudentId);
        }
    }
}
