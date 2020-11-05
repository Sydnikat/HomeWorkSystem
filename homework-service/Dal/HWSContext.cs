using homework_service.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework_service.Dal
{
    public class HWSContext : DbContext
    {
        public HWSContext(DbContextOptions<HWSContext> options) 
            : base(options)
        {
        
        }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().ToTable("Comments");
        }
    }
}
