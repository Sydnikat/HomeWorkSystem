using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.MongoUsers.JoinTables
{
    internal class GroupTeacherConfiguration : IEntityTypeConfiguration<GroupTeacherJoin>
    {
        public void Configure(EntityTypeBuilder<GroupTeacherJoin> builder)
        {
            builder
                 .HasKey(gsj => new { gsj.GroupId, gsj.TeacherId });

            builder
                .HasOne(gsj => gsj.Group)
                .WithMany(g => g.Teachers)
                .HasForeignKey(gsj => gsj.GroupId);

            builder
                .HasOne(gsj => gsj.Teacher)
                .WithMany(s => s.Groups)
                .HasForeignKey(gsj => gsj.TeacherId);
        }
    }
}
