using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWS.Dal.Sql.Groups.DbEntities
{
    internal class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder
                .ToTable("Groups");

            builder
               .HasMany(g => g.Comments)
               .WithOne(c => c.Group)
               .HasForeignKey(c => c.GroupId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasMany(g => g.Homeworks)
                .WithOne(h => h.Group)
                .HasForeignKey(h => h.GroupId);
        }
    }
}
