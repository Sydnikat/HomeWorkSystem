using HWS.Dal.Sql;
using HWS.Dal.Sql.Comments.DbEntities;
using HWS.Dal.Sql.Groups.DbEntities;
using HWS.Dal.Sql.Homeworks.DbEntities;
using HWS.Dal.Sql.MongoUsers.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HWS.Dal
{
    public class HWSContext : DbContext
    {
        public HWSContext(DbContextOptions<HWSContext> options) 
            : base(options)
        {
        
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<MongoUser> MongoUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                assembly: Assembly.GetAssembly(typeof(HWSContext)));

            modelBuilder.Entity<Comment>().ToTable("Comments");
        }
    }
}
